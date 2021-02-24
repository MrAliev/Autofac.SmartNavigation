using Autofac.SmartNavigation.Base;
using WpfFramework.Interfaces;

namespace WpfFramework.ViewModels
{
    public class Page1ViewModel : BaseVM
    {
        private readonly IDataService _dataProvider;
        private int _score;
        
        public string ViewModelName { get; } = nameof(Page1ViewModel);

        public int Score
        {
            get => _score;
            set => Set(ref _score, value);
        }

        public string SomeText => _dataProvider.SomeText;
       
        public Page1ViewModel(IDataService dataProvider)
        {
            _dataProvider = dataProvider;
            _dataProvider.ScoreChanged += (s, e) => Score = e;
            _dataProvider.SomeTextChanged += (s, e) => OnPropertyChanged(nameof(SomeText));
            Score = dataProvider.Score;
        }

        public Page1ViewModel() { }
    }
}
