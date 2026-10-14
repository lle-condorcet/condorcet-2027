namespace IncidentHub.Demos;

// Chaque type numérique a une taille fixe, donc des limites.
public static class NombresDemo
{
    public static void Run()
    {
        Console.WriteLine("=== Les nombres et leurs limites ===");
        Console.WriteLine($"byte   : {byte.MinValue} à {byte.MaxValue}");
        Console.WriteLine($"short  : {short.MinValue} à {short.MaxValue}");
        Console.WriteLine($"ushort : {ushort.MinValue} à {ushort.MaxValue}");
        Console.WriteLine($"int    : {int.MinValue} à {int.MaxValue}");
        Console.WriteLine($"uint   : {uint.MinValue} à {uint.MaxValue}");
        Console.WriteLine($"long   : {long.MinValue} à {long.MaxValue}");
        Console.WriteLine($"ulong  : {ulong.MinValue} à {ulong.MaxValue}");
        Console.WriteLine();

        Console.WriteLine("=== Dépassement de capacité ===");

        int max = int.MaxValue;

        // unchecked : le compteur « fait le tour » sans prévenir.
        int resultat = unchecked(max + 1);
        Console.WriteLine($"unchecked : {max} + 1 = {resultat}");

        // checked : le dépassement devient une erreur visible.
        try
        {
            int controle = checked(max + 1);
            Console.WriteLine($"checked : {controle}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"checked : OverflowException ({ex.Message})");
        }
        Console.WriteLine();

        Console.WriteLine("=== Nombres à virgule : double ou decimal ===");

        // double est stocké en binaire : 0,1 n'y tombe pas juste.
        double d = 0.1 + 0.2;
        Console.WriteLine($"double  : 0.1 + 0.2 == 0.3 ? {d == 0.3} (valeur réelle {d:R})");

        // decimal est stocké en base 10 : adapté aux montants.
        decimal m = 0.1m + 0.2m;
        Console.WriteLine($"decimal : 0.1 + 0.2 == 0.3 ? {m == 0.3m}");
        Console.WriteLine();

        Console.WriteLine("=== Coût d'un incident ===");

        decimal heures = 6.5m;
        decimal tauxHoraire = 85.40m;
        decimal cout = heures * tauxHoraire;
        Console.WriteLine($"{heures} h x {tauxHoraire} EUR/h = {cout:N2} EUR");
        Console.WriteLine();
    }
}
