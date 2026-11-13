using System.Globalization;

namespace IncidentHub;

// Jeu de données de démonstration (fictif) : le même que seed.sql de la
// séance 4 (8 utilisateurs, 12 incidents, 11 commentaires).
// Chaque appel à Create() fabrique de NOUVEAUX objets.
public class DemoData
{
    public List<User> Users { get; } = new();
    public List<Incident> Incidents { get; } = new();

    public static DemoData Create()
    {
        var data = new DemoData();

        var alice = new Reporter("Alice Martin", "alice.martin@exemple.be");
        var bruno = new Reporter("Bruno Lefèvre", "bruno.lefevre@exemple.be");
        var chloe = new Reporter("Chloé Dubois", "chloe.dubois@exemple.be");
        var david = new Analyst("David Peeters", "david.peeters@exemple.be", "SOC");
        var emma = new Analyst("Emma Janssens", "emma.janssens@exemple.be", "SOC");
        var farid = new Analyst("Farid Benali", "farid.benali@exemple.be", "CERT");
        var grace = new Analyst("Grace Lambert", "grace.lambert@exemple.be", "CERT");
        var hugo = new Manager("Hugo Mertens", "hugo.mertens@exemple.be");
        data.Users.AddRange([alice, bruno, chloe, david, emma, farid, grace, hugo]);

        var i1 = data.Add("Phishing ciblant la comptabilité", "Faux e-mail de fournisseur demandant un changement d'IBAN.",
            Severity.High, IncidentStatus.Closed, "2026-10-01 08:12", alice, david);
        var i2 = data.Add("Rançongiciel sur le serveur de fichiers", "Fichiers chiffrés avec l'extension .lock et note de rançon.",
            Severity.Critical, IncidentStatus.Resolved, "2026-10-03 22:47", bruno, farid);
        data.Add("Mot de passe partagé dans Teams", "Le mot de passe d'un compte de service a été posté dans un canal.",
            Severity.Medium, IncidentStatus.Closed, "2026-10-06 10:05", chloe, emma);
        var i4 = data.Add("Connexion VPN depuis un pays inhabituel", "Connexion réussie à 3 h du matin depuis l'étranger.",
            Severity.High, IncidentStatus.InProgress, "2026-10-09 03:31", bruno, david);
        data.Add("Clé USB trouvée sur le parking", "Clé déposée à l'accueil, personne ne l'a branchée.",
            Severity.Low, IncidentStatus.Open, "2026-10-12 14:20", alice, null);
        var i6 = data.Add("Fuite de données clients sur un forum", "Un extrait de la base clients est proposé à la vente.",
            Severity.Critical, IncidentStatus.InProgress, "2026-10-15 09:58", chloe, farid);
        var i7 = data.Add("Certificat TLS expiré sur l'intranet", "Les navigateurs affichent un avertissement de sécurité.",
            Severity.Medium, IncidentStatus.Resolved, "2026-10-18 07:40", alice, emma);
        var i8 = data.Add("Compte administrateur créé sans demande", "Un compte « admin2 » est apparu dans l'annuaire.",
            Severity.Critical, IncidentStatus.Open, "2026-10-21 18:03", bruno, null);
        data.Add("Faux support informatique au téléphone", "Un appelant demande d'installer un logiciel de prise en main.",
            Severity.Medium, IncidentStatus.Open, "2026-10-24 11:16", chloe, null);
        data.Add("Scan de ports depuis le réseau invité", "Le pare-feu signale un balayage de ports.",
            Severity.Low, IncidentStatus.Closed, "2026-10-27 16:22", alice, david);
        var i11 = data.Add("Macro suspecte dans une facture", "Document Word qui demande d'activer les macros.",
            Severity.High, IncidentStatus.Open, "2026-10-29 08:50", bruno, emma);
        data.Add("Imprimante qui contacte un serveur externe", "Trafic sortant inhabituel depuis l'imprimante du 2e étage.",
            Severity.Medium, IncidentStatus.InProgress, "2026-11-02 13:37", chloe, farid);

        i1.AddComment(david, "Expéditeur bloqué, 3 boîtes nettoyées.", Date("2026-10-01 09:30"));
        i1.AddComment(hugo, "Prévoir une sensibilisation pour la comptabilité.", Date("2026-10-02 10:00"));
        i2.AddComment(farid, "Serveur isolé du réseau.", Date("2026-10-03 23:10"));
        i2.AddComment(farid, "Restauration depuis la sauvegarde du 2 octobre.", Date("2026-10-04 06:45"));
        i2.AddComment(hugo, "Déclaration au CCB à faire avant demain.", Date("2026-10-04 08:00"));
        i4.AddComment(david, "Session coupée, MFA réinitialisée.", Date("2026-10-09 07:15"));
        i6.AddComment(farid, "Échantillon vérifié : 2 400 adresses e-mail.", Date("2026-10-15 11:20"));
        i6.AddComment(hugo, "Prévenir le DPO.", Date("2026-10-15 11:45"));
        i7.AddComment(emma, "Certificat renouvelé.", Date("2026-10-18 09:05"));
        i8.AddComment(alice, "Je n'ai pas créé ce compte, je confirme.", Date("2026-10-21 19:00"));
        i11.AddComment(emma, "Pièce jointe envoyée en bac à sable.", Date("2026-10-29 09:40"));

        return data;
    }

    // Crée un incident puis rejoue les étapes de son cycle de vie
    // avec les méthodes du domaine : les règles sont respectées.
    private Incident Add(string title, string description, Severity severity, IncidentStatus status,
        string createdAt, User reportedBy, Analyst? analyst)
    {
        var incident = new Incident(title, description, severity, reportedBy, Date(createdAt));

        if (analyst is not null) incident.AssignTo(analyst);
        if (status >= IncidentStatus.InProgress) incident.StartWork();
        if (status >= IncidentStatus.Resolved) incident.Resolve();
        if (status >= IncidentStatus.Closed) incident.Close();

        Incidents.Add(incident);
        return incident;
    }

    private static DateTime Date(string text) =>
        DateTime.ParseExact(text, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
}
