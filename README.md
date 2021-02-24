# Autofac.SmartNavigation
Autofac + умный навигационный сервис WPF MVVM. В пакете реализован навигационный сервис, позволяющий выполнять страничные переходы (Page) и оконную навигацию в рамках шаблона MVVM. 
Присутствует возможность автоматичекого сканирования всех сборок в папке проекта (при запуске) на предмет наличия в них представлений (Window и Page) и моделей представлений, с автоматической регистрацией в контейнере Autofac. Эта возможность позволяет не задумываясь о регистрации в контейнере представлений и их моделей - просто использовать их в навигационном сервисе.

## Поддерживаемые платформы

Пакет нацелен на .Net Framework >= 4.7.1

## Зависимости
* Autofac 6.1.0 [Autofac][]

### Версии пакета
##### Актуальная версия
* 1.0.0-alpha
##### Предыдущие версии
--

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
* Более детальное описание можно посмотреть в [документации][]
* Документация по [Autofac](https://autofaccn.readthedocs.io/en/latest/) 

[Autofac]: https://www.nuget.org/packages/Autofac/6.1.0?_src=template
[NuGet]: https://
[документации]: https://
[скачайте]: https://github.com/MrAliev/Autofac.SmartNavigation/tree/master
