using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Services.DatabaseInitialization;
using Services.Leaderboard;
using Services.Repositories;
using Services.User;

namespace Services.Windsor.Installers
{
    public class UserInstaller
        : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            IRegistration[] components =
            {
                Component
                    .For<UserService, IUserService>()
                    .ImplementedBy<UserService>()
                    .LifestyleSingleton(),
                Component
                    .For<UserRepository, IUserRepository>()
                    .ImplementedBy<UserRepository>()
                    .LifestyleSingleton(),
                Component
                    .For<LeaderboardService, ILeaderboardService>()
                    .ImplementedBy<LeaderboardService>()
                    .LifestyleSingleton(),
                Component
                    .For<LeaderboardRepository, ILeaderboardRepository>()
                    .ImplementedBy<LeaderboardRepository>()
                    .LifestyleSingleton(),
                Component
                    .For<DatabaseInitializationService, IDatabaseInitializationService>()
                    .ImplementedBy<DatabaseInitializationService>()
                    .LifestyleSingleton(),
                Component
                    .For<DatabaseInitializationRepository, IDatabaseInitializationRepository>()
                    .ImplementedBy<DatabaseInitializationRepository>()
                    .LifestyleSingleton(),
            };

            container.Register(components);
        }
    }
}
