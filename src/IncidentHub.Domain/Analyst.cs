namespace IncidentHub;

// Membre de l'équipe sécurité qui traite les incidents.
public class Analyst : User
{
    public string Team { get; private set; }

    // Pour EF Core (voir Incident).
    private Analyst()
    {
        Team = "";
    }

    public Analyst(string name, string email, string team) : base(name, email)
    {
        if (string.IsNullOrWhiteSpace(team))
        {
            throw new ArgumentException("L'équipe de l'analyste est obligatoire.");
        }

        Team = team.Trim();
    }

    public override string RoleName()
    {
        return $"Analyste ({Team})";
    }
}
