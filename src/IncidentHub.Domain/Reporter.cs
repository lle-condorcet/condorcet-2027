namespace IncidentHub;

// Personne qui signale un incident.
public class Reporter : User
{
    // Pour EF Core (voir Incident).
    private Reporter()
    {
    }

    public Reporter(string name, string email) : base(name, email)
    {
    }

    public override string RoleName()
    {
        return "Déclarant";
    }
}
