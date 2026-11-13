namespace IncidentHub.Web.Data;

// Contrat : « je sais retrouver des incidents ».
// Les pages dépendent de cette interface, pas d'une classe précise :
// on pourra remplacer la liste en mémoire par une base de données.
public interface IIncidentRepository
{
    IReadOnlyList<Incident> GetAll();
    Incident? GetById(Guid id);
}
