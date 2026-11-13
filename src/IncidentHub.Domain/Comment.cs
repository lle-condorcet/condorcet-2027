namespace IncidentHub;

// Un commentaire laissé sur un incident.
// Il appartient à l'incident (composition) : il n'existe pas sans lui.
public class Comment
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public User Author { get; private set; }
    public DateTime WrittenAt { get; private set; }

    // writtenAt est facultatif : il ne sert qu'à importer un historique
    // (données de démonstration). Sinon, c'est la date du jour.
    public Comment(string text, User author, DateTime? writtenAt = null)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Un commentaire ne peut pas être vide.");
        }

        if (author is null)
        {
            throw new ArgumentException("Un commentaire doit avoir un auteur.");
        }

        Id = Guid.NewGuid();
        Text = text.Trim();
        Author = author;
        WrittenAt = writtenAt ?? DateTime.UtcNow;
    }
}
