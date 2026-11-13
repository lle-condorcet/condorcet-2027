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

    // Bouton « Prendre en charge » : POST /Incidents/Details/{id}?handler=StartWork
    // Le nom du handler vient du nom de la méthode : OnPost + StartWork + Async.
    public async Task<IActionResult> OnPostStartWorkAsync(Guid id)
    {
        try
        {
            if (!await _service.StartWorkAsync(id))
            {
                return NotFound();
            }

            return RedirectToPage(new { id });   // Post-Redirect-Get
        }
        catch (InvalidOperationException ex)
        {
            // Refus du domaine (pas d'analyste, mauvais statut) : on l'affiche proprement.
            ModelState.AddModelError(string.Empty, ex.Message);
            return await OnGetAsync(id);         // recharge l'incident et réaffiche la page
        }
    }
}
