namespace IncidentHub;

// Les setters sont privés : seule la classe Incident peut modifier ses données.
// Le constructeur garantit qu'un incident est valide dès sa création.
public class Incident
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Severity Severity { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Incident(string title, string description, Severity severity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Le titre d'un incident est obligatoire.");
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        Description = description ?? "";
        Severity = severity;
        Status = IncidentStatus.Open;
        CreatedAt = DateTime.UtcNow;
    }
}
