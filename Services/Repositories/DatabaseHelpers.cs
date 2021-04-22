using System;
using System.IO;
using System.Reflection;

namespace Services.Repositories
{
    public static class DatabaseHelpers
    {
        public static string PathToDatabase =>
            Directory.GetParent(Directory.GetCurrentDirectory()) + "\\leaderboard.db";
        public static readonly string DatabaseConnectionString = $"Data Source={PathToDatabase};";

        public static string GetSql(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            return reader.ReadToEnd();
        }
    }
}
