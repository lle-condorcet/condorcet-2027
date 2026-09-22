// Sortie de référence — à remplacer par la sortie réelle capturée lors de la préparation (date, outil, modèle).
// Prompt (voir seance-01/prompts.md, prompt 2) :
//   Tu es développeur C# senior.
//   Crée une classe Incident (.NET 10) :
//   - pas de setter public
//   - un enum Severity : Low, Medium, High, Critical
//   - transitions autorisées uniquement :
//     Open -> InProgress -> Resolved -> Closed
//   - un titre vide est refusé
//   Explique chaque choix en une ligne.

namespace IncidentHub;

// Enum : seules les quatre sévérités prévues sont possibles.
public enum Severity
{
    Low,
    Medium,
    High,
    Critical
}

// Enum : le statut ne peut pas contenir une valeur inventée.
public enum IncidentStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}

public class Incident
{
    // private set : seule la classe peut modifier ses données.
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Severity Severity { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    // Le constructeur refuse un titre vide : un incident est valide dès sa création.
    public Incident(string title, string description, Severity severity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Le titre est obligatoire.", nameof(title));
        }

        if (!Enum.IsDefined(severity))
        {
            throw new ArgumentException("Sévérité inconnue.", nameof(severity));
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        Description = description?.Trim() ?? string.Empty;
        Severity = severity;
        Status = IncidentStatus.Open;
        CreatedAt = DateTime.UtcNow; // UTC : indépendant du fuseau horaire du serveur.
    }

    // Une méthode par transition autorisée, plutôt qu'un setter sur Status.
    public void StartWork()
    {
        EnsureStatus(IncidentStatus.Open, "démarrer le traitement");
        Status = IncidentStatus.InProgress;
    }

    public void Resolve()
    {
        EnsureStatus(IncidentStatus.InProgress, "résoudre");
        Status = IncidentStatus.Resolved;
        ResolvedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        EnsureStatus(IncidentStatus.Resolved, "clôturer");
        Status = IncidentStatus.Closed;
    }

    // Vérifie que l'incident est dans le statut attendu avant une transition.
    private void EnsureStatus(IncidentStatus expected, string action)
    {
        if (Status != expected)
        {
            throw new InvalidOperationException(
                $"Impossible de {action} l'incident : statut actuel {Status}, statut attendu {expected}.");
        }
    }
}
