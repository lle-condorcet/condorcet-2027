namespace IncidentHub;

// Notification dans un canal Teams.
// Pour la démo, on affiche le message au lieu de l'envoyer vraiment.
public class TeamsNotifier : INotifier
{
    public void Notify(Incident incident, string message)
    {
        Console.WriteLine($"[Teams] {incident.Title} : {message}");
    }
}
