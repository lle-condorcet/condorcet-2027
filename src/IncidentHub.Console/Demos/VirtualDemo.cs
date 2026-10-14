namespace IncidentHub.Demos;

// virtual / override : la méthode réellement appelée dépend de l'objet,
// pas du type de la variable.
public static class VirtualDemo
{
    public static void Run()
    {
        var alice = new Reporter("Alice Martin", "alice.martin@exemple.be");
        var karim = new Analyst("Karim Diallo", "karim.diallo@exemple.be", "SOC niveau 1");
        var sophie = new Manager("Sophie Leroy", "sophie.leroy@exemple.be");

        Console.WriteLine("=== Signature() : appel polymorphe ===");
        var equipe = new List<User> { alice, karim, sophie };
        foreach (User user in equipe)
        {
            // Reporter et Analyst gardent la version de User, Manager a la sienne.
            Console.WriteLine($"{user.GetType().Name,-8} : {user.Signature()}");
        }
        Console.WriteLine();

        Console.WriteLine("=== ToString() redéfinie ===");
        Console.WriteLine($"Analyst (redéfinie)   : {karim}");
        Console.WriteLine($"Manager (par défaut)  : {sophie}");
        Console.WriteLine();

        // sealed : la ligne suivante ne compile pas.
        // class SuperReporter : Reporter { }
    }
}
