using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

// L'incident est créé en une ligne, avec les informations obligatoires.
// Id, statut et date de création sont fixés par la classe.
var incident = new Incident(
    "Hameçonnage ciblant la comptabilité",
    "Plusieurs e-mails imitant la banque ont été reçus.",
    Severity.High);

Afficher(incident);

// Ces lignes ne compilent plus : les setters sont privés.
// incident.Title = "";
// incident.Status = IncidentStatus.Closed;
// incident.CreatedAt = new DateTime(1990, 1, 1);

// Un titre vide est refusé dès la création.
try
{
    var incidentSansTitre = new Incident("", "Pas de titre", Severity.Low);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Création refusée : {ex.Message}");
}

static void Afficher(Incident incident)
{
    Console.WriteLine($"Id          : {incident.Id}");
    Console.WriteLine($"Titre       : {incident.Title}");
    Console.WriteLine($"Description : {incident.Description}");
    Console.WriteLine($"Sévérité    : {incident.Severity}");
    Console.WriteLine($"Statut      : {incident.Status}");
    Console.WriteLine($"Créé le     : {incident.CreatedAt:yyyy-MM-dd HH:mm:ss} (UTC)");
    Console.WriteLine();
}
