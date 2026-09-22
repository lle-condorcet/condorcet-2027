using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

var alice = new User("Alice Martin", "alice.martin@exemple.be");

// Cas valide : l'incident suit le cycle prévu.
var incident = new Incident(
    "Hameçonnage ciblant la comptabilité",
    "Plusieurs e-mails imitant la banque ont été reçus.",
    Severity.High,
    alice);

Afficher(incident);

incident.StartWork();
Console.WriteLine($"Prise en charge -> {incident.Status}");
incident.Resolve();
Console.WriteLine($"Résolution      -> {incident.Status}");
incident.Close();
Console.WriteLine($"Clôture         -> {incident.Status}");
Console.WriteLine();

// Cas refusé : on tente de clôturer un incident qui n'a pas été traité.
var autreIncident = new Incident(
    "Poste infecté par un rançongiciel",
    "Fichiers chiffrés sur le poste d'accueil.",
    Severity.Critical,
    alice);

try
{
    autreIncident.Close();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Action refusée : {ex.Message}");
}

Console.WriteLine($"Statut inchangé : {autreIncident.Status}");

static void Afficher(Incident incident)
{
    Console.WriteLine($"Id          : {incident.Id}");
    Console.WriteLine($"Titre       : {incident.Title}");
    Console.WriteLine($"Description : {incident.Description}");
    Console.WriteLine($"Sévérité    : {incident.Severity}");
    Console.WriteLine($"Statut      : {incident.Status}");
    Console.WriteLine($"Créé le     : {incident.CreatedAt:yyyy-MM-dd HH:mm:ss} (UTC)");
    Console.WriteLine($"Signalé par : {incident.ReportedBy.Name} <{incident.ReportedBy.Email}>");
    Console.WriteLine();
}
