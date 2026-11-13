namespace IncidentHub;

// Responsable qui supervise le traitement des incidents.
public class Manager : User
{
    // Pour EF Core (voir Incident).
    private Manager()
    {
    }

    public Manager(string name, string email) : base(name, email)
    {
    }

    public override string RoleName()
    {
        return "Responsable";
    }
}
