using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Services.User;
using Services.User.Models;
using Services.User.Roles;

namespace Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser(
            UserModel user,
            string salt,
            string password,
            string accessToken,
            long accessTokenDate)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);

            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText =
                @"INSERT INTO User (email, username, dateCreated, salt, password, accessToken, accessTokenDate)
                    VALUES (@email, @username, @dateCreated, @salt, @password, @accessToken, @accessTokenDate);
                    SELECT last_insert_rowid()";

            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@dateCreated", user.DateCreated);
            command.Parameters.AddWithValue("@salt", salt);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@accessToken", accessToken);
            command.Parameters.AddWithValue("@accessTokenDate", accessTokenDate);

            user.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public FullUser GetUserByAccessToken(string accessToken)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = GetUserSql(GetUserType.AccessToken);
            command.Parameters.AddWithValue("@accessToken", accessToken);

            var reader = command.ExecuteReader();
            return CreateFullUserFromRows(reader);
        }

        public FullUser GetUserById(long id)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = GetUserSql(GetUserType.Id);
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();
            return CreateFullUserFromRows(reader);
        }

        public FullUser GetUserByUsername(string username)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = GetUserSql(GetUserType.Username);
            command.Parameters.AddWithValue("@username", username);

            var reader = command.ExecuteReader();
            return CreateFullUserFromRows(reader);
        }

        public UserPasswordModel GetFullPasswordUser(string username)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = @"SELECT 
	                u.Id, 
	                u.email, 
	                u.username, 
	                u.dateCreated,
	                u.salt,
                    u.password,
                    u.accessToken,
                    u.accessTokenDate
                FROM User u
                WHERE u.username = @username;";
            command.Parameters.AddWithValue("@username", username);

            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new UserPasswordModel
                {
                    Id = reader.GetInt32("Id"),
                    Email = reader.GetString("email"),
                    Username = reader.GetString("username"),
                    DateCreated = reader.GetInt64("dateCreated"),
                    Salt = reader.GetString("salt"),
                    HashedPassword = reader.GetString("password"),
                    AccessToken = reader.GetString("accessToken"),
                    AccessTokenDate = reader.GetInt64("accessTokenDate")
                };
            }

            return null;
        }

        public void AddSiteRoleToUser(long userId, SiteRoleType type)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);

            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText =
                @"INSERT INTO UserSiteRole (userId, type) VALUES (@userId, @type);";

            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@type", (int) type);

            command.ExecuteNonQuery();
        }

        private static string GetUserSql(GetUserType type)
        {
            var sql = @"SELECT 
	                u.Id, 
	                u.email, 
	                u.username, 
	                u.dateCreated,
	                usr.type AS siteRoleType,
	                ugr.gameId AS gameRoleGameId,
	                ugr.type AS gameRoleType
                FROM User u
                LEFT JOIN UserSiteRole usr ON usr.userId = u.ID
                LEFT JOIN UserGameRole ugr ON ugr.userId = u.ID";
            sql += type switch
            {
                GetUserType.AccessToken => "\nWHERE u.accessToken = @accessToken;",
                GetUserType.Id => "\nWHERE u.ID = @id;",
                GetUserType.Username => "\nWHERE u.username = @username;",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return sql;
        }

        private static FullUser CreateFullUserFromRows(DbDataReader reader)
        {
            FullUser fullUser = null;
            while (reader.Read())
            {
                fullUser ??= new FullUser
                {
                    User = new UserModel
                    {
                        Id = reader.GetInt32("Id"),
                        Email = reader.GetString("email"),
                        Username = reader.GetString("username"),
                        DateCreated = reader.GetInt64("dateCreated")
                    }
                };

                if (!reader.IsDBNull("siteRoleType") && 
                    fullUser.SiteRoles.All(x => x != (SiteRoleType) reader.GetInt32("siteRoleType")))
                {
                    fullUser.SiteRoles.Add((SiteRoleType) reader.GetInt32("siteRoleType"));
                }

                if (!reader.IsDBNull("gameRoleGameId"))
                {
                    var currentGameRole =
                        fullUser.GameRoles.FirstOrDefault(x => x.GameId == reader.GetInt32("gameRoleGameId"));
                    if (currentGameRole == null)
                    {
                        fullUser.GameRoles.Add(new GameRole
                        {
                            GameId = reader.GetInt32("gameRoleGameId"),
                            Roles = new List<GameRoleType> {(GameRoleType) reader.GetInt32("gameRoleType")}
                        });
                    }
                    else
                    {
                        if (!currentGameRole.Roles.Contains((GameRoleType) reader.GetInt32("gameRoleType")))
                        {
                            currentGameRole.Roles.Add((GameRoleType) reader.GetInt32("gameRoleType"));
                        }
                    }
                }
            }

            return fullUser;
        }

        private enum GetUserType
        {
            AccessToken,
            Id,
            Username
        }
    }
}
