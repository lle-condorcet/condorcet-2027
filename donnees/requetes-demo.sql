-- IncidentHub : requêtes montrées en direct (séances 4 et 5), dans l'ordre des slides.
-- Ouvrir incidenthub.db dans Rider (voir donnees/README.md), puis exécuter
-- chaque requête : curseur dans la requête, Ctrl+Entrée.

-- 1. Lire une table
SELECT id, title, severity, status
FROM incidents;

-- 2. Filtrer et trier
SELECT title, severity, created_at
FROM incidents
WHERE status = 'Open'
ORDER BY created_at DESC;

-- 3. Le piège de NULL : la première requête ne renvoie rien
SELECT title FROM incidents WHERE assigned_to_id = NULL;
SELECT title FROM incidents WHERE assigned_to_id IS NULL;

-- 4. Jointure : chaque incident avec son analyste
SELECT i.title, u.name AS analyste
FROM incidents i
JOIN users u ON u.id = i.assigned_to_id;

-- 5. LEFT JOIN : garder aussi les incidents sans analyste
SELECT i.title, u.name AS analyste
FROM incidents i
LEFT JOIN users u ON u.id = i.assigned_to_id;

-- 6. Regrouper : nombre d'incidents par sévérité
SELECT severity, COUNT(*) AS nb
FROM incidents
GROUP BY severity
ORDER BY nb DESC;

-- 7. Charge de travail par analyste (y compris ceux qui n'ont rien)
SELECT u.name, u.team, COUNT(i.id) AS nb
FROM users u
LEFT JOIN incidents i ON i.assigned_to_id = u.id
WHERE u.role = 'Analyst'
GROUP BY u.id, u.name, u.team
ORDER BY nb DESC;

-- 8. HAVING : les équipes qui ont plus de 3 incidents
SELECT u.team, COUNT(*) AS nb
FROM incidents i
JOIN users u ON u.id = i.assigned_to_id
GROUP BY u.team
HAVING COUNT(*) > 3;

-- 9. Relation N-N : les étiquettes de chaque incident
SELECT i.title, GROUP_CONCAT(t.name, ', ') AS etiquettes
FROM incidents i
JOIN incident_tags it ON it.incident_id = i.id
JOIN tags t ON t.id = it.tag_id
GROUP BY i.id, i.title;

-- 10. Index : plan d'exécution avant et après
EXPLAIN QUERY PLAN SELECT title FROM incidents WHERE severity = 'Critical';
CREATE INDEX ix_incidents_severity ON incidents (severity);
EXPLAIN QUERY PLAN SELECT title FROM incidents WHERE severity = 'Critical';
DROP INDEX ix_incidents_severity;   -- pour pouvoir rejouer la démo
