using System.Windows;
using System.Windows.Controls;
using Autofac.SmartNavigation.Interfaces;

namespace Autofac.SmartNavigation.Base
{
    /// <summary>
    /// Базовый класс окна с возможностью страничной навигации
    /// </summary>
    public abstract class NavigationalWindow : Window, INavigationWindow
    {
        public abstract Frame Frame { get; set; }
    }
}
