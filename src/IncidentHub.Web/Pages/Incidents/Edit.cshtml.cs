using IncidentHub.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IncidentHub.Web.Pages.Incidents;

// URL : /Incidents/Edit/{id} - démo de l'overposting.
//
// REJOUER L'ATTAQUE (sur un incident « Critique », version vulnérable) :
//  a) DevTools : clic droit sur le formulaire > Inspecter, ajouter dans le <form>
//     <input name="Input.Severity" value="Low">, puis cliquer sur Enregistrer.
//  b) curl : 1. récupérer le cookie et le jeton anti-falsification
//       curl -c cookies.txt http://localhost:5199/Incidents/Edit/<id>
//       (copier la valeur du champ caché __RequestVerificationToken)
//     2. envoyer le POST forgé avec le MÊME cookie et le jeton
//       curl -b cookies.txt http://localhost:5199/Incidents/Edit/<id> \
//         -d "Input.Title=Compte administrateur créé sans demande" \
//         -d "Input.Severity=Low" -d "__RequestVerificationToken=<jeton>"
//  -> l'incident passe en « Faible ». Avec DemoVulnerable = false : la sévérité ne bouge pas.
//
// JETON ANTI-FALSIFICATION (CSRF) : Razor Pages vérifie le jeton sur chaque POST.
//   curl -i http://localhost:5199/Incidents/Edit/<id> -d "Input.Title=Sans jeton"
//   -> 400 Bad Request : un site tiers ne peut pas faire poster le navigateur
//      de la victime à sa place, car il ne connaît pas le jeton.
public class EditModel : PageModel
{
    // Interrupteur de la démo : true = version vulnérable, false = version sûre.
    private static readonly bool DemoVulnerable = true;

    private readonly IncidentService _service;

    public EditModel(IncidentService service)
    {
        _service = service;
    }

    // Ce que le formulaire affiche : titre et description, rien d'autre.
    public EditIncidentInput Input { get; set; } = new();

    public Incident Incident { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var incident = await _service.GetByIdAsync(id);
        if (incident is null)
        {
            return NotFound();
        }

        Incident = incident;
        Input = new EditIncidentInput { Title = incident.Title, Description = incident.Description };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        var incident = await _service.GetByIdAsync(id);
        if (incident is null)
        {
            return NotFound();
        }

        Incident = incident;
        Severity? newSeverity = null;

        // TryUpdateModelAsync fait la même chose que [BindProperty], mais à la demande :
        // il remplit l'objet donné avec les champs « Input.xxx » de la requête.
        if (DemoVulnerable)
        {
            // VULNÉRABLE : l'objet lié a une propriété Severity (pré-remplie avec
            // la valeur actuelle). Si la requête contient Input.Severity, elle l'écrase.
            var saisie = new EditIncidentInputVulnerable
            {
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity
            };
            await TryUpdateModelAsync(saisie, "Input");

            Input = new EditIncidentInput { Title = saisie.Title, Description = saisie.Description };
            newSeverity = saisie.Severity;
        }
        else
        {
            // SÛR : seuls Title et Description existent dans EditIncidentInput ;
            // un Input.Severity envoyé en plus est ignoré.
            Input = new EditIncidentInput { Title = incident.Title, Description = incident.Description };
            await TryUpdateModelAsync(Input, "Input");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _service.UpdateAsync(id, Input.Title, Input.Description, newSeverity);
            return RedirectToPage("Details", new { id });
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            ModelState.AddModelError(string.Empty, ex.Message);   // ex. incident clôturé
            return Page();
        }
    }
}
