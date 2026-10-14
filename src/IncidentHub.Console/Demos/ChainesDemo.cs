using System.Globalization;
using System.Text;

namespace IncidentHub.Demos;

// Construire, formater et transformer du texte.
public static class ChainesDemo
{
    public static void Run()
    {
        string titre = "Hameçonnage";
        Severity severite = Severity.High;
        decimal cout = 1234.5m;
        DateTime date = new DateTime(2026, 10, 14, 9, 5, 0);

        Console.WriteLine("=== Interpolation et formats ===");
        Console.WriteLine($"Incident {titre} ({severite})");
        Console.WriteLine(string.Format("{0} : {1}", titre, severite));
        // Sans culture précisée, le format suit les réglages du poste.
        Console.WriteLine(string.Format("Coût : {0:N2} EUR", cout));
        Console.WriteLine($"Signalé le {date:dd/MM/yyyy HH:mm}");
        Console.WriteLine();

        Console.WriteLine("=== Culture : virgule ou point ===");
        var belge = CultureInfo.GetCultureInfo("fr-BE");
        var americaine = CultureInfo.GetCultureInfo("en-US");
        Console.WriteLine($"fr-BE : {cout.ToString("N2", belge)}");
        Console.WriteLine($"en-US : {cout.ToString("N2", americaine)}");

        // Un fichier ou une API envoie « 3.5 » : on lit sans dépendre du poste.
        double duree = double.Parse("3.5", CultureInfo.InvariantCulture);
        Console.WriteLine($"double.Parse(\"3.5\", InvariantCulture) = {duree}");
        Console.WriteLine();

        Console.WriteLine("=== Méthodes utiles ===");
        string brut = "  Phishing : faux e-mail de la banque  ";
        string propre = brut.Trim();
        Console.WriteLine($"Trim              : [{propre}]");
        Console.WriteLine($"ToUpperInvariant  : {propre.ToUpperInvariant()}");
        Console.WriteLine($"Contains(banque)  : {propre.Contains("banque")}");
        Console.WriteLine($"StartsWith(Phish) : {propre.StartsWith("Phish")}");
        Console.WriteLine($"Replace           : {propre.Replace("Phishing", "Hameçonnage")}");

        string[] parties = propre.Split(" : ");
        Console.WriteLine($"Split             : {parties.Length} parties, [{parties[0]}] et [{parties[1]}]");
        Console.WriteLine($"string.Join       : {string.Join(" | ", parties)}");
        Console.WriteLine($"PadLeft           : [{"42".PadLeft(6, '0')}]");
        Console.WriteLine($"Substring(0, 8)   : {propre.Substring(0, 8)}");
        Console.WriteLine();

        Console.WriteLine("=== Une chaîne ne change jamais ===");
        string original = "incident ouvert";
        string majuscules = original.ToUpperInvariant();   // nouvelle chaîne
        Console.WriteLine($"original   : {original}");
        Console.WriteLine($"majuscules : {majuscules}");
        Console.WriteLine();

        Console.WriteLine("=== StringBuilder pour un rapport ===");
        var rapport = new StringBuilder();
        rapport.AppendLine("Rapport du jour");
        string[] incidents = { "Hameçonnage", "Rançongiciel", "Clé USB inconnue" };
        for (int i = 0; i < incidents.Length; i++)
        {
            rapport.AppendLine($"  {i + 1}. {incidents[i]}");
        }
        rapport.Append($"Total : {incidents.Length} incident(s)");
        Console.WriteLine(rapport.ToString());
        Console.WriteLine();

        Console.WriteLine("=== Chaînes verbatim et raw ===");
        // @ : le \ n'est plus un caractère d'échappement.
        string chemin = @"C:\logs\incidents.txt";
        Console.WriteLine(chemin);

        // """ (C# 11) : texte sur plusieurs lignes, guillemets compris.
        string json = """
            {
              "titre": "Hameçonnage",
              "severite": "High"
            }
            """;
        Console.WriteLine(json);
        Console.WriteLine();

        // Sécurité : ne jamais construire une requête SQL par interpolation (injection).
        // string sql = $"SELECT * FROM Incidents WHERE Title = '{saisieUtilisateur}'";
        // On utilise des requêtes paramétrées (vu plus tard avec la base de données).
    }
}
