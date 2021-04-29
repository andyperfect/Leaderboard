using System;
using Microsoft.Data.Sqlite;
using Services.Leaderboard;
using Services.Leaderboard.Models;

namespace Services.Repositories
{
    public class LeaderboardRepository : 
        RepositoryBase, 
        ILeaderboardRepository
    {
        public void Create(LeaderboardModel leaderboard)
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);

            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = Sql(nameof(Create));
            command.Parameters.AddWithValue("@title", leaderboard.Title);
            command.Parameters.AddWithValue("@dateCreated", leaderboard.DateCreated);
            command.Parameters.AddWithValue("@addedBy", leaderboard.AddedBy);

            leaderboard.Id = Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
