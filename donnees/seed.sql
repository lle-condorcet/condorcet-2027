-- IncidentHub : données de démonstration (fictives).
-- sqlite3 incidenthub.db < seed.sql

PRAGMA foreign_keys = ON;

INSERT INTO users (id, name, email, role, team) VALUES
    (1, 'Alice Martin',   'alice.martin@exemple.be',   'Reporter', NULL),
    (2, 'Bruno Lefèvre',  'bruno.lefevre@exemple.be',  'Reporter', NULL),
    (3, 'Chloé Dubois',   'chloe.dubois@exemple.be',   'Reporter', NULL),
    (4, 'David Peeters',  'david.peeters@exemple.be',  'Analyst',  'SOC'),
    (5, 'Emma Janssens',  'emma.janssens@exemple.be',  'Analyst',  'SOC'),
    (6, 'Farid Benali',   'farid.benali@exemple.be',   'Analyst',  'CERT'),
    (7, 'Grace Lambert',  'grace.lambert@exemple.be',  'Analyst',  'CERT'),
    (8, 'Hugo Mertens',   'hugo.mertens@exemple.be',   'Manager',  NULL);

INSERT INTO incidents (id, title, severity, status, created_at, reported_by_id, assigned_to_id) VALUES
    ( 1, 'Phishing ciblant la comptabilité',        'High',     'Closed',     '2026-10-01 08:12', 1, 4),
    ( 2, 'Rançongiciel sur le serveur de fichiers', 'Critical', 'Resolved',   '2026-10-03 22:47', 2, 6),
    ( 3, 'Mot de passe partagé dans Teams',         'Medium',   'Closed',     '2026-10-06 10:05', 3, 5),
    ( 4, 'Connexion VPN depuis un pays inhabituel', 'High',     'InProgress', '2026-10-09 03:31', 2, 4),
    ( 5, 'Clé USB trouvée sur le parking',          'Low',      'Open',       '2026-10-12 14:20', 1, NULL),
    ( 6, 'Fuite de données clients sur un forum',   'Critical', 'InProgress', '2026-10-15 09:58', 3, 6),
    ( 7, 'Certificat TLS expiré sur l''intranet',   'Medium',   'Resolved',   '2026-10-18 07:40', 1, 5),
    ( 8, 'Compte administrateur créé sans demande', 'Critical', 'Open',       '2026-10-21 18:03', 2, NULL),
    ( 9, 'Faux support informatique au téléphone',  'Medium',   'Open',       '2026-10-24 11:16', 3, NULL),
    (10, 'Scan de ports depuis le réseau invité',   'Low',      'Closed',     '2026-10-27 16:22', 1, 4),
    (11, 'Macro suspecte dans une facture',         'High',     'Open',       '2026-10-29 08:50', 2, 5),
    (12, 'Imprimante qui contacte un serveur externe', 'Medium', 'InProgress', '2026-11-02 13:37', 3, 6);

INSERT INTO comments (incident_id, author_id, text, created_at) VALUES
    (1, 4, 'Expéditeur bloqué, 3 boîtes nettoyées.',                '2026-10-01 09:30'),
    (1, 8, 'Prévoir une sensibilisation pour la comptabilité.',     '2026-10-02 10:00'),
    (2, 6, 'Serveur isolé du réseau.',                              '2026-10-03 23:10'),
    (2, 6, 'Restauration depuis la sauvegarde du 2 octobre.',       '2026-10-04 06:45'),
    (2, 8, 'Déclaration au CCB à faire avant demain.',              '2026-10-04 08:00'),
    (4, 4, 'Session coupée, MFA réinitialisée.',                    '2026-10-09 07:15'),
    (6, 6, 'Échantillon vérifié : 2 400 adresses e-mail.',          '2026-10-15 11:20'),
    (6, 8, 'Prévenir le DPO.',                                      '2026-10-15 11:45'),
    (7, 5, 'Certificat renouvelé.',                                 '2026-10-18 09:05'),
    (8, 1, 'Je n''ai pas créé ce compte, je confirme.',             '2026-10-21 19:00'),
    (11, 5, 'Pièce jointe envoyée en bac à sable.',                 '2026-10-29 09:40');

INSERT INTO tags (id, name) VALUES
    (1, 'phishing'), (2, 'ransomware'), (3, 'fuite-de-données'),
    (4, 'accès'), (5, 'réseau'), (6, 'ingénierie-sociale');

INSERT INTO incident_tags (incident_id, tag_id) VALUES
    (1, 1), (1, 6), (2, 2), (3, 4), (4, 4), (4, 5), (5, 6), (6, 3),
    (8, 4), (9, 6), (10, 5), (11, 1), (12, 5), (12, 3);
