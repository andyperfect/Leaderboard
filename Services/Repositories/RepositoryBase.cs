using System;
using System.IO;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace Services.Repositories
{
    public class RepositoryBase
    {
        protected string Sql(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"Services.EmbeddedResources.Sql.{GetType().Name}.{name}.sql";
            using var stream = assembly.GetManifestResourceStream(resourcePath);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            return reader.ReadToEnd();
        }

        protected SqliteCommand OpenConnWithSql(SqliteConnection conn, string sqlSource)
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = Sql(sqlSource);
            return command;
        }
    }
}
