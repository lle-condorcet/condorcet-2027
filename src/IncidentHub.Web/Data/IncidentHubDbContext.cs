using Microsoft.EntityFrameworkCore;

namespace IncidentHub.Web.Data;

// Le DbContext : la porte d'entrée vers la base de données.
// Chaque DbSet devient une table ; EF Core traduit le LINQ en SQL.
public class IncidentHubDbContext : DbContext
{
    public IncidentHubDbContext(DbContextOptions<IncidentHubDbContext> options) : base(options)
    {
    }

    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();

    // Fluent API : on décrit le mapping ICI, sans mettre d'attributs EF
    // dans le domaine. Le domaine ne sait pas qu'il est stocké en base.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ---- Utilisateurs : héritage TPH (Table Per Hierarchy) ----
        // Une seule table Users pour Reporter, Analyst et Manager.
        // La colonne « Role » (le discriminateur) dit quelle classe recréer.
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Id);
            user.Property(u => u.Id).ValueGeneratedNever();   // le Guid vient du domaine
            user.Property(u => u.Name).HasMaxLength(100);
            user.Property(u => u.Email).HasMaxLength(200);
            user.HasIndex(u => u.Email).IsUnique();

            user.HasDiscriminator<string>("Role")
                .HasValue<Reporter>("Reporter")
                .HasValue<Analyst>("Analyst")
                .HasValue<Manager>("Manager");
        });

        // Team n'existe que pour Analyst : colonne nullable dans Users.
        modelBuilder.Entity<Analyst>().Property(a => a.Team).HasMaxLength(50);

        // ---- Incidents ----
        modelBuilder.Entity<Incident>(incident =>
        {
            incident.HasKey(i => i.Id);
            incident.Property(i => i.Id).ValueGeneratedNever();

            // Title, Description... ont un setter privé : EF Core les remplit quand même.
            incident.Property(i => i.Title).HasMaxLength(120);
            incident.Property(i => i.Description).HasMaxLength(2000);

            // Enums stockés en texte ("Critical") plutôt qu'en nombre (3) :
            // lisible dans la base, et ajouter une valeur ne décale rien.
            incident.Property(i => i.Severity).HasConversion<string>().HasMaxLength(20);
            incident.Property(i => i.Status).HasConversion<string>().HasMaxLength(20);

            // Relations vers Users. Les clés étrangères ReportedById et AssignedToId
            // n'existent pas dans le domaine : ce sont des « propriétés fantômes » (shadow).
            incident.HasOne(i => i.ReportedBy)
                .WithMany()
                .HasForeignKey("ReportedById")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            incident.HasOne(i => i.AssignedTo)
                .WithMany()
                .HasForeignKey("AssignedToId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Comments est une vue en lecture seule : EF Core passe par
            // le champ privé _comments pour lire ET remplir la liste.
            incident.HasMany(i => i.Comments)
                .WithOne()
                .HasForeignKey("IncidentId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            incident.Navigation(i => i.Comments)
                .HasField("_comments")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        // ---- Commentaires ----
        modelBuilder.Entity<Comment>(comment =>
        {
            comment.HasKey(c => c.Id);
            comment.Property(c => c.Id).ValueGeneratedNever();
            comment.Property(c => c.Text).HasMaxLength(2000);

            comment.HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey("AuthorId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
