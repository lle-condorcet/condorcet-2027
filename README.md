# IncidentHub — dépôt de démonstration

Dépôt support du cours **Complément Application** (Bachelier en cybersécurité).

IncidentHub est le fil rouge du cours : une plateforme de signalement
d'incidents de sécurité. Ce dépôt montre, étape par étape, comment une
classe `Incident` évolue d'une version naïve vers une version qui protège ses
données et applique des règles métier (séance 1), puis comment le modèle
s'enrichit avec l'héritage, l'abstraction, les interfaces et la composition
(séance 2).

Le code est **lu et commenté ensemble**, il n'est pas écrit en direct.
Chaque étape correspond à un tag Git.

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

## Séance 2 — POO : héritage, abstraction, interface, composition

| Tag | Ce que l'on montre |
|-----|--------------------|
| `s02-etape1-heritage` | `User` devient une classe de base. `Reporter`, `Analyst` (avec `Team`) et `Manager` en héritent : `Name` et `Email` ne sont écrits qu'une fois. Méthode `protected Identity()` utilisable par les classes dérivées, pas depuis `Program.cs`. `Incident` reçoit `AssignedTo` et `AssignTo(Analyst)` ; `StartWork()` refuse un incident sans analyste. |
| `s02-etape2-abstraction` | `User` devient `abstract` : `new User(...)` ne compile plus. Méthode abstraite `RoleName()` que chaque rôle doit fournir, et `Describe()` écrite une seule fois dans `User`. |
| `s02-etape3-interface` | Interface `INotifier` et deux implémentations `EmailNotifier` et `TeamsNotifier`. `Incident` n'en dépend pas : c'est `Program.cs` qui notifie après chaque changement d'état. |
| `s02-etape4-polymorphisme-composition` | Une `List<User>` parcourue avec `Describe()` (polymorphisme), une `List<INotifier>` pour notifier sur tous les canaux. Classe `Comment` : l'incident possède ses commentaires (composition), exposés en lecture seule, ajoutés via `AddComment()`. |

La branche `main` correspond à l'état final `s02-etape4-polymorphisme-composition`.

```bash
git diff s01-etape4-regles-metier s02-etape1-heritage
git diff s02-etape3-interface s02-etape4-polymorphisme-composition -- src/IncidentHub.Console/Incident.cs
```

## Branches « démo IA »

### Séance 1

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

### Séance 2

| Branche | Contenu | Ce que l'on observe |
|---------|---------|---------------------|
| `demo/s02-ia-diagramme` | `docs/modele-genere-par-ia.md` : diagramme de classes Mermaid obtenu avec un prompt naïf (« Fais-moi le diagramme de classes d'IncidentHub »). Part de `s02-etape4-polymorphisme-composition`. | Chaîne d'héritage abusive (`Manager` → `Analyst` → `Reporter` → `User`), `Comment` qui hérite d'`Incident`, tout en public, aucune multiplicité, statut en `String`. À comparer avec le code de `main`. |

```bash
git checkout demo/s02-ia-diagramme
# ouvrir docs/modele-genere-par-ia.md dans Rider ou sur la forge (rendu Mermaid)
```

Les fichiers de ces branches sont des sorties de référence. Avant le cours,
ils sont remplacés par la sortie réellement obtenue (en notant la date,
l'outil et le modèle utilisés).

## Conventions

Les messages de commit suivent [Conventional Commits](https://www.conventionalcommits.org/fr/) :

```
feat(incident): ajoute les transitions de statut
chore: initialise la solution
docs: complète le README
```
