using System.ComponentModel.DataAnnotations;

namespace IncidentHub.Web.Pages.Incidents;

// Modèle de saisie : UNIQUEMENT les champs du formulaire.
// Ce n'est pas l'entité Incident : l'utilisateur ne choisit ni l'Id,
// ni le statut, ni la date de création.
// Les attributs (DataAnnotations) décrivent les règles de saisie ;
// ASP.NET Core les vérifie au POST et remplit ModelState.
public class CreateIncidentInput
{
    [Display(Name = "Titre")]
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "Le titre doit contenir entre {2} et {1} caractères.")]
    public string Title { get; set; } = "";

    [Display(Name = "Description")]
    [StringLength(2000, ErrorMessage = "La description ne peut pas dépasser {1} caractères.")]
    public string? Description { get; set; }

    // En C#, un enum accepte n'importe quel nombre : (Severity)42 compile.
    // EnumDataType refuse toute valeur hors de Severity. (Au POST, le lieur
    // d'ASP.NET Core rejette déjà Input.Severity=42 : double sécurité.)
    [Display(Name = "Sévérité")]
    [EnumDataType(typeof(Severity), ErrorMessage = "Sévérité inconnue.")]
    public Severity Severity { get; set; } = Severity.Medium;

    // Guid? et pas Guid : un Guid vaut 00000000-... par défaut, jamais « vide »,
    // et [Required] ne pourrait pas détecter qu'on n'a rien choisi.
    [Display(Name = "Déclarant")]
    [Required(ErrorMessage = "Choisissez un déclarant.")]
    public Guid? ReporterId { get; set; }
}
