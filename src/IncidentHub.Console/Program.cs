using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

var alice = new Reporter("Alice Martin", "alice.martin@exemple.be");
var karim = new Analyst("Karim Diallo", "karim.diallo@exemple.be", "SOC niveau 1");

// La variable est de type INotifier, pas EmailNotifier.
// Remplacer par new TeamsNotifier() : le reste du programme ne change pas.
INotifier notifier = new EmailNotifier();

// Incident ne connaît pas INotifier : c'est Program qui prévient
// après chaque changement d'état. Le domaine reste simple.
var incident = new Incident(
    "Hameçonnage ciblant la comptabilité",
    "Plusieurs e-mails imitant la banque ont été reçus.",
    Severity.High,
    alice);
notifier.Notify(incident, $"nouvel incident signalé par {alice.Name}");

incident.AssignTo(karim);
notifier.Notify(incident, $"assigné à {karim.Name}");

incident.StartWork();
notifier.Notify(incident, $"statut {incident.Status}");

incident.Resolve();
notifier.Notify(incident, $"statut {incident.Status}");

incident.Close();
notifier.Notify(incident, $"statut {incident.Status}");
Console.WriteLine();

// Cas refusé, prévenu cette fois sur Teams.
INotifier teams = new TeamsNotifier();

var autreIncident = new Incident(
    "Poste infecté par un rançongiciel",
    "Fichiers chiffrés sur le poste d'accueil.",
    Severity.Critical,
    alice);
teams.Notify(autreIncident, $"nouvel incident signalé par {alice.Name}");

try
{
    autreIncident.StartWork();
}
catch (InvalidOperationException ex)
{
    teams.Notify(autreIncident, $"action refusée ({ex.Message})");
}

Console.WriteLine($"Statut inchangé : {autreIncident.Status}");
