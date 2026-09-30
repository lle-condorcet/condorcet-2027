namespace IncidentHub;

// Personne qui signale un incident (un employé, par exemple).
public class Reporter : User
{
    public Reporter(string name, string email) : base(name, email)
    {
    }

    public override string RoleName()
    {
        return "Déclarant";
    }
}
