using System;
using WpfFramework.Interfaces;

namespace WpfFramework.Services
{
    public class DataProvider : IDataService
    {
        private string _someText;
        public int Score { get; private set; }

        public string SomeText
        {
            get => _someText;
            set
            {
                if(_someText == value) return;
                _someText = value;
                SomeTextChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<int> ScoreChanged;
        public event EventHandler<string> SomeTextChanged;

        public void IncrementScore()
        {
            Score++;
            ScoreChanged?.Invoke(this, Score);
        }
    }
}
