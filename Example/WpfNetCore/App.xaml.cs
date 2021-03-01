using System.Windows;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.SmartNavigation;
using Autofac.SmartNavigation.Extensions;
using Autofac.SmartNavigation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WpfNetCore.Extensions;

namespace WpfNetCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ILifetimeScope Scope { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // создаем Autofac
            var builder = new ContainerBuilder()
                .UseAutofind()              // для использования автоматической регистрации представлений и моделей представлений
                .RegisterServices();        // регистрация сервисов в Autofac


            // если вы используете стандартный IoC .Net Core, и не хотите от него отказываться в пользу Autofac,
            // то можно зарегистрировать все ваши сервисы из стандартного контейнера в Autofac всего одной строчкой:

            // это создание и конфигурация стандартного контейнера
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // а здесь мы регистрируем в Autofac все то что было в стандартном контейнере
            builder.Populate(serviceCollection);

            // формируем скоп Autofac
            Scope = builder.Build().BeginLifetimeScope();

            // получаем сервис навигации из скопа Autofac, не зависимо от того где он был зарегистрирован (в Autofac или в стандартном IoC .Net Core)
            var navigation = Scope.Resolve<INavigationService>();

            navigation.Navigate("ShellWindow");
        }

        
        private void ConfigureServices(IServiceCollection services)
        {
            // ...
            // для примера сервис навигации регистрируется в стандартном контейнере
            services.AddSingleton<INavigationService, AppNavigationService>();
        }
    }
}
