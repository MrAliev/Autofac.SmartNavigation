using System.Windows.Controls;

namespace Autofac.SmartNavigation.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page404.xaml
    /// </summary>
    public partial class Page404 : Page
    {
        /// <summary>
        /// Страница с сообщением об ошибке
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public Page404(string message)
        {
            InitializeComponent();
            Message.Text = message;
        }
    }
}
