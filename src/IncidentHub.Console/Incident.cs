namespace IncidentHub;

// Première version : tout est public et modifiable.
// Ça fonctionne, mais rien ne protège les données.
public class Incident
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    // Sévérité et statut sont du texte libre.
    public string Severity { get; set; } = "";
    public string Status { get; set; } = "";

    public DateTime CreatedAt { get; set; }
}
