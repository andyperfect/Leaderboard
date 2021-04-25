INSERT INTO User (email, username, dateCreated, salt, password, accessToken, accessTokenDate)
VALUES (@email, @username, @dateCreated, @salt, @password, @accessToken, @accessTokenDate);
SELECT last_insert_rowid();
