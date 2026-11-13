# La base de données IncidentHub (séances 4 et 5)

Ce dossier contient la base utilisée en cours pour apprendre à lire du SQL, et pour les exercices
des QCM.

| Fichier | Contenu |
|---|---|
| `incidenthub.db` | La base SQLite, prête à l'emploi : 8 utilisateurs, 12 incidents, des commentaires et des étiquettes. Données fictives. |
| `requetes-demo.sql` | Les requêtes montrées en cours, dans l'ordre des slides. |
| `schema.sql` | La création des tables (`CREATE TABLE`). |
| `seed.sql` | Les données insérées (`INSERT`). |

Une base SQLite est un **simple fichier** : rien à installer côté serveur, pas de mot de passe.

## 1. Récupérer le fichier `incidenthub.db`

Au choix :

- **Sans Git** : sur la page GitHub du dépôt
  (https://github.com/lle-condorcet/condorcet-2027), ouvrir le dossier `donnees`, cliquer sur
  `incidenthub.db`, puis sur le bouton **Download raw file** (flèche vers le bas, en haut à droite
  du fichier). Faire de même pour `requetes-demo.sql`.
- **Avec Git** : `git clone https://github.com/lle-condorcet/condorcet-2027.git`, puis aller dans
  le dossier `donnees`.

Ranger les fichiers dans un dossier facile à retrouver (par exemple `Documents\IncidentHub`).

## 2. Ouvrir la base dans Rider

1. Ouvrir Rider (aucune solution n'est nécessaire : l'écran d'accueil suffit, ou n'importe quel
   projet ouvert).
2. Ouvrir la fenêtre **Database** : menu **View > Tool Windows > Database** (ou l'icône en forme
   de cylindre sur le bord droit de la fenêtre).
3. Dans la fenêtre Database : **+** > **Data Source** > **SQLite**.
4. Dans le champ **File**, cliquer sur **…** et choisir `incidenthub.db`.
5. Si Rider affiche **Download missing driver files** en bas de la fenêtre, cliquer dessus et
   attendre quelques secondes (une seule fois).
6. Cliquer sur **Test Connection** (le message doit être « Succeeded »), puis sur **OK**.
7. La base apparaît dans la fenêtre Database : déplier `main` > `tables` pour voir `users`,
   `incidents`, `comments`, `tags` et `incident_tags`. Double-cliquer sur une table affiche son
   contenu.

## 3. Écrire et exécuter une requête

1. Clic droit sur la base dans la fenêtre Database > **New > Query Console**.
2. Écrire la requête, par exemple :

   ```sql
   SELECT title, severity FROM incidents;
   ```

3. Placer le curseur dans la requête et appuyer sur **Ctrl+Entrée** (ou le bouton ▶ vert).
4. Le résultat s'affiche en bas. Le nombre de lignes est indiqué au-dessus ou en dessous du
   tableau de résultats.

Pour rejouer les requêtes du cours : ouvrir `requetes-demo.sql` dans Rider (**File > Open**), choisir
la base `incidenthub.db` comme source de données si Rider le demande (en haut à droite de
l'éditeur), puis Ctrl+Entrée sur chaque requête.

## 4. Sans Rider

- **DB Browser for SQLite** (gratuit, Windows, macOS, Linux : https://sqlitebrowser.org) :
  **Open Database** > `incidenthub.db`, onglet **Execute SQL**, écrire la requête, **Ctrl+Entrée**.
- **SQLite dans le navigateur** (https://sqlite.org/fiddle) : charger le fichier `incidenthub.db`
  depuis le menu de la page, puis écrire la requête dans la zone de saisie et l'exécuter. Le
  fichier reste sur votre ordinateur : rien n'est envoyé à un serveur.

## 5. En cas de problème

| Problème | Solution |
|---|---|
| « Driver files are not downloaded » | Cliquer sur **Download** dans la fenêtre de la source de données (étape 5). |
| La table est vide ou n'existe pas | Vérifier que le fichier choisi est bien `incidenthub.db` du dossier `donnees`, et non un fichier vide créé par erreur. |
| J'ai modifié les données par erreur | Télécharger à nouveau `incidenthub.db` depuis GitHub. |
| « no such column » | Vérifier l'orthographe : les noms sont en anglais et en minuscules (`severity`, `assigned_to_id`). |
