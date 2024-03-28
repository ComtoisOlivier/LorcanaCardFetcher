using System.Windows.Input;

namespace WPFMtgApp.BusinessLogic
{
    public class RelayCommand(Action execute, Func<bool> canExecute) : ICommand
    {
        private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        private readonly Func<bool> _canExecute = canExecute;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
