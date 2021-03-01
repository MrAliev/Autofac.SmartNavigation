using Autofac;
using WpfNetCore.Services;

namespace WpfNetCore.Extensions
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterServices(this ContainerBuilder builder)
        {
            // для примера, этот сервис был зарегистрирован через стандартный Ioc контейнер Net.Core
            //builder.RegisterType<AppNavigationService>().As<INavigationService>().SingleInstance().AsSelf();

            builder.RegisterType<DataProvider>().AsImplementedInterfaces().SingleInstance().AsSelf();

            return builder;
        }
    }
}
