using IncidentHub.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IncidentHub.Web.Pages.Incidents;

// URL : /Incidents (le dossier Pages/Incidents + le fichier Index).
public class IndexModel : PageModel
{
    private readonly IIncidentRepository _repository;

    // Injection de dépendances : ASP.NET Core fournit le dépôt
    // enregistré dans Program.cs. La page ne fait jamais « new ».
    public IndexModel(IIncidentRepository repository)
    {
        _repository = repository;
    }

    // Ce que la vue (Index.cshtml) va afficher.
    public IReadOnlyList<Incident> Incidents { get; private set; } = [];

    // Appelé pour chaque requête GET sur /Incidents.
    public void OnGet()
    {
        Incidents = _repository.GetAll();
    }
}
