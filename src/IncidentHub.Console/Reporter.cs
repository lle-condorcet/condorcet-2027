namespace IncidentHub;

// Personne qui signale un incident (un employé, par exemple).
// sealed : personne ne peut hériter de Reporter. Le rôle a le moins de
// droits ; on évite qu'une sous-classe lui en ajoute en douce.
public sealed class Reporter : User
{
    public Reporter(string name, string email) : base(name, email)
    {
    }

    public override string RoleName()
    {
        return "Déclarant";
    }
}
