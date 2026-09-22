# IncidentHub — dépôt de démonstration

Dépôt support du cours **Complément Application** (Bachelier en cybersécurité).

IncidentHub est le fil rouge du cours : une plateforme de signalement
d'incidents de sécurité. Ce dépôt montre, étape par étape, comment une
classe `Incident` évolue d'une version naïve vers une version qui protège ses
données et applique des règles métier.

En séance 1, le code est **lu et commenté ensemble**, il n'est pas écrit en
direct. Chaque étape correspond à un tag Git.

## Prérequis

- [.NET SDK 10](https://dotnet.microsoft.com/download) (`dotnet --version` doit afficher 10.x)
- JetBrains Rider (ou tout autre éditeur C#)
- Git

## Récupérer une étape

```bash
git clone <url-du-depot> incidenthub-demo
cd incidenthub-demo

git tag -l                                  # liste des étapes
git checkout s01-etape1-classe-incident     # se placer sur une étape
dotnet run --project src/IncidentHub.Console

git checkout main                           # revenir à la version finale
```

Pour comparer deux étapes :

```bash
git diff s01-etape1-classe-incident s01-etape2-enum-severity
```

## Séance 1 — POO : encapsulation, enum, validation

| Tag | Ce que l'on montre |
|-----|--------------------|
| `s01-etape0-solution-vide` | Structure d'une solution .NET : fichier `.slnx`, un projet console, `Program.cs` qui affiche « IncidentHub ». |
| `s01-etape1-classe-incident` | Une classe `Incident` naïve : tout est public, `Severity` et `Status` sont des `string`. Le programme fonctionne, mais on peut écrire `Status = "n'importe quoi"` sans que rien ne l'empêche. |
| `s01-etape2-enum-severity` | Les `enum` `Severity` et `IncidentStatus` : le compilateur refuse désormais une valeur inconnue. |
| `s01-etape3-encapsulation` | Setters privés et constructeur : un incident ne peut plus être créé sans titre ni sévérité. `Id` et `CreatedAt` sont fixés par la classe elle-même. |
| `s01-etape4-regles-metier` | Méthodes `StartWork()`, `Resolve()`, `Close()` : seul le cycle Open → InProgress → Resolved → Closed est autorisé. Ajout de la classe `User` (qui a signalé l'incident). Le programme montre un cas accepté puis un cas refusé. |

La branche `main` correspond à l'état final `s01-etape4-regles-metier`.

## Branches « démo IA »

Deux branches partent de `s01-etape0-solution-vide` et contiennent chacune une
classe `Incident` telle qu'un assistant IA pourrait la produire.

| Branche | Prompt utilisé | Ce que l'on observe |
|---------|----------------|---------------------|
| `demo/s01-ia-naif` | « Crée une classe C# Incident pour une plateforme de signalement d'incidents cyber » | Tout est modifiable de l'extérieur, statuts en texte libre, aucune validation, heure locale (`DateTime.Now`), `UpdateStatus(string)` accepte n'importe quoi. |
| `demo/s01-ia-structure` | Prompt structuré : contexte, contraintes (enum, setters privés, transitions autorisées, validation des entrées) | Résultat proche de l'étape 4 : le prompt précis a guidé le code. |

```bash
git checkout demo/s01-ia-naif
git diff demo/s01-ia-naif demo/s01-ia-structure -- src/IncidentHub.Console/Incident.cs
```

Le fichier `Incident.cs` de ces branches est une sortie de référence. Avant le
cours, il est remplacé par la sortie réellement obtenue (en notant la date,
l'outil et le modèle utilisés).

## Conventions

Les messages de commit suivent [Conventional Commits](https://www.conventionalcommits.org/fr/) :

```
feat(incident): ajoute les transitions de statut
chore: initialise la solution
docs: complète le README
```
