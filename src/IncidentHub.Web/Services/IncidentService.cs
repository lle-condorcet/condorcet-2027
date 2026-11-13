using IncidentHub.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace IncidentHub.Web.Services;

// Couche application : les pages demandent « la liste », « un incident »...
// Le service sait COMMENT l'obtenir (EF Core, requêtes, Include).
// Les pages ne voient jamais le DbContext.
public class IncidentService
{
    private readonly IncidentHubDbContext _db;

    public IncidentService(IncidentHubDbContext db)
    {
        _db = db;
    }

    // Liste pour l'écran Index : UNE seule requête SQL (avec des JOIN).
    public async Task<List<Incident>> GetAllAsync()
    {
        return await _db.Incidents
            .AsNoTracking()                 // lecture seule : EF ne surveille pas les objets
            .Include(i => i.ReportedBy)     // JOIN Users : le déclarant
            .Include(i => i.AssignedTo)     // JOIN Users : l'analyste (LEFT JOIN, il peut manquer)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }

    // VERSION NAÏVE, volontairement lente : le problème « N+1 ».
    // On a « oublié » l'Include de l'analyste et on le charge dans une boucle :
    // 1 requête pour la liste + 1 requête PAR incident.
    // Dans la console (/Incidents?naif=true) : 13 « Executed DbCommand »
    // pour 12 incidents, contre 1 seule avec GetAllAsync. Avec 10 000
    // incidents, ce serait 10 001 allers-retours vers la base.
    public async Task<List<Incident>> GetAllNaiveAsync()
    {
        var incidents = await _db.Incidents
            .Include(i => i.ReportedBy)     // gardé pour ne montrer que le problème de l'analyste
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();                                     // 1 requête

        foreach (var incident in incidents)
        {
            // Query().LoadAsync() envoie une requête à CHAQUE tour. (Un simple
            // Reference(...).LoadAsync() serait plus malin : EF Core sauterait les
            // incidents sans analyste et ceux dont l'analyste est déjà chargé.)
            await _db.Entry(incident)
                .Reference(i => i.AssignedTo)
                .Query()
                .LoadAsync();                                   // + 1 requête par incident
        }

        return incidents;
    }

    // Un incident avec ses commentaires et leurs auteurs, ou null s'il n'existe pas.
    public async Task<Incident?> GetByIdAsync(Guid id)
    {
        return await _db.Incidents
            .AsNoTracking()
            .Include(i => i.ReportedBy)
            .Include(i => i.AssignedTo)
            .Include(i => i.Comments.OrderBy(c => c.WrittenAt))  // les commentaires, par date...
                .ThenInclude(c => c.Author)                      // ...et, pour chacun, son auteur
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    // Un analyste commence à traiter l'incident. Renvoie false si l'incident n'existe pas.
    // Si la règle métier est violée, le domaine lève InvalidOperationException.
    public async Task<bool> StartWorkAsync(Guid id)
    {
        // Pas d'AsNoTracking : on va MODIFIER l'objet, EF Core doit le surveiller.
        // Include obligatoire : sans lui, AssignedTo serait null et le domaine
        // refuserait à tort (« aucun analyste »).
        var incident = await _db.Incidents
            .Include(i => i.AssignedTo)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (incident is null)
        {
            return false;
        }

        incident.StartWork();               // la règle est dans le domaine
        await _db.SaveChangesAsync();       // UPDATE Incidents SET Status = 'InProgress' ...
        return true;
    }

    // Les déclarants, pour la liste déroulante du formulaire de création.
    public async Task<List<Reporter>> GetReportersAsync()
    {
        return await _db.Users
            .OfType<Reporter>()             // WHERE Role = 'Reporter'
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    // Crée un incident et renvoie son identifiant.
    // Les règles (titre obligatoire...) sont vérifiées par le domaine, pas ici.
    public async Task<Guid> CreateAsync(string title, string? description, Severity severity, Guid reporterId)
    {
        var reporter = await _db.Users.FindAsync(reporterId);
        if (reporter is null)
        {
            throw new ArgumentException("Le déclarant indiqué n'existe pas.");
        }

        var incident = new Incident(title, description ?? "", severity, reporter);

        _db.Incidents.Add(incident);
        await _db.SaveChangesAsync();       // INSERT INTO Incidents ...
        return incident.Id;
    }
}
