CREATE TABLE "User"
(
    "Id"              INTEGER,
    "email"           TEXT    NOT NULL UNIQUE,
    "username"        TEXT    NOT NULL UNIQUE,
    "dateCreated"     INTEGER NOT NULL,
    "salt"            TEXT    NOT NULL,
    "password"        TEXT    NOT NULL,
    "accessToken"     TEXT    NOT NULL,
    "accessTokenDate" NUMERIC NOT NULL,
    PRIMARY KEY ("Id" AUTOINCREMENT)
);

CREATE TABLE "Leaderboard"
(
    "Id"          INTEGER,
    "title"       TEXT    NOT NULL,
    "dateCreated" INTEGER NOT NULL,
    "addedBy"     INTEGER,
    PRIMARY KEY ("Id" AUTOINCREMENT),
    FOREIGN KEY ("addedBy") REFERENCES "User" ("Id") ON DELETE SET NULL
);

CREATE TABLE "LeaderboardCategory"
(
    "Id"            INTEGER,
    "leaderboardId" INTEGER NOT NULL,
    "name"          TEXT    NOT NULL,
    "rules"         TEXT,
    FOREIGN KEY ("leaderboardId") REFERENCES "Leaderboard" ("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id" AUTOINCREMENT)
);

CREATE TABLE "LeaderboardCategoryVariable"
(
    "Id"                    INTEGER,
    "leaderboardCategoryId" INTEGER NOT NULL,
    "dataType"              INTEGER NOT NULL,
    "displayType"           INTEGER NOT NULL,
    "name"                  TEXT    NOT NULL,
    "defaultValue"          TEXT    NOT NULL,
    PRIMARY KEY ("Id" AUTOINCREMENT)
);

CREATE TABLE "Submission"
(
    "Id"               INTEGER,
    "leaderboardId"    INTEGER NOT NULL,
    "userId"           INTEGER NOT NULL,
    "dateCreated"      INTEGER NOT NULL,
    "dateOfSubmission" INTEGER NOT NULL,
    "status"           INTEGER NOT NULL,
    FOREIGN KEY ("leaderboardId") REFERENCES "Leaderboard" ("Id") ON DELETE CASCADE,
    FOREIGN KEY ("userId") REFERENCES "User" ("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id" AUTOINCREMENT)
);

CREATE TABLE "SubmissionVariable"
(
    "Id"                            INTEGER,
    "submissionId"                  INTEGER NOT NULL,
    "leaderboardCategoryVariableId" INTEGER NOT NULL,
    "value"                         TEXT,
    FOREIGN KEY ("submissionId") REFERENCES "Submission" ("Id") ON DELETE CASCADE,
    FOREIGN KEY ("leaderboardCategoryVariableId") REFERENCES "LeaderboardCategoryVariable" ("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id" AUTOINCREMENT)
);

CREATE TABLE "UserLeaderboardRole"
(
    "Id"            INTEGER,
    "userId"        INTEGER NOT NULL,
    "leaderboardId" INTEGER NOT NULL,
    "type"          INTEGER NOT NULL,
    FOREIGN KEY ("leaderboardId") REFERENCES "Leaderboard" ("Id") ON DELETE CASCADE,
    FOREIGN KEY ("userId") REFERENCES "User" ("Id") ON DELETE CASCADE,
    PRIMARY KEY ("Id" AUTOINCREMENT)
);

CREATE TABLE "UserSiteRole"
(
    "Id"     INTEGER,
    "userId" INTEGER NOT NULL,
    "type"   INTEGER NOT NULL,
    PRIMARY KEY ("Id" AUTOINCREMENT),
    FOREIGN KEY ("userId") REFERENCES "User" ("Id") ON DELETE CASCADE
);
