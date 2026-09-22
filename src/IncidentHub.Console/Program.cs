using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

var incident = new Incident();
incident.Id = Guid.NewGuid();
incident.Title = "Hameçonnage ciblant la comptabilité";
incident.Description = "Plusieurs e-mails imitant la banque ont été reçus.";
incident.Severity = Severity.High;
incident.Status = IncidentStatus.Open;
incident.CreatedAt = DateTime.Now;

Afficher(incident);

// Cette ligne ne compile plus : "n'importe quoi" n'est pas un IncidentStatus.
// incident.Status = "n'importe quoi";

// Mais d'autres problèmes restent possibles :
// titre vide, statut qui revient en arrière, date modifiable...
incident.Title = "";
incident.Status = IncidentStatus.Closed;
incident.Status = IncidentStatus.Open;
incident.CreatedAt = new DateTime(1990, 1, 1);

Console.WriteLine("Après modification sans contrôle :");
Afficher(incident);

static void Afficher(Incident incident)
{
    Console.WriteLine($"Id          : {incident.Id}");
    Console.WriteLine($"Titre       : {incident.Title}");
    Console.WriteLine($"Description : {incident.Description}");
    Console.WriteLine($"Sévérité    : {incident.Severity}");
    Console.WriteLine($"Statut      : {incident.Status}");
    Console.WriteLine($"Créé le     : {incident.CreatedAt}");
    Console.WriteLine();
}
