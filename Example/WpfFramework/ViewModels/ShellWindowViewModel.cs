using System.Windows.Input;
using Autofac.SmartNavigation.Base;
using Autofac.SmartNavigation.Interfaces;
using WpfFramework.Commands;
using WpfFramework.Interfaces;

namespace WpfFramework.ViewModels
{
    public class ShellWindowViewModel : BaseVM
    {
        #region Private fields

        private readonly IDataService _data;
        private readonly INavigationService _navigation;
        
        private bool _findContext;
        private bool _findDuplicateView;
        private string _aliasView;
        private string _aliasContext;

        #endregion

        #region Public Properties

        public string Title { get; } = "Навигационное окно";
        public string ViewModelName { get; } = nameof(ShellWindowViewModel);
        public ICommand NavigateCommand { get; private set; }
        public ICommand NavigateDialogCommand { get; private set; }

        public int Score => _data.Score;

        public string SomeText
        {
            get => _data.SomeText;
            set => _data.SomeText = value;
        }

        public bool FindContext
        {
            get => _findContext;
            set => Set(ref _findContext, value);
        }

        public bool FindDuplicateView
        {
            get => _findDuplicateView;
            set => Set(ref _findDuplicateView, value);
        }

        public string AliasView
        {
            get => _aliasView;
            set => Set(ref _aliasView, value);
        }

        public string AliasContext
        {
            get => _aliasContext;
            set => Set(ref _aliasContext, value);
        }

        #endregion

        #region Constructors

        public ShellWindowViewModel(IDataService data, INavigationService navigation)
        {
            _data = data;
            _navigation = navigation;

            DataInitialize();
            CommandsInitialize();
        }

        public ShellWindowViewModel() { }

        #endregion

        #region Private Methods

        private void CommandsInitialize()
        {
            NavigateCommand = new LambdaCommand(() =>
            {
                if (!FindContext)
                {
                    if (string.IsNullOrEmpty(AliasContext))
                    {
                        _navigation.Navigate(AliasView, FindContext, FindDuplicateView);
                        return;
                    }

                    _navigation.Navigate(AliasView, AliasContext, FindDuplicateView);
                    _data.IncrementScore();
                    return;
                }

                if (string.IsNullOrEmpty(AliasView))
                {
                    AliasView = "Введите имя представления !!!";
                    return;
                }

                _navigation.Navigate(AliasView, FindContext, FindDuplicateView);
                _data.IncrementScore();
            });

            NavigateDialogCommand = new LambdaCommand(() =>
            {
                if (string.IsNullOrEmpty(AliasView))
                {
                    AliasView = "Введите имя представления !!!";
                    return;
                }

                if (!FindContext)
                {
                    if (string.IsNullOrEmpty(AliasContext))
                    {
                        _navigation.NavigateDialog(AliasView, FindContext);
                        return;
                    }

                    _navigation.NavigateDialog(AliasView, AliasContext);
                    _data.IncrementScore();
                    return;
                }
                
                _navigation.NavigateDialog(AliasView);
                _data.IncrementScore();
            });
        }

        private void DataInitialize()
        {
            FindContext = true;
            FindDuplicateView = false;
            SomeText = _data.SomeText;

            _data.ScoreChanged += (s, e) => { OnPropertyChanged(nameof(Score)); };
            _data.SomeTextChanged += (s, e) => { OnPropertyChanged(nameof(SomeText)); };
        }

        #endregion
    }
}
