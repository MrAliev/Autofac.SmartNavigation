using System.Windows;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.SmartNavigation.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace WpfCore
{
    public partial class App : Application
    {
       public ILifetimeScope _Scope { get; private set; }
 
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection); // внедряем в Autofac все что было в стандартном контейнере 
            //builder.UseAutofind();
            
            _Scope = builder.Build();

            var mainWindow = _Scope.Resolve<MainWindow>();

            mainWindow.Show();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            // ...

            services.AddTransient(typeof(MainWindow));
        }
    }
}
