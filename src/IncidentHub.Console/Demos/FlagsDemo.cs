namespace IncidentHub.Demos;

// Un enum [Flags] range plusieurs droits dans un seul nombre.
public static class FlagsDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Combiner des droits avec | ===");
        Permission droits = Permission.Read | Permission.Comment;
        Console.WriteLine($"ToString() : {droits}");
        Console.WriteLine($"Valeur     : {(int)droits} (1 + 2)");
        Console.WriteLine($"All        : {(int)Permission.All} (1 + 2 + 4 + 8)");
        Console.WriteLine();

        Console.WriteLine("=== Tester un droit ===");
        // HasFlag : lisible.
        Console.WriteLine($"HasFlag(Comment) : {droits.HasFlag(Permission.Comment)}");
        Console.WriteLine($"HasFlag(Close)   : {droits.HasFlag(Permission.Close)}");

        // & : on garde seulement les bits communs, puis on compare.
        bool peutFermer = (droits & Permission.Close) == Permission.Close;
        Console.WriteLine($"Avec & (Close)   : {peutFermer}");
        Console.WriteLine();

        Console.WriteLine("=== Droits de chaque rôle ===");
        var equipe = new List<User>
        {
            new Reporter("Alice Martin", "alice.martin@exemple.be"),
            new Analyst("Karim Diallo", "karim.diallo@exemple.be", "SOC niveau 1"),
            new Manager("Sophie Leroy", "sophie.leroy@exemple.be")
        };
        foreach (User user in equipe)
        {
            string fermer = user.Permissions.HasFlag(Permission.Close) ? "peut clôturer" : "ne peut pas clôturer";
            Console.WriteLine($"{user.Name} : {user.Permissions} ({(int)user.Permissions}), {fermer}");
        }
        Console.WriteLine();
    }
}
