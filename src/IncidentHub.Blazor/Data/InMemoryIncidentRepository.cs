namespace IncidentHub.Blazor.Data;

// Les incidents de démonstration, en mémoire (comme dans IncidentHub.Web).
// Enregistré en singleton dans Program.cs.
public class InMemoryIncidentRepository
{
    private readonly List<Incident> _incidents = DemoData.Create().Incidents;

    public IReadOnlyList<Incident> GetAll() =>
        _incidents.OrderByDescending(i => i.CreatedAt).ToList();
}
