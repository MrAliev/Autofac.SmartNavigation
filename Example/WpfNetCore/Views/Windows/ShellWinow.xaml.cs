using System.Windows.Controls;
using Autofac.SmartNavigation.Base;

namespace WpfNetCore.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : NavigationalWindow
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
