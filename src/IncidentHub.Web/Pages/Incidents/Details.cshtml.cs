using IncidentHub.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IncidentHub.Web.Pages.Incidents;

// URL : /Incidents/Details/{id}
public class DetailsModel : PageModel
{
    private readonly IncidentService _service;

    public DetailsModel(IncidentService service)
    {
        _service = service;
    }

    public Incident Incident { get; private set; } = null!;

    // Le paramètre id est rempli automatiquement à partir de l'URL
    // (c'est le {id:guid} de la directive @page).
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var incident = await _service.GetByIdAsync(id);
        if (incident is null)
        {
            return NotFound();   // 404 : on ne révèle rien de plus
        }

        Incident = incident;
        return Page();           // affiche Details.cshtml
    }
}
