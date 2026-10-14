namespace IncidentHub;

// static : la classe ne s'instancie pas, il n'existe qu'un seul compteur
// partagé par tout le programme.
public static class IncidentNumbering
{
    private static int _dernierNumero = 0;

    // Renvoie INC-0001, puis INC-0002, etc.
    public static string NextNumber()
    {
        _dernierNumero++;
        return $"INC-{_dernierNumero:D4}";
    }
}
