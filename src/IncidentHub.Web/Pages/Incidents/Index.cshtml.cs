using IncidentHub.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IncidentHub.Web.Pages.Incidents;

// URL : /Incidents (le dossier Pages/Incidents + le fichier Index).
public class IndexModel : PageModel
{
    private readonly IncidentService _service;

    // Injection de dépendances : ASP.NET Core fournit le service
    // enregistré dans Program.cs. La page ne fait jamais « new ».
    public IndexModel(IncidentService service)
    {
        _service = service;
    }

    // Ce que la vue (Index.cshtml) va afficher.
    public IReadOnlyList<Incident> Incidents { get; private set; } = [];

    // Appelé pour chaque requête GET sur /Incidents.
    // naif vient de la query string : /Incidents?naif=true (démo du N+1).
    public async Task OnGetAsync(bool naif = false)
    {
        Incidents = naif
            ? await _service.GetAllNaiveAsync()
            : await _service.GetAllAsync();
    }
}
