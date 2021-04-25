SELECT
    u.Id,
    u.email,
    u.username,
    u.dateCreated,
    u.salt,
    u.password,
    u.accessToken,
    u.accessTokenDate
FROM User u
WHERE u.username = @username;
