using System.ComponentModel.DataAnnotations;

namespace IncidentHub.Web.Pages.Incidents;

// Deux modèles de saisie côte à côte pour la démo de l'overposting
// (« mass assignment »). Le formulaire n'affiche QUE titre et description.

// VULNÉRABLE : contient aussi Severity. Le formulaire ne l'affiche pas, mais
// la liaison de modèle remplit TOUT ce qui arrive dans la requête et qui
// correspond à une propriété. Un POST forgé avec Input.Severity=Low passe.
public class EditIncidentInputVulnerable
{
    [Display(Name = "Titre")]
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "Le titre doit contenir entre {2} et {1} caractères.")]
    public string Title { get; set; } = "";

    [Display(Name = "Description")]
    [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser {1} caractères.")]
    public string? Description { get; set; }

    public Severity Severity { get; set; }   // <- de trop !
}

// SÛRE : uniquement les champs que l'utilisateur a le droit de modifier.
// Input.Severity=Low est simplement ignoré : il n'y a aucune propriété où le mettre.
public class EditIncidentInput
{
    [Display(Name = "Titre")]
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "Le titre doit contenir entre {2} et {1} caractères.")]
    public string Title { get; set; } = "";

    [Display(Name = "Description")]
    [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser {1} caractères.")]
    public string? Description { get; set; }
}
