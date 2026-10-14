namespace IncidentHub;

// Responsable qui supervise le traitement des incidents.
public class Manager : User
{
    public Manager(string name, string email) : base(name, email)
    {
    }

    public override string RoleName()
    {
        return "Responsable";
    }

    public override Permission Permissions => Permission.All;

    // override : le responsable remplace la signature par défaut.
    public override string Signature()
    {
        return $"— {Name}, responsable";
    }
}
