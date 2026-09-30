namespace IncidentHub;

// Un commentaire laissé sur un incident.
// Il appartient à l'incident (composition) : il n'existe pas sans lui.
public class Comment
{
    public string Text { get; private set; }
    public User Author { get; private set; }
    public DateTime WrittenAt { get; private set; }

    public Comment(string text, User author)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Un commentaire ne peut pas être vide.");
        }

        if (author is null)
        {
            throw new ArgumentException("Un commentaire doit avoir un auteur.");
        }

        Text = text.Trim();
        Author = author;
        WrittenAt = DateTime.UtcNow;
    }
}
