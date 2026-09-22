namespace IncidentHub;

// Un incident de sécurité signalé sur la plateforme.
// Le statut ne change qu'à travers des méthodes qui vérifient les règles :
// Open -> InProgress -> Resolved -> Closed
public class Incident
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Severity Severity { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public User ReportedBy { get; private set; }

    public Incident(string title, string description, Severity severity, User reportedBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Le titre d'un incident est obligatoire.");
        }

        if (reportedBy is null)
        {
            throw new ArgumentException("Un incident doit indiquer qui l'a signalé.");
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        Description = description ?? "";
        Severity = severity;
        Status = IncidentStatus.Open;
        CreatedAt = DateTime.UtcNow;
        ReportedBy = reportedBy;
    }

    // Un analyste commence à traiter l'incident.
    public void StartWork()
    {
        ChangeStatus(from: IncidentStatus.Open, to: IncidentStatus.InProgress);
    }

    // Le problème est corrigé.
    public void Resolve()
    {
        ChangeStatus(from: IncidentStatus.InProgress, to: IncidentStatus.Resolved);
    }

    // L'incident est archivé.
    public void Close()
    {
        ChangeStatus(from: IncidentStatus.Resolved, to: IncidentStatus.Closed);
    }

    // Seul un passage à l'étape suivante est autorisé.
    private void ChangeStatus(IncidentStatus from, IncidentStatus to)
    {
        if (Status != from)
        {
            throw new InvalidOperationException(
                $"Impossible de passer à {to} : l'incident est {Status}, il devrait être {from}.");
        }

        Status = to;
    }
}
