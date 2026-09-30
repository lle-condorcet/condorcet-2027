namespace IncidentHub;

// Responsable qui supervise le traitement des incidents.
public class Manager : User
{
    public Manager(string name, string email) : base(name, email)
    {
    }

    public string Describe()
    {
        return $"Responsable : {Identity()}";
    }
}
