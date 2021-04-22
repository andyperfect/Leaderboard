using System;
using System.IO;
using Services.Repositories;
using Services.User;
using Services.User.Roles;
using Services.Windsor;

namespace Services.DatabaseInitialization
{
    public class DatabaseInitializationService : IDatabaseInitializationService
    {
        private readonly IDatabaseInitializationRepository _databaseInitializationRepository;
        private readonly IUserService _userService;
        
        public DatabaseInitializationService()
        {
            _databaseInitializationRepository = IoC.Container.Resolve<IDatabaseInitializationRepository>();
            _userService = IoC.Container.Resolve<IUserService>();
        }
        
        public void InitializeDatabase(string email, string username, string password)
        {
            if (File.Exists(DatabaseHelpers.PathToDatabase))
            {
                throw new UnauthorizedAccessException("Not Authorized to perform this action");
            }
            _databaseInitializationRepository.CreateDatabase();
            var user = _userService.CreateUser(email, username, password);
            _userService.GiveUserSiteRole(user.Id, SiteRoleType.Administrator);
        }
    }
}
