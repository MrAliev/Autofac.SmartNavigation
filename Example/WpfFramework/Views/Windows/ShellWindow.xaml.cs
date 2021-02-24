using System.Windows.Controls;

namespace WpfFrameworkTemplate.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class SecondNavigationWindow
    {
        public SecondNavigationWindow()
        {
            InitializeComponent();
        }

        public override Frame Frame
        {
            get => ShellWindowFrame;
            set => ShellWindowFrame = value;
        }
    }
}
