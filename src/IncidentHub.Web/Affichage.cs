namespace IncidentHub.Web;

// Petites aides d'affichage : libellés français et couleur des badges Bootstrap.
public static class Affichage
{
    public static string Libelle(Severity severity) => severity switch
    {
        Severity.Critical => "Critique",
        Severity.High => "Élevée",
        Severity.Medium => "Moyenne",
        _ => "Faible"
    };

    public static string Badge(Severity severity) => severity switch
    {
        Severity.Critical => "badge bg-danger",
        Severity.High => "badge bg-warning text-dark",
        Severity.Medium => "badge bg-info text-dark",
        _ => "badge bg-secondary"
    };

    public static string Libelle(IncidentStatus status) => status switch
    {
        IncidentStatus.Open => "Ouvert",
        IncidentStatus.InProgress => "En cours",
        IncidentStatus.Resolved => "Résolu",
        _ => "Clôturé"
    };
}
