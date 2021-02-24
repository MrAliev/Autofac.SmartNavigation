using System.Windows.Controls;

namespace Autofac.SmartNavigation.Interfaces
{
    /// <summary>
    /// Интерфейс окна со страничной навигацией
    /// </summary>
    public interface INavigationWindow
    {
        /// <summary>
        /// Фрейм для страничной навигации
        /// </summary>
        Frame Frame { get; set; }
    }
}
