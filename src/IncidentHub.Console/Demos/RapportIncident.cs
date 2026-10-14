// Sortie de référence — à remplacer par la sortie réelle capturée lors de la
// préparation (date, outil, modèle).
// Prompt : "Corrige les avertissements de nullabilité de ce fichier."
//
// Code d'un étudiant, puis « corrigé » par l'IA : les avertissements ont
// disparu parce que l'IA a ajouté des ! au lieu de gérer le cas null.

namespace IncidentHub.Demos;

public static class RapportIncident
{
    public static void Afficher(Incident incident)
    {
        Console.WriteLine("=== Rapport d'incident ===");
        Console.WriteLine($"Titre      : {incident.Title}");
        Console.WriteLine($"Sévérité   : {incident.Severity}");

        // Correction IA : « ! » ajouté. Si personne n'est assigné -> plantage.
        Console.WriteLine($"Analyste   : {incident.AssignedTo!.Name}");
        Console.WriteLine($"Équipe     : {incident.AssignedTo!.Team}");

        // Correction IA : « ! » ajouté. Sans commentaire -> plantage.
        Comment? dernier = incident.Comments.LastOrDefault();
        Console.WriteLine($"Dernier commentaire : {dernier!.Text}");
        Console.WriteLine($"Écrit par           : {dernier!.Author.Name}");
        Console.WriteLine();
    }
}
