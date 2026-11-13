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
}
