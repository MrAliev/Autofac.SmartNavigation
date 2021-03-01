using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Autofac.SmartNavigation.Base;
using Autofac.SmartNavigation.Interfaces;

namespace Autofac.SmartNavigation
{
    /// <summary>
    /// Сервис навигации
    /// </summary>
    public class AppNavigationService : INavigationService
    {
        #region Private fields

        private readonly ILifetimeScope _scope;
        private NavigationService _service;

        #endregion

        #region Public properties

        /// <summary>
        /// Навигационный сервис приложения для страничной (Page) навигации
        /// </summary>
        public NavigationService Service
        {
            get => _service;
            set
            {
                if (_service != null)
                {
                    _service.Navigated -= NavService_Navigated;
                }
                _service = value;
                _service.Navigated += NavService_Navigated;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Открывает модальное окно. В качестве владельца окна устанавливается текущее активное окно
        /// </summary>
        /// <param name="aliasDialog">Название модального окна</param>
        /// <param name="context">Объект модели-представления для конструктора</param>
        /// <returns></returns>
        public bool? NavigateDialog(string aliasDialog, object context)
        {
            if (context == null)
            {
                ShowError("Контекст не должен быть null");
                return false;
            }
            aliasDialog = aliasDialog.ToLower();
            if (!GetView(aliasDialog, out var view))
            {
                ShowError($"Не удалось найти запрашиваемое окно \"{aliasDialog}\"");
                return false;
            }

            switch (view)
            {
                case Window win:
                    var type = win.GetType();
                    win.Close();

                    if (!(Activator.CreateInstance(type, context) is Window dialog)) return false;
                    dialog.Owner = GetActiveWindow();
                    return dialog.ShowDialog();
            }

            ShowError($"Запрашиваемое представление не является \"{aliasDialog}\" не является потомком класса \"Window\"");
            return false;
        }

        /// <summary>
        /// Открывает модальное окно. В качестве владельца окна устанавливается текущее активное окно
        /// </summary>
        /// <param name="aliasDialog">Название модального окна</param>
        /// <param name="findContext">Флаг, указывающий на необходимость автоматического поиска модели представления</param>
        /// <returns></returns>
        public bool? NavigateDialog(string aliasDialog, bool findContext = true)
        {
            aliasDialog = aliasDialog.ToLower();
            if (!GetView(aliasDialog, out var view))
            {
                ShowError($"Не удалось найти запрашиваемое окно \"{aliasDialog}\"");
                return false;
            }

            object context = null;

            if (findContext && !GetContext(aliasDialog, out context))
            {
                ShowError($"Не удалось найти контекст \"{aliasDialog}\" для окна \"{aliasDialog}\"");
                return false;
            }

            try
            {
                switch (view)
                {

                    case Window win:
                        var type = win.GetType();
                        win.Close();

                        if (!findContext)
                        {
                            if (!(Activator.CreateInstance(type) is Window dialog)) return false;
                            dialog.Owner = GetActiveWindow();
                            return dialog.ShowDialog();
                        }
                        else
                        {
                            if (!(Activator.CreateInstance(type, context) is Window dialog)) return false;
                            dialog.Owner = GetActiveWindow();
                            return dialog.ShowDialog();
                        }
                }
            }
            catch (ArgumentNullException e)
            {
                ShowError(e.Message);
            }
            catch (ArgumentException e)
            {
                ShowError(e.Message);
            }
            catch (NotSupportedException e)
            {
                ShowError(e.Message);
            }
            catch (TypeLoadException e)
            {
                ShowError(e.Message);
            }
            catch (MissingMemberException e)
            {
                ShowError(e.Message);
            }

            return false;
        }

        /// <summary>
        /// Открывает модальное окно. В качестве владельца окна устанавливается текущее активное окно
        /// </summary>
        /// <param name="aliasDialog">Название модального окна</param>
        /// <param name="aliasContext">Название модели представления</param>
        /// <returns></returns>
        public bool? NavigateDialog(string aliasDialog, string aliasContext)
        {
            aliasDialog = aliasDialog.ToLower();
            if (!GetView(aliasDialog, out var view))
            {
                ShowError($"Не удалось найти запрашиваемое окно \"{aliasDialog}\"");
                return false;
            }

            if (!GetContext(aliasContext, out var context))
            {
                ShowError($"Не удалось найти контекст \"{aliasContext}\" для окна \"{aliasDialog}\"");
                return false;
            }

            switch (view)
            {
                case Window win:
                    var type = win.GetType();
                    win.Close();

                    if (!(Activator.CreateInstance(type, context) is Window dialog)) return false;
                    dialog.Owner = GetActiveWindow();
                    return dialog.ShowDialog();
            }

            ShowError($"Запрашиваемое представление не является \"{aliasDialog}\" не является потомком класса \"Window\"");
            return false;
        }

        /// <summary>
        /// Выполняет переход на указанную страницу (или открывает новое окно) в зависимости от типа представления
        /// </summary>
        /// <param name="aliasView">Название представления</param>
        /// <param name="context">Объект модели представления</param>
        /// <param name="findDuplicate">Флаг, указывающий на необходимость искать запрошенное окно среди открытых.
        /// Если запрошенное окно не будет найдено среди уже открытых - будет открыто новое окно.
        /// Флаг игнорируется, если в качестве представления выступает страница (Page)</param>
        public void Navigate(string aliasView, INotifyPropertyChanged context, bool findDuplicate = false)
        {
            aliasView = aliasView.ToLower();

            if (!GetView(aliasView, out var view))
            {
                ShowError($"Не удалось найти запрашиваемое представление \"{aliasView}\"");
                return;
            }

            switch (view)
            {
                case Window win:
                    if (findDuplicate && ShowDuplicate(aliasView, out var dialog))
                    {
                        dialog.Activate();
                        dialog.WindowState = WindowState.Normal;
                        win.Close();
                        return;
                    }

                    win.Name = aliasView;
                    win.DataContext = context;
                    win.Show();
                    return;

                case Page page:

                    if (GetActiveWindow() is NavigationalWindow)
                    {
                        var navigatedWindow = (NavigationalWindow)GetActiveWindow();
                        if (navigatedWindow.IsLoaded)
                        {
                            Service = navigatedWindow.Frame.NavigationService;
                        }
                        else
                        {
                            navigatedWindow.Loaded += (s, e) =>
                            {
                                Service = navigatedWindow.Frame.NavigationService;
                                Service.Navigate(page, context);
                            };
                        }

                        page.DataContext = context;
                        Service.Navigate(page, context);
                        return;
                    }

                    ShowError($"Не удалось отобразить представление {aliasView}");
                    return;
            }

        }

        /// <summary>
        /// Выполняет переход на указанную страницу (или открывает новое окно) в зависимости от типа представления
        /// </summary>
        /// <param name="aliasView">Название представления</param>
        /// <param name="aliasContext">Название модели представления</param>
        /// <param name="findDuplicate">Флаг, указывающий на необходимость искать запрошенное окно среди открытых.
        /// Если запрошенное окно не будет найдено среди уже открытых - будет открыто новое окно.
        /// Флаг игнорируется, если в качестве представления выступает страница (Page)</param>
        public void Navigate(string aliasView, string aliasContext, bool findDuplicate = false)
        {
            aliasView = aliasView.ToLower();

            if (!GetView(aliasView, out var view))
            {
                ShowError($"Не удалось найти запрашиваемое представление \"{aliasView}\"");
                return;
            }

            if (!GetContext(aliasContext, out var context))
            {
                ShowError($"Не удалось найти контекст для представления {aliasContext}");
                return;
            }

            switch (view)
            {
                case Window win:

                    if (findDuplicate && ShowDuplicate(aliasView, out var dialog))
                    {
                        dialog.Activate();
                        dialog.WindowState = WindowState.Normal;
                        win.Close();
                        return;
                    }

                    win.Name = aliasView;
                    win.DataContext = context;
                    win.Show();
                    return;

                case Page page:

                    if (GetActiveWindow() is NavigationalWindow)
                    {
                        var navigatedWindow = (NavigationalWindow)GetActiveWindow();
                        if (navigatedWindow.IsLoaded)
                        {
                            Service = navigatedWindow.Frame.NavigationService;
                        }
                        else
                        {
                            navigatedWindow.Loaded += (s, e) =>
                            {
                                Service = navigatedWindow.Frame.NavigationService;
                                Service.Navigate(page, context);
                            };
                        }

                        Service.Navigate(page, context);
                        return;
                    }

                    ShowError($"Не удалось отобразить представление {aliasView}");
                    return;
            }
        }

        /// <summary>
        /// Выполняет переход на указанную страницу (или открывает новое окно) в зависимости от типа представления
        /// </summary>
        /// <param name="aliasView">Название представления</param>
        /// <param name="findContext">Флаг, указывающий на необходимость автоматического поиска модели представления</param>
        /// <param name="findDuplicate">Флаг, указывающий на необходимость искать запрошенное окно среди открытых.
        /// Если запрошенное окно не будет найдено среди уже открытых - будет открыто новое окно.
        /// Флаг игнорируется, если в качестве представления выступает страница (Page)</param>
        public void Navigate(string aliasView, bool findContext = true, bool findDuplicate = false)
        {
            aliasView = aliasView.ToLower();

            if (!GetView(aliasView, out var view))
            {
                ShowError($"Не удалось найти запрашиваемое представление \"{aliasView}\"");
                return;
            }

            object context = default;

            if (findContext)
            {
                if (!GetContext(aliasView, out context))
                {
                    ShowError($"Не удалось найти контекст для представления {aliasView}");
                    return;
                }
            }

            switch (view)
            {
                case Window win:

                    if (findDuplicate && ShowDuplicate(aliasView, out var dialog))
                    {
                        dialog.Activate();
                        dialog.WindowState = WindowState.Normal;
                        win.Close();
                        return;
                    }

                    win.Name = aliasView;
                    win.DataContext = context;
                    win.Show();
                    return;

                case Page page:

                    if (GetActiveWindow() is NavigationalWindow)
                    {
                        var navigatedWindow = (NavigationalWindow)GetActiveWindow();
                        if (navigatedWindow.IsLoaded)
                        {
                            Service = navigatedWindow.Frame.NavigationService;
                        }
                        else
                        {
                            navigatedWindow.Loaded += (s, e) =>
                            {
                                Service = navigatedWindow.Frame.NavigationService;
                                Service.Navigate(page, context);
                            };
                        }

                        Service.Navigate(page, context);
                        return;
                    }

                    ShowError($"Не удалось отобразить представление {aliasView}");
                    return;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="scope">Скоп с представлениями и вью-моделями</param>
        public AppNavigationService(ILifetimeScope scope)
        {
            _scope = scope;
        }

        #endregion

        #region Private methods

        private bool ShowDuplicate(string aliasView, out Window duplicate)
        {
            duplicate = null;
            if (IsWindowOpen<Window>(aliasView))
            {
                var activeWindow = GetActiveWindow();

                var list = Application.Current.Windows.OfType<Window>().Where(w => w.Name == aliasView && w.IsLoaded).ToList();
                switch (list.Count)
                {
                    case 0:
                        return false;
                    case 1:
                        duplicate = list[0];
                        return true;
                }

                var index = list.IndexOf(activeWindow);
                if (index >= 0 && index < list.Count - 1)
                {
                    duplicate = list[index + 1];
                    return true;
                }

                duplicate = list[0];
                return true;
            }

            return false;
        }

        private bool GetView(string alias, out object view)
        {
            if (_scope.TryResolveKeyed(alias.ToLower(), typeof(NavigationalWindow), out view))
            {
                var navigationalWindow = (NavigationalWindow)view;
                navigationalWindow.Name = alias;
                navigationalWindow.Loaded += (s, e) => { Service = navigationalWindow.Frame.NavigationService; };

                return true;
            }

            if (_scope.TryResolveNamed(alias, typeof(Window), out view))
            {
                if (Application.Current.MainWindow == null || Application.Current.Windows.Count == 0)
                {
                    var window = (Window)view;
                    window.Name = alias;
                    Application.Current.MainWindow = window;
                    return true;
                }

                return true;
            }

            return _scope.TryResolveNamed(alias, typeof(Page), out view);
        }

        private bool GetContext(string aliasContext, out object context)
        {
            aliasContext = aliasContext.ToLower();
            context = null;
            if (!_scope.TryResolveKeyed(aliasContext, typeof(INotifyPropertyChanged), out var model)) return false;
            if (!(model is INotifyPropertyChanged changed)) return false;
            context = changed;
            return true;
        }

        private void ShowError(string message)
        {
            var window = GetActiveWindow();
            //if (window is NavigationalWindow navigationalWindow)
            //{
            //    if (navigationalWindow.IsLoaded)
            //    {
            //        Service = navigationalWindow.Frame.NavigationService;
            //        Service.Navigate(new Page404(/*message*/));
            //        return;
            //    }
            //}

            MessageBox.Show(message, "Ошибка навигации!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private Window GetActiveWindow()
        {
            return Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(window => window.IsActive);
        }

        private void NavService_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page))
            {
                return;
            }

            page.DataContext = e.ExtraData;
        }

        private bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
                ? Application.Current.Windows.OfType<T>().Any()
                : Application.Current.Windows.OfType<T>().Any(w => w.Name.ToLower().Equals(name));
        }

        #endregion
    }
}
