using Microsoft.Data.Sqlite;
using Services.DatabaseInitialization;

namespace Services.Repositories
{
    public class DatabaseInitializationRepository : IDatabaseInitializationRepository
    {
        public void CreateDatabase()
        {
            using var conn = new SqliteConnection(DatabaseHelpers.DatabaseConnectionString);
            conn.Open();
            var command = conn.CreateCommand();

            command.CommandText = DatabaseHelpers.GetSql("Services.EmbeddedResources.Sql.CreateDatabase.sql");
            command.ExecuteNonQuery();
        }
    }
}
