namespace IncidentHub.Blazor.Client;

// Libellés français et couleur des badges, utilisés par les deux composants.
// Le projet Client ne connaît pas le domaine : la sévérité arrive en texte.
public static class Affichage
{
    public static readonly string[] Severites = ["Low", "Medium", "High", "Critical"];

    public static string Libelle(string severity) => severity switch
    {
        "Critical" => "Critique",
        "High" => "Élevée",
        "Medium" => "Moyenne",
        _ => "Faible"
    };

    public static string Badge(string severity) => severity switch
    {
        "Critical" => "badge bg-danger",
        "High" => "badge bg-warning text-dark",
        "Medium" => "badge bg-info text-dark",
        _ => "badge bg-secondary"
    };
}
