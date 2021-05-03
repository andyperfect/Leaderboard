SELECT Id,
       title,
       dateCreated,
       addedBy
FROM Leaderboard
WHERE Id = @id;
