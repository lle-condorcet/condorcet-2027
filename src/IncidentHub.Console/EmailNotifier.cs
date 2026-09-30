namespace IncidentHub;

// Notification par e-mail.
// Pour la démo, on affiche le message au lieu de l'envoyer vraiment.
public class EmailNotifier : INotifier
{
    public void Notify(Incident incident, string message)
    {
        Console.WriteLine($"[E-mail] {incident.Title} : {message}");
    }
}
