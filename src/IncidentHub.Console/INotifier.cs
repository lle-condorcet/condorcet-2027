namespace IncidentHub;

// Contrat : « je sais prévenir quelqu'un qu'un incident a changé ».
// L'interface dit QUOI faire, pas COMMENT : chaque classe qui
// l'implémente choisit son canal (e-mail, Teams, SMS...).
public interface INotifier
{
    void Notify(Incident incident, string message);
}
