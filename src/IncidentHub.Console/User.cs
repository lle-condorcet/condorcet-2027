namespace IncidentHub;

// Personne qui utilise la plateforme (par exemple pour signaler un incident).
public class User
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Le nom de l'utilisateur est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("L'e-mail de l'utilisateur est obligatoire.");
        }

        Name = name.Trim();
        Email = email.Trim();
    }
}
