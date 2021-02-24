using System;
using System.Windows.Input;

namespace WpfFramework.Commands
{
    public abstract class Command : ICommand
    {
        private bool _executable = true;

        public bool Executable
        {
            get => _executable;
            set
            {
                if (_executable == value) return;
                _executable = value;
                ExecutableChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ExecutableChanged;

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => _executable && CanExecute(parameter);

        void ICommand.Execute(object parameter)
        {
            if (!((ICommand)this).CanExecute(parameter)) return;
            Execute(parameter);
        }

        protected virtual bool CanExecute(object p) => true;

        protected abstract void Execute(object p);
    }
}
