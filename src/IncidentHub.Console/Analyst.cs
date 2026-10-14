namespace IncidentHub;

// Membre de l'équipe sécurité qui traite les incidents.
public class Analyst : User
{
    public string Team { get; private set; }

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

    // ToString() est déclarée virtual dans object, la classe de base de tout.
    // Sans cette redéfinition, on afficherait « IncidentHub.Analyst ».
    public override string ToString()
    {
        return $"{Name} ({Team})";
    }
}
