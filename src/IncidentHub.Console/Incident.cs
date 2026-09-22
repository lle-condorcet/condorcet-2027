namespace IncidentHub;

// Sévérité et statut sont maintenant des enum :
// seules les valeurs prévues sont acceptées.
public class Incident
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public Severity Severity { get; set; }
    public IncidentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
