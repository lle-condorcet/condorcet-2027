namespace IncidentHub.Web.Pages.Incidents;

// Modèle de saisie : UNIQUEMENT les champs du formulaire.
// Ce n'est pas l'entité Incident : l'utilisateur ne choisit ni l'Id,
// ni le statut, ni la date de création.
public class CreateIncidentInput
{
    public string Title { get; set; } = "";

    public string? Description { get; set; }

    public Severity Severity { get; set; } = Severity.Medium;

    public Guid ReporterId { get; set; }
}
