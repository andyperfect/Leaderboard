using Castle.Windsor;
using Services.Windsor.Installers;

namespace Services.Windsor
{
    public class IoC
    {
        public static IWindsorContainer Container;

        public static void Instantiate()
        {
            Container = new WindsorContainer();
            Container.Install(new UserInstaller());
        }
    }
}
