using System.Windows.Controls;

namespace WpfFramework.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ShellWindow
    {
        public ShellWindow()
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
