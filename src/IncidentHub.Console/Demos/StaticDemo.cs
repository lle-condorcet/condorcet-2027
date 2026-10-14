namespace IncidentHub.Demos;

// Membres static : on les appelle sur la classe, sans créer d'objet.
public static class StaticDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Un compteur static partagé ===");
        Console.WriteLine(IncidentNumbering.NextNumber());
        Console.WriteLine(IncidentNumbering.NextNumber());
        Console.WriteLine(IncidentNumbering.NextNumber());
        Console.WriteLine();

        Console.WriteLine("=== Méthode d'extension ToLabel() ===");
        foreach (Severity severite in Enum.GetValues<Severity>())
        {
            Console.WriteLine($"{severite} -> {severite.ToLabel()}");
        }
        Console.WriteLine();

        Console.WriteLine("=== Méthodes static du framework ===");
        // Math et DateTime s'utilisent sans new.
        int incidentsLundi = 4;
        int incidentsMardi = 7;
        Console.WriteLine($"Math.Max({incidentsLundi}, {incidentsMardi}) = {Math.Max(incidentsLundi, incidentsMardi)}");
        Console.WriteLine($"DateTime.UtcNow = {DateTime.UtcNow:dd/MM/yyyy HH:mm} (UTC)");
        Console.WriteLine();
    }
}
