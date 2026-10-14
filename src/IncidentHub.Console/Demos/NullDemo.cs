namespace IncidentHub.Demos;

// null = « pas d'objet ». Le csproj active <Nullable>enable</Nullable> :
// le compilateur signale chaque endroit où un null pourrait passer.
public static class NullDemo
{
    public static void Run()
    {
        var alice = new Reporter("Alice Martin", "alice.martin@exemple.be");
        var incident = new Incident("Clé USB inconnue branchée", "", Severity.Medium, alice);

        Console.WriteLine("=== Un incident non assigné ===");

        // Le « ? » de Analyst? annonce que la valeur peut manquer.
        Analyst? analyste = incident.AssignedTo;
        Console.WriteLine($"AssignedTo est null ? {analyste is null}");
        Console.WriteLine();

        Console.WriteLine("=== ?. et ?? ===");

        // ?. : si AssignedTo est null, on s'arrête et on obtient null.
        string? nom = incident.AssignedTo?.Name;
        Console.WriteLine($"Nom obtenu avec ?. : {(nom is null ? "null" : nom)}");

        // ?? : valeur de remplacement quand la gauche est null.
        string affichage = incident.AssignedTo?.Name ?? "non assigné";
        Console.WriteLine($"Avec ?? : {affichage}");

        // ??= : n'affecte que si la variable est encore null.
        string? equipe = null;
        equipe ??= "SOC niveau 1";
        equipe ??= "jamais utilisé";
        Console.WriteLine($"Avec ??= : {equipe}");
        Console.WriteLine();

        Console.WriteLine("=== Le ! (null-forgiving) ===");
        try
        {
            // Le ! fait taire le compilateur, il ne protège de rien.
            string nomForce = incident.AssignedTo!.Name;
            Console.WriteLine(nomForce);
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Plantage : {ex.GetType().Name}");
        }
        Console.WriteLine();

        Console.WriteLine("=== Texte vide ou blanc ===");
        string?[] saisies = { null, "", "   ", "Poste verrouillé" };
        foreach (string? saisie in saisies)
        {
            string etat = string.IsNullOrWhiteSpace(saisie) ? "refusé" : "accepté";
            Console.WriteLine($"[{saisie ?? "null"}] : {etat}");
        }
        Console.WriteLine();
    }
}
