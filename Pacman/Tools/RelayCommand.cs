using System;
using System.Windows.Input;

namespace Pacman.Tools
{
    internal class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> action, Predicate<object> predicate = null)
        {
            _action = action;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter) => 
            _predicate == null || _predicate(parameter);

        public void Execute(object parameter) => 
            _action?.Invoke(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _action;
        private readonly Predicate<object> _predicate;
    }
}