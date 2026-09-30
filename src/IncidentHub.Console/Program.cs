using IncidentHub;

Console.WriteLine("IncidentHub");
Console.WriteLine();

var alice = new Reporter("Alice Martin", "alice.martin@exemple.be");
var karim = new Analyst("Karim Diallo", "karim.diallo@exemple.be", "SOC niveau 1");
var sophie = new Manager("Sophie Leroy", "sophie.leroy@exemple.be");

// Polymorphisme : une seule liste de User, trois classes différentes.
// Describe() appelle le RoleName() de la classe réelle de chaque objet.
var equipe = new List<User> { alice, karim, sophie };

Console.WriteLine("Utilisateurs :");
foreach (User user in equipe)
{
    Console.WriteLine($"  {user.Describe()}");
}
Console.WriteLine();

// Même idée pour les notifications : Program ne sait pas quel canal
// se cache derrière chaque INotifier. Ajouter un canal = ajouter un élément.
var notifiers = new List<INotifier> { new EmailNotifier(), new TeamsNotifier() };

// Cas valide : signalement -> assignation -> traitement -> résolution -> clôture.
var incident = new Incident(
    "Hameçonnage ciblant la comptabilité",
    "Plusieurs e-mails imitant la banque ont été reçus.",
    Severity.High,
    alice);
NotifierTous(notifiers, incident, $"nouvel incident signalé par {alice.Name}");

incident.AssignTo(karim);
NotifierTous(notifiers, incident, $"assigné à {karim.Name}");

incident.StartWork();
incident.AddComment(karim, "Expéditeur bloqué sur la passerelle mail.");
NotifierTous(notifiers, incident, $"statut {incident.Status}");

incident.Resolve();
incident.AddComment(sophie, "Prévoir un rappel de sensibilisation pour le service.");
NotifierTous(notifiers, incident, $"statut {incident.Status}");

incident.Close();
NotifierTous(notifiers, incident, $"statut {incident.Status}");
Console.WriteLine();

Console.WriteLine($"Commentaires sur « {incident.Title} » :");
foreach (Comment comment in incident.Comments)
{
    Console.WriteLine($"  {comment.WrittenAt:HH:mm} {comment.Author.Name} : {comment.Text}");
}
Console.WriteLine();

// La liste est en lecture seule : la ligne suivante ne compile pas.
// incident.Comments.Add(new Comment("Texte", karim));

// Cas refusés.
var autreIncident = new Incident(
    "Poste infecté par un rançongiciel",
    "Fichiers chiffrés sur le poste d'accueil.",
    Severity.Critical,
    alice);
NotifierTous(notifiers, autreIncident, $"nouvel incident signalé par {alice.Name}");

try
{
    autreIncident.StartWork();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Action refusée : {ex.Message}");
}

try
{
    autreIncident.AddComment(alice, "   ");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Action refusée : {ex.Message}");
}

Console.WriteLine($"Statut inchangé : {autreIncident.Status}, {autreIncident.Comments.Count} commentaire(s)");

static void NotifierTous(List<INotifier> notifiers, Incident incident, string message)
{
    foreach (INotifier notifier in notifiers)
    {
        notifier.Notify(incident, message);
    }
}
