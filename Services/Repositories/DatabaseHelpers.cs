using System.IO;

namespace Services.Repositories
{
    public static class DatabaseHelpers
    {
        private static string PathToDatabase =>
            Directory.GetParent(Directory.GetCurrentDirectory()) + "\\leaderboard.db";
        public static readonly string DatabaseConnectionString = $"Data Source={PathToDatabase};";
    }
}
