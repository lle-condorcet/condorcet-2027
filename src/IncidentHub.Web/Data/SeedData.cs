namespace IncidentHub.Web.Data;

// Remplit la base avec le jeu de démonstration, uniquement si elle est vide.
// Appelé au démarrage dans Program.cs, après l'application des migrations.
public static class SeedData
{
    public static void Initialize(IncidentHubDbContext db)
    {
        if (db.Users.Any())
        {
            return;   // déjà rempli : on ne touche à rien
        }

        var data = DemoData.Create();

        // Add marque les objets « à insérer » ; rien n'est encore envoyé.
        // Les commentaires suivent leur incident (composition).
        db.Users.AddRange(data.Users);
        db.Incidents.AddRange(data.Incidents);

        // Un seul SaveChanges : tous les INSERT dans une même transaction.
        db.SaveChanges();
    }
}
