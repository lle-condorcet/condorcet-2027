namespace IncidentHub;

// Personne qui utilise la plateforme.
// abstract : on ne crée jamais un « User » tout court,
// toujours un Reporter, un Analyst ou un Manager.
public abstract class User
{
    // Identifiant unique : nécessaire pour retrouver l'utilisateur (URL, base de données).
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    // protected : seules les classes dérivées appellent ce constructeur.
    protected User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Le nom de l'utilisateur est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("L'e-mail de l'utilisateur est obligatoire.");
        }

        Id = Guid.NewGuid();
        Name = name.Trim();
        Email = email.Trim();
    }

    // Chaque classe dérivée est obligée de dire quel est son rôle.
    public abstract string RoleName();

    // Écrite une seule fois ici, elle utilise le RoleName() de la classe réelle.
    public string Describe()
    {
        return $"{RoleName()} : {Identity()}";
    }

    protected string Identity()
    {
        return $"{Name} <{Email}>";
    }
}
