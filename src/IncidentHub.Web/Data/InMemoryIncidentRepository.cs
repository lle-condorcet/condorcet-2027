namespace IncidentHub.Web.Data;

// Les incidents vivent dans une simple liste en mémoire.
// Enregistré en singleton : une seule instance pour toute l'application,
// donc les données sont perdues à chaque redémarrage.
public class InMemoryIncidentRepository : IIncidentRepository
{
    private readonly List<Incident> _incidents = DemoData.Create().Incidents;

    public IReadOnlyList<Incident> GetAll() =>
        _incidents.OrderByDescending(i => i.CreatedAt).ToList();

    public Incident? GetById(Guid id) =>
        _incidents.FirstOrDefault(i => i.Id == id);
}
