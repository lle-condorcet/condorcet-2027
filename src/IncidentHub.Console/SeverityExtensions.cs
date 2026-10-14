namespace IncidentHub;

// Méthode d'extension : le « this » devant le paramètre permet d'écrire
// severity.ToLabel() comme si la méthode faisait partie de l'enum.
public static class SeverityExtensions
{
    public static string ToLabel(this Severity severity)
    {
        return severity switch
        {
            Severity.Low => "Faible",
            Severity.Medium => "Moyenne",
            Severity.High => "Haute",
            Severity.Critical => "Critique",
            _ => severity.ToString()
        };
    }
}
