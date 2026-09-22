// Sortie de référence — à remplacer par la sortie réelle capturée lors de la préparation (date, outil, modèle).
// Prompt : "Crée une classe C# Incident pour une plateforme de signalement d'incidents cyber"

using System;
using System.Collections.Generic;

namespace IncidentHub
{
    /// <summary>
    /// Représente un incident de cybersécurité signalé sur la plateforme.
    /// </summary>
    public class Incident
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Sévérité de l'incident (Low, Medium, High, Critical).
        /// </summary>
        public string Severity { get; set; } = "Low";

        /// <summary>
        /// Statut de l'incident (Open, In Progress, Resolved, Closed).
        /// </summary>
        public string Status { get; set; } = "Open";

        public string Category { get; set; } = string.Empty;
        public string ReportedBy { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public List<string> AffectedSystems { get; set; } = new List<string>();

        public Incident()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Incident(string title, string description, string severity, string reportedBy)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Severity = severity;
            ReportedBy = reportedBy;
            Status = "Open";
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Met à jour le statut de l'incident.
        /// </summary>
        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;

            if (newStatus == "Resolved")
            {
                ResolvedAt = DateTime.Now;
            }
        }

        public void AssignTo(string analyst)
        {
            AssignedTo = analyst;
            Status = "In Progress";
        }

        public override string ToString()
        {
            return $"[{Severity}] {Title} - {Status} (créé le {CreatedAt})";
        }
    }
}
