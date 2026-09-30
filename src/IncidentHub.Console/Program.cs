using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

// Trois profils, trois classes qui héritent toutes de User.
var alice = new Reporter("Alice Martin", "alice.martin@exemple.be");
var karim = new Analyst("Karim Diallo", "karim.diallo@exemple.be", "SOC niveau 1");
var sophie = new Manager("Sophie Leroy", "sophie.leroy@exemple.be");

Console.WriteLine(alice.Describe());
Console.WriteLine(karim.Describe());
Console.WriteLine(sophie.Describe());
Console.WriteLine();

// Name et Email viennent de User : Analyst en hérite sans les réécrire.
Console.WriteLine($"Karim s'appelle {karim.Name} et fait partie de l'équipe {karim.Team}.");
Console.WriteLine();

// Identity() est protected : la ligne suivante ne compile pas.
// Console.WriteLine(karim.Identity());

// Cas valide : l'incident est assigné puis suit le cycle prévu.
var incident = new Incident(
    "Hameçonnage ciblant la comptabilité",
    "Plusieurs e-mails imitant la banque ont été reçus.",
    Severity.High,
    alice);

Afficher(incident);

incident.AssignTo(karim);
Console.WriteLine($"Assigné à       -> {incident.AssignedTo?.Name}");
incident.StartWork();
Console.WriteLine($"Prise en charge -> {incident.Status}");
incident.Resolve();
Console.WriteLine($"Résolution      -> {incident.Status}");
incident.Close();
Console.WriteLine($"Clôture         -> {incident.Status}");
Console.WriteLine();

// Cas refusé : on veut démarrer un incident que personne n'a pris en charge.
var autreIncident = new Incident(
    "Poste infecté par un rançongiciel",
    "Fichiers chiffrés sur le poste d'accueil.",
    Severity.Critical,
    alice);

try
{
    autreIncident.StartWork();
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
