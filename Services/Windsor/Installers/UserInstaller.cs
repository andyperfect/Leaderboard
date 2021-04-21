using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
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
                    .For<IUserRepository, IUserRepository>()
                    .ImplementedBy<UserRepository>()
                    .LifestyleSingleton(),
            };

            container.Register(components);
        }
    }
}
