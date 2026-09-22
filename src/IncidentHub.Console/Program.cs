using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

var incident = new Incident();
incident.Id = Guid.NewGuid();
incident.Title = "Hameçonnage ciblant la comptabilité";
incident.Description = "Plusieurs e-mails imitant la banque ont été reçus.";
incident.Severity = "High";
incident.Status = "Open";
incident.CreatedAt = DateTime.Now;

Afficher(incident);

// Problème : rien n'empêche d'écrire des valeurs absurdes.
incident.Status = "n'importe quoi";
incident.Severity = "très très grave";
incident.Title = "";

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
