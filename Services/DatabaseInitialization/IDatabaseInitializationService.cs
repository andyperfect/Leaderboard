namespace Services.DatabaseInitialization
{
    public interface IDatabaseInitializationService
    {
        void InitializeDatabase(string email, string username, string password);
    }
}
