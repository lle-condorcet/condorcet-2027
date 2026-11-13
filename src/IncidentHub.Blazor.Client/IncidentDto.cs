namespace IncidentHub.Blazor.Client;

// DTO (Data Transfer Object) : ce que l'API /api/incidents envoie au navigateur.
// On choisit ce qui sort : pas d'e-mail, pas de description, pas de commentaires.
// Partagé : le serveur le remplit, le composant WebAssembly le lit.
public record IncidentDto(
    Guid Id,
    string Title,
    string Severity,
    string Status,
    string? AnalystName,
    DateTime CreatedAt);
