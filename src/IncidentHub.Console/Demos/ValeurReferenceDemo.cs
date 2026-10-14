namespace IncidentHub.Demos;

// Type valeur ou type référence : que copie-t-on vraiment avec « = » ?
public static class ValeurReferenceDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Type valeur : on copie la valeur ===");

        int a = 10;
        int b = a;   // b reçoit une copie du nombre 10
        b = 99;      // seule b change
        Console.WriteLine($"a = {a}, b = {b}");
        Console.WriteLine();

        Console.WriteLine("=== Type référence : on copie l'adresse ===");

        var karim = new Analyst("Karim Diallo", "karim.diallo@exemple.be", "SOC niveau 1");
        var incident = new Incident("Mot de passe partagé par e-mail", "", Severity.Medium, karim);
        incident.AddComment(karim, "Premier commentaire.");

        Comment premier = incident.Comments[0];
        Comment memeCommentaire = premier;   // copie de la référence, pas de l'objet
        Console.WriteLine($"Même objet ? {ReferenceEquals(premier, memeCommentaire)}");

        // Un objet simple modifiable rend l'effet visible.
        var original = new FicheContact { Nom = "Alice" };
        var copie = original;   // les deux variables désignent la même fiche
        copie.Nom = "Bob";
        Console.WriteLine($"original.Nom = {original.Nom}, copie.Nom = {copie.Nom}");
        Console.WriteLine();

        Console.WriteLine("=== struct contre class ===");

        var pointA = new Coordonnees { X = 1, Y = 2 };
        var pointB = pointA;   // struct : copie complète
        pointB.X = 50;
        Console.WriteLine($"struct : pointA.X = {pointA.X}, pointB.X = {pointB.X}");

        var ficheA = new FicheContact { Nom = "Sophie" };
        var ficheB = ficheA;   // class : même objet
        ficheB.Nom = "Karim";
        Console.WriteLine($"class  : ficheA.Nom = {ficheA.Nom}, ficheB.Nom = {ficheB.Nom}");
        Console.WriteLine();
    }
}

// struct : type valeur, copié en entier à chaque affectation.
public struct Coordonnees
{
    public int X;
    public int Y;
}

// class : type référence, l'affectation partage le même objet.
public class FicheContact
{
    public string Nom { get; set; } = "";
}
