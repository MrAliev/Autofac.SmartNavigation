using System.Windows;
using Autofac;
using Autofac.SmartNavigation.Extensions;
using Autofac.SmartNavigation.Interfaces;
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

            var builder = new ContainerBuilder()
                .UseAutofind()
                .RegisterServices();

            Scope = builder.Build().BeginLifetimeScope();

            var navigation = Scope.Resolve<INavigationService>();

            navigation.Navigate("ShellWindow");
        }
    }
}
