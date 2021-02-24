using System.ComponentModel;
using System.Windows.Navigation;

namespace Autofac.SmartNavigation.Interfaces
{
    /// <summary>
    /// Интерфейс навигационного сервиса
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Навигационный сервис приложения для страничной (Page) навигации
        /// </summary>
        NavigationService Service { get; set; }

        /// <summary>
        /// Открывает модальное окно. В качестве владельца окна устанавливается текущее активное окно
        /// </summary>
        /// <param name="aliasDialog">Название модального окна</param>
        /// <param name="context">Объект модели-представления для конструктора</param>
        /// <returns></returns>
        bool? NavigateDialog(string aliasDialog, object context);

        /// <summary>
        /// Открывает модальное окно. В качестве владельца окна устанавливается текущее активное окно
        /// </summary>
        /// <param name="aliasDialog">Название модального окна</param>
        /// <param name="findContext">Флаг, указывающий на необходимость автоматического поиска модели представления</param>
        /// <returns></returns>
        bool? NavigateDialog(string aliasDialog, bool findContext = true);

        /// <summary>
        /// Открывает модальное окно. В качестве владельца окна устанавливается текущее активное окно
        /// </summary>
        /// <param name="aliasDialog">Название модального окна</param>
        /// <param name="aliasContext">Название модели представления</param>
        /// <returns></returns>
        bool? NavigateDialog(string aliasDialog, string aliasContext);

        /// <summary>
        /// Выполняет переход на указанную страницу (или открывает новое окно) в зависимости от типа представления
        /// </summary>
        /// <param name="aliasView">Название представления</param>
        /// <param name="context">Объект модели представления</param>
        /// <param name="findDuplicate">Флаг, указывающий на необходимость искать запрошенное окно среди открытых.
        /// Если запрошенное окно не будет найдено среди уже открытых - будет открыто новое окно.
        /// Флаг игнорируется, если в качестве представления выступает страница (Page)</param>
        void Navigate(string aliasView, INotifyPropertyChanged context, bool findDuplicate = false);

        /// <summary>
        /// Выполняет переход на указанную страницу (или открывает новое окно) в зависимости от типа представления
        /// </summary>
        /// <param name="aliasView">Название представления</param>
        /// <param name="aliasContext">Название модели представления</param>
        /// <param name="findDuplicate">Флаг, указывающий на необходимость искать запрошенное окно среди открытых.
        /// Если запрошенное окно не будет найдено среди уже открытых - будет открыто новое окно.
        /// Флаг игнорируется, если в качестве представления выступает страница (Page)</param>
        void Navigate(string aliasView, string aliasContext, bool findDuplicate = false);

        /// <summary>
        /// Выполняет переход на указанную страницу (или открывает новое окно) в зависимости от типа представления
        /// </summary>
        /// <param name="aliasView">Название представления</param>
        /// <param name="findContext">Флаг, указывающий на необходимость автоматического поиска модели представления</param>
        /// <param name="findDuplicate">Флаг, указывающий на необходимость искать запрошенное окно среди открытых.
        /// Если запрошенное окно не будет найдено среди уже открытых - будет открыто новое окно.
        /// Флаг игнорируется, если в качестве представления выступает страница (Page)</param>
        void Navigate(string aliasView, bool findContext = true, bool findDuplicate = false);
    }
}
