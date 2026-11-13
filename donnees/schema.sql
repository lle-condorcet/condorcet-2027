-- IncidentHub : schéma de la séance 4 (SQL et modélisation)
-- Données entièrement fictives. Exécuter avant seed.sql.
-- SQLite : sqlite3 incidenthub.db < schema.sql

PRAGMA foreign_keys = ON;

DROP TABLE IF EXISTS incident_tags;
DROP TABLE IF EXISTS tags;
DROP TABLE IF EXISTS comments;
DROP TABLE IF EXISTS incidents;
DROP TABLE IF EXISTS users;

-- Une seule table pour User et ses trois rôles (Reporter, Analyst, Manager).
-- La colonne role dit de quel type est la ligne ; team n'a de sens que pour un analyste.
-- C'est la stratégie qu'EF Core utilise par défaut pour l'héritage (TPH, séance 6).
CREATE TABLE users (
    id    INTEGER PRIMARY KEY,
    name  TEXT    NOT NULL,
    email TEXT    NOT NULL UNIQUE,
    role  TEXT    NOT NULL CHECK (role IN ('Reporter', 'Analyst', 'Manager')),
    team  TEXT
);

CREATE TABLE incidents (
    id             INTEGER PRIMARY KEY,
    title          TEXT    NOT NULL CHECK (length(trim(title)) > 0),
    description    TEXT    NOT NULL DEFAULT '',
    severity       TEXT    NOT NULL CHECK (severity IN ('Low', 'Medium', 'High', 'Critical')),
    status         TEXT    NOT NULL DEFAULT 'Open'
                           CHECK (status IN ('Open', 'InProgress', 'Resolved', 'Closed')),
    created_at     TEXT    NOT NULL,
    reported_by_id INTEGER NOT NULL REFERENCES users (id),
    assigned_to_id INTEGER          REFERENCES users (id)   -- NULL : personne n'est assigné
);

-- Composition : un commentaire n'existe pas sans son incident.
CREATE TABLE comments (
    id          INTEGER PRIMARY KEY,
    incident_id INTEGER NOT NULL REFERENCES incidents (id) ON DELETE CASCADE,
    author_id   INTEGER NOT NULL REFERENCES users (id),
    text        TEXT    NOT NULL,
    created_at  TEXT    NOT NULL
);

-- Relation N-N : un incident a plusieurs étiquettes, une étiquette concerne plusieurs incidents.
CREATE TABLE tags (
    id   INTEGER PRIMARY KEY,
    name TEXT    NOT NULL UNIQUE
);

CREATE TABLE incident_tags (
    incident_id INTEGER NOT NULL REFERENCES incidents (id) ON DELETE CASCADE,
    tag_id      INTEGER NOT NULL REFERENCES tags (id),
    PRIMARY KEY (incident_id, tag_id)
);
