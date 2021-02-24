using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfFramework.Commands
{
    public class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action execute, Func<bool> canExecute = null)
            : this(p => execute(), canExecute is null ? (Func<object, bool>)null : p => canExecute())
        {

        }

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        protected override bool CanExecute(object p) => _canExecute?.Invoke(p) ?? true;

        protected override void Execute(object p) => _execute(p);
    }

    public class LambdaCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _execute;
        private readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors

        public LambdaCommand(Action<T> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Implementation

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                _execute(typedParameter);
            }
        }

        #endregion
    }
}
