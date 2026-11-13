# IncidentHub — dépôt de démonstration

Dépôt support du cours **Complément Application** (Bachelier en cybersécurité).

IncidentHub est le fil rouge du cours : une plateforme de signalement
d'incidents de sécurité. Ce dépôt montre, étape par étape, comment une
classe `Incident` évolue d'une version naïve vers une version qui protège ses
données et applique des règles métier (séance 1), puis comment le modèle
s'enrichit avec l'héritage, l'abstraction, les interfaces et la composition
(séance 2), et enfin les bases du langage C# : types, null, chaînes, static,
virtual et enum `[Flags]` (séance 3). Le domaine passe ensuite sur le web :
Razor Pages et Blazor (séance 5), base de données avec EF Core (séance 6),
formulaires et validation, jusqu'à une attaque par overposting (séance 7).

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

La séance 2 se termine au tag `s02-etape4-polymorphisme-composition`.

```bash
git diff s01-etape4-regles-metier s02-etape1-heritage
git diff s02-etape3-interface s02-etape4-polymorphisme-composition -- src/IncidentHub.Console/Incident.cs
```

## Séance 3 — Le langage C# : types, null, chaînes, static, virtual

Chaque thème a sa démo dans `src/IncidentHub.Console/Demos/`, appelée à la
fin de `Program.cs` et affichée sous un titre `=== ... ===`.

| Tag | Ce que l'on montre |
|-----|--------------------|
| `s03-etape1-valeur-reference` | `ValeurReferenceDemo` : copier un `int` donne une valeur indépendante ; copier une référence (un `Comment`, une `FicheContact`) donne deux variables vers le même objet. `struct Coordonnees` (copiée) contre `class FicheContact` (partagée). |
| `s03-etape2-nombres` | `NombresDemo` : limites de `byte` à `ulong`, `int.MaxValue + 1` qui devient négatif en `unchecked`, `OverflowException` en `checked`, `0.1 + 0.2 == 0.3` faux en `double` et vrai en `decimal`, coût d'un incident en `decimal`. |
| `s03-etape3-null` | `NullDemo` (avec `<Nullable>enable</Nullable>`) : `Analyst?` d'un incident non assigné, `?.`, `??`, `??=`, le `!` qui fait taire le compilateur et plante quand même, `string.IsNullOrWhiteSpace`. |
| `s03-etape4-chaines` | `ChainesDemo` : interpolation, `string.Format`, formats `N2` et date, culture `fr-BE` contre `en-US`, `double.Parse` en `InvariantCulture`, méthodes usuelles (`Trim`, `Split`, `Replace`...), immuabilité, `StringBuilder`, chaînes `@"..."` et `"""..."""`. Rappel : jamais de SQL construit par interpolation. |
| `s03-etape5-static` | Classe statique `IncidentNumbering` (`INC-0001`, `INC-0002`...), méthode d'extension `Severity.ToLabel()` (« Haute », « Critique »...). `StaticDemo` utilise aussi `Math.Max` et `DateTime.UtcNow`. |
| `s03-etape6-virtual-override` | `User.Signature()` est `virtual`, `Manager` la redéfinit avec `override`. `Analyst` redéfinit `ToString()`. `Reporter` devient `sealed`. `VirtualDemo` : appel polymorphe sur une `List<User>`. |
| `s03-etape7-flags` | Enum `[Flags] Permission` (Read, Comment, Assign, Close, All) et propriété abstraite `Permissions` sur `User`. `FlagsDemo` : `\|` pour combiner, `HasFlag` et `&` pour tester, valeur numérique et `ToString()` (« Read, Comment »). Parallèle avec les droits Unix `rwx` (4 + 2 + 1 = 7). |

```bash
git diff s03-etape5-static s03-etape6-virtual-override -- src/IncidentHub.Console/User.cs
```

## Séance 5 — Le web : Razor Pages et Blazor

Le domaine du Livrable 1 est extrait dans la bibliothèque
`src/IncidentHub.Domain` (namespace `IncidentHub`), avec le jeu de données
`DemoData` : les mêmes 8 utilisateurs et 12 incidents que `seed.sql` (séance 4).

```bash
dotnet run --project src/IncidentHub.Web --urls http://localhost:5199
# http://localhost:5199/Incidents
```

| Tag | Ce que l'on montre |
|-----|--------------------|
| `s05-etape1-projet-web` | Projet `IncidentHub.Web` (gabarit `webapp` nettoyé). `Program.cs` commenté ligne à ligne : les services, puis le pipeline dans l'ordre (gestion d'erreurs, HTTPS, fichiers statiques, routage, autorisation, `MapRazorPages`). `_Layout` avec le menu Accueil / Incidents. |
| `s05-etape2-liste` | `IIncidentRepository` et `InMemoryIncidentRepository` enregistré en singleton (injection de dépendances). Page `Incidents/Index` : `PageModel`, `@foreach`, badge de sévérité, « Non assigné », liens `asp-page` / `asp-route-id`. |
| `s05-etape3-detail` | Page `Incidents/Details` avec `@page "{id:guid}"` : `OnGet` renvoie `NotFound()` (404) si l'id est inconnu ; `/Incidents/Details/abc` est refusé par la route. Affichage des commentaires. |
| `s05-etape4-blazor` | Projets `IncidentHub.Blazor` et `IncidentHub.Blazor.Client` (`dotnet new blazor --interactivity Auto`). La même liste filtrable par sévérité en deux versions : `/incidents-serveur` (`InteractiveServer`, lit le dépôt directement) et `/incidents-wasm` (`InteractiveWebAssembly`, tourne dans le navigateur et appelle la minimal API `GET /api/incidents` qui renvoie des DTO). Ce qui part dans le navigateur est lisible par l'utilisateur. |

```bash
dotnet run --project src/IncidentHub.Blazor --urls http://localhost:5198
# http://localhost:5198/incidents-wasm  (DevTools > Réseau : api/incidents, _framework/*.wasm)
```

## Séance 6 — Les données avec EF Core

La base SQLite `incidenthub-web.db` est créée et remplie au premier démarrage
(elle est ignorée par Git : la supprimer pour repartir de zéro).

| Tag | Ce que l'on montre |
|-----|--------------------|
| `s06-etape1-dbcontext` | Paquets `Microsoft.EntityFrameworkCore.Sqlite` et `.Design`. `IncidentHubDbContext` et la Fluent API dans `OnModelCreating` : héritage TPH (une table `Users`, discriminateur `Role`), enums stockés en texte, setters privés, clés étrangères fantômes `ReportedById` / `AssignedToId`, liste privée `_comments` mappée par le champ. Constructeurs privés « pour EF Core » dans le domaine. Chaîne de connexion dans `appsettings.json`. |
| `s06-etape2-migration` | Outil local `dotnet-ef` (`dotnet-tools.json`), migration `InitialCreate`, `SeedData` qui remplit une base vide. Au démarrage : `Database.Migrate()` puis seed. |
| `s06-etape3-service` | Couche application `IncidentService` (scoped) : les pages ne voient plus ni dépôt ni `DbContext`. `AsNoTracking`, `Include` (déclarant, analyste), `ThenInclude` (auteurs des commentaires). Le SQL généré s'affiche dans la console. |
| `s06-etape4-n-plus-1` | `GetAllNaiveAsync` charge l'analyste de chaque incident dans une boucle. `/Incidents?naif=true` : 13 requêtes SQL dans la console pour 12 incidents, contre 1 seule avec `Include`. |

```bash
dotnet tool restore
dotnet ef migrations list --project src/IncidentHub.Web
git diff s06-etape3-service s06-etape4-n-plus-1 -- src/IncidentHub.Web/Services/IncidentService.cs
```

## Séance 7 — Formulaires, validation et overposting

| Tag | Ce que l'on montre |
|-----|--------------------|
| `s07-etape1-formulaire` | Page `Incidents/Create` : modèle de saisie `CreateIncidentInput` en `[BindProperty]`, liste déroulante des déclarants, Post-Redirect-Get vers Details. Sans validation, un titre vide fait remonter l'`ArgumentException` du domaine jusqu'à la page d'erreur. |
| `s07-etape2-validation-serveur` | DataAnnotations (`[Required]`, `[StringLength(120, MinimumLength = 5)]`, `[EnumDataType]`) avec messages en français, `if (!ModelState.IsValid) return Page();`, `asp-validation-for` et `asp-validation-summary`. |
| `s07-etape3-validation-client` | `_ValidationScriptsPartial` : le navigateur bloque le formulaire invalide. Confort, pas sécurité : curl ou les DevTools la contournent. |
| `s07-etape4-domaine-et-base` | Les exceptions du domaine deviennent des erreurs de `ModelState`. Bouton « Prendre en charge » sur Details (`OnPostStartWorkAsync`) : refus propre si aucun analyste. Contrainte `CHECK` sur le titre en base (migration `TitreNonVide`). Les quatre étages : navigateur, serveur, domaine, base. |
| `s07-etape5-overposting` | Page `Incidents/Edit` : `EditIncidentInputVulnerable` (avec `Severity`) contre `EditIncidentInput` (titre et description), choisis par `DemoVulnerable`. Un POST forgé avec `Input.Severity=Low` fait passer un incident critique en faible dans la version vulnérable. Un POST sans jeton anti-falsification reçoit 400. |

Les commandes pour rejouer les attaques sont en commentaire en tête de
`src/IncidentHub.Web/Pages/Incidents/Edit.cshtml.cs`. Pour l'étage « base » :

```sql
-- dans un client SQLite ouvert sur src/IncidentHub.Web/incidenthub-web.db
UPDATE Incidents SET Title = '   ';   -- CHECK constraint failed: CK_Incidents_Title_NonVide
```

La branche `main` correspond à l'état final `s07-etape5-overposting`.

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

### Séance 3

| Branche | Contenu | Ce que l'on observe |
|---------|---------|---------------------|
| `demo/s03-ia-nullable` | `Demos/RapportIncident.cs` : code d'un étudiant « corrigé » par une IA avec le prompt « Corrige les avertissements de nullabilité de ce fichier. ». Part de `s03-etape3-null`. Commit au message volontairement trompeur. | Les avertissements ont disparu parce que l'IA a ajouté des `!` partout (`incident.AssignedTo!.Name`...). Le build est propre, mais le programme plante (`NullReferenceException`) sur un incident non assigné. |

```bash
git checkout demo/s03-ia-nullable
dotnet run --project src/IncidentHub.Console   # plante à la fin
git diff s03-etape3-null demo/s03-ia-nullable
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
