using System;

namespace WpfFramework.Interfaces
{
    public interface IDataService
    {
        event EventHandler<int> ScoreChanged;
        event EventHandler<string> SomeTextChanged;
        int Score { get; }
        string SomeText { get; set; }
        void IncrementScore();
    }
}
