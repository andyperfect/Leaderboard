using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Services.Leaderboard;
using Services.Leaderboard.Models;
using System.Data;
using System.Data.Common;

namespace Services.Repositories
{
    public class LeaderboardRepository : 
        RepositoryBase, 
        ILeaderboardRepository
    {
        public void Create(LeaderboardModel leaderboard)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            var command = OpenConnWithSql(conn, nameof(Create));

            command.Parameters.AddWithValue("@title", leaderboard.Title);
            command.Parameters.AddWithValue("@dateCreated", leaderboard.DateCreated);
            command.Parameters.AddWithValue("@addedBy", leaderboard.AddedBy);

            leaderboard.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public LeaderboardModel Get(long id)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            var command = OpenConnWithSql(conn, nameof(Get));
            command.Parameters.AddWithValue("@id", id);

            var reader = command.ExecuteReader();
            return reader.Read() ? CreateModelFromReader(reader) : null;
        }

        public bool UpdateTitle(long id, string title)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            var command = OpenConnWithSql(conn, nameof(UpdateTitle));
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            return command.ExecuteNonQuery() > 0;
        }

        private static LeaderboardModel CreateModelFromReader(DbDataReader reader)
        {
            return new LeaderboardModel
            {
                Id = reader.GetInt64("Id"),
                Title = reader.GetString("title"),
                DateCreated = reader.GetInt64("dateCreated"),
                AddedBy = reader.GetInt64("addedBy")
            };
        }
    }
}
