using WpfFramework.ViewModels;

namespace WpfFramework.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class SecondWindow
    {
        // Привязка модели представления в навигационной системе не устанавливается для диалоговых окон!
        // Чтобы окно открывалось как модальное, и при этом окну требуется вью-модель -
        // нужно запрашивать необходимую вью-модель через конструктор окна
        public SecondWindow(SecondWindowViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
