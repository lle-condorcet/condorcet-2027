using IncidentHub.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IncidentHub.Web.Pages.Incidents;

// URL : /Incidents/Create
public class CreateModel : PageModel
{
    private readonly IncidentService _service;

    public CreateModel(IncidentService service)
    {
        _service = service;
    }

    // [BindProperty] : au POST, ASP.NET Core remplit Input avec les champs
    // envoyés par le formulaire (Input.Title, Input.Severity...).
    [BindProperty]
    public CreateIncidentInput Input { get; set; } = new();

    // Options de la liste déroulante des déclarants.
    public SelectList Reporters { get; private set; } = null!;

    // GET : affiche le formulaire vide.
    public async Task OnGetAsync()
    {
        await LoadReportersAsync();
    }

    // POST : le formulaire a été envoyé.
    public async Task<IActionResult> OnPostAsync()
    {
        // Validation côté serveur : ModelState contient les erreurs des DataAnnotations
        // (et celles de liaison, ex. Input.Severity=abc). Si une règle n'est pas
        // respectée, on réaffiche le formulaire avec les messages, sans rien créer.
        if (!ModelState.IsValid)
        {
            await LoadReportersAsync();   // la liste déroulante doit être rechargée
            return Page();
        }

        // ReporterId n'est pas null ici : [Required] l'a vérifié.
        var id = await _service.CreateAsync(Input.Title, Input.Description, Input.Severity,
            Input.ReporterId ?? Guid.Empty);

        // Post-Redirect-Get : on répond par une redirection (302) vers la page
        // de détail. Un F5 rechargera cette page (GET), sans recréer l'incident.
        return RedirectToPage("Details", new { id });
    }

    private async Task LoadReportersAsync()
    {
        var reporters = await _service.GetReportersAsync();
        Reporters = new SelectList(reporters, nameof(Reporter.Id), nameof(Reporter.Name));
    }
}
