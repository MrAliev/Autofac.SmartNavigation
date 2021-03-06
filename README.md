# Autofac.SmartNavigation
Autofac + умный навигационный сервис WPF MVVM. В пакете реализован навигационный сервис, позволяющий выполнять страничные переходы (Page) и оконную навигацию в рамках шаблона MVVM. 
Присутствует возможность автоматичекого сканирования всех сборок в папке проекта (при запуске) на предмет наличия в них представлений (Window и Page) и моделей представлений, с автоматической регистрацией в контейнере Autofac. Эта возможность позволяет не задумываясь о регистрации в контейнере представлений и их моделей - просто использовать их в навигационном сервисе.

## Поддерживаемые платформы

* .Net Framework >= 4.7.1
* .Net Core >= 3.1 (.Net 5.0)

## Зависимости
* Autofac 6.1.0 [Autofac][]

### Версии пакета
##### Актуальная версия
* [1.0.2-alpha](https://www.nuget.org/packages/Autofac.SmartNavigation/1.0.2-alpha)
##### Предыдущие версии
* [1.0.1-alpha](https://www.nuget.org/packages/Autofac.SmartNavigation/1.0.1-alpha)

## Использование
Установить [NuGet][] - пакет или [скачайте][] репозиторий с примером использования.

#### .Net Framework
```c#
using System.Windows;
using Autofac;
using Autofac.SmartNavigation.Extensions;
using Autofac.SmartNavigation.Interfaces;
using WpfFramework.Extensions;

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

            navigation.Navigate("Имя_окна");  // вызываем окно по имени.
        }
    }
}
```

#### .Net Core
```c#
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
```

* Более детальное описание можно посмотреть в [документации][]
* Документация по [Autofac](https://autofaccn.readthedocs.io/en/latest/) 

#### Обратная связь 
* Mr.Aliev.Anton@gmail.com
* [Issues](https://github.com/MrAliev/Autofac.SmartNavigation/issues)

[Autofac]: https://www.nuget.org/packages/Autofac/6.1.0?_src=template
[NuGet]: https://www.nuget.org/packages/Autofac.SmartNavigation/1.0.0-alpha
[документации]: https://
[скачайте]: https://github.com/MrAliev/Autofac.SmartNavigation/tree/master
