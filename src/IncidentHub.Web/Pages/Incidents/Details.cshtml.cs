using IncidentHub.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IncidentHub.Web.Pages.Incidents;

// URL : /Incidents/Details/{id}
public class DetailsModel : PageModel
{
    private readonly IIncidentRepository _repository;

    public DetailsModel(IIncidentRepository repository)
    {
        _repository = repository;
    }

    public Incident Incident { get; private set; } = null!;

    // Le paramètre id est rempli automatiquement à partir de l'URL
    // (c'est le {id:guid} de la directive @page).
    public IActionResult OnGet(Guid id)
    {
        var incident = _repository.GetById(id);
        if (incident is null)
        {
            return NotFound();   // 404 : on ne révèle rien de plus
        }

        Incident = incident;
        return Page();           // affiche Details.cshtml
    }
}
