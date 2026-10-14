namespace IncidentHub;

// [Flags] : chaque valeur est une puissance de 2 (un bit), on peut donc
// en cumuler plusieurs dans une seule variable.
// Même principe que les droits Unix : r = 4, w = 2, x = 1, et rwx = 4 + 2 + 1 = 7.
[Flags]
public enum Permission
{
    None = 0,
    Read = 1,
    Comment = 2,
    Assign = 4,
    Close = 8,
    All = Read | Comment | Assign | Close
}
