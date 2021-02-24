using System.Windows;
using Autofac;
using Autofac.SmartNavigation.Extensions;
using Autofac.SmartNavigation.Interfaces;
using WpfFramework.Extensions;
using WpfFramework.Views.Windows;

namespace WpfFramework
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // создание и конфигурация контейнера
            var scope = new ContainerBuilder()
                .UseAutofind() // использовать автоматическую регистрацию представлений и моделей представлений
                .RegisterServices() 
                .Build();

            // если отключить StartUpUri в App.xaml, то можно вызвать стартовое окно следующим образом:
            
            var navigation = scope.Resolve<INavigationService>(); // получаем сервис навигации

            navigation.Navigate(nameof(ShellWindow));  // вызываем окно по имени.
        }
    }
}
