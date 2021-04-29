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
    public class UserRepository :
        RepositoryBase,
        IUserRepository
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

            command.CommandText = Sql(nameof(CreateUser));

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

            command.CommandText = Sql(nameof(GetFullPasswordUser));
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

            command.CommandText = Sql(nameof(AddSiteRoleToUser));

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
	                ugr.leaderboardId AS leaderboardId,
	                ugr.type AS leaderboardRoleType
                FROM User u
                LEFT JOIN UserSiteRole usr ON usr.userId = u.ID
                LEFT JOIN UserLeaderboardRole ugr ON ugr.userId = u.ID";
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

                if (!reader.IsDBNull("leaderboardId"))
                {
                    var currentLeaderboardRole =
                        fullUser.LeaderboardRoles.FirstOrDefault(x => x.LeaderboardId == reader.GetInt32("leaderboardId"));
                    if (currentLeaderboardRole == null)
                    {
                        fullUser.LeaderboardRoles.Add(new LeaderboardRole
                        {
                            LeaderboardId = reader.GetInt32("leaderboardId"),
                            Roles = new List<LeaderboardRoleType> {(LeaderboardRoleType) reader.GetInt32("leaderboardRoleType")}
                        });
                    }
                    else
                    {
                        if (!currentLeaderboardRole.Roles.Contains((LeaderboardRoleType) reader.GetInt32("leaderboardRoleType")))
                        {
                            currentLeaderboardRole.Roles.Add((LeaderboardRoleType) reader.GetInt32("leaderboardRoleType"));
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
