BEGIN TRANSACTION;

INSERT INTO Kategorije VALUES (1, 'Test');
INSERT INTO Kategorije VALUES (2, 'Svijet');

INSERT INTO Vijesti VALUES (1, 1, NULL, 'Vijest prva', '', 'I vijest bijaše riječ', 1, GETDATE(), NULL);
INSERT INTO Vijesti VALUES (2, 1, NULL, 'Vijest driga', '', 'U naslovu je typo', 1, GETDATE(), NULL);
INSERT INTO Vijesti VALUES (3, 1, NULL, 'Vijest treća', '', 'Nešto neočekivano', 1, GETDATE(), NULL);
INSERT INTO Vijesti VALUES (4, 1, NULL, 'Vijest četvrta', '', '. . .', 1, GETDATE(), NULL);
INSERT INTO Vijesti VALUES (5, 1, NULL, 'Vijest peta', '', 'Ostao bez idea', 1, GETDATE(), NULL);
INSERT INTO Vijesti VALUES (6, 1, NULL, 'BLAARGH', '', 'Nekome je dosadno', 2, GETDATE(), NULL);
INSERT INTO Vijesti VALUES (7, 1, NULL, 'Test procedure takeover', '', 'Hello. It''s me, GLaDOS. I took over the admin account to take over the testing.', 2, GETDATE(), NULL);

COMMIT TRANSACTION;

SELECT * FROM Vijesti;