INSERT INTO Leaderboard (title, dateCreated, addedBy)
VALUES (@title, @dateCreated, @addedBy);
SELECT last_insert_rowid();
