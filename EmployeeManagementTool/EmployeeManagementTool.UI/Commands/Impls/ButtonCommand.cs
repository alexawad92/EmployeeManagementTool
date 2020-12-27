using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace EmployeeManagementTool.Commands.Impls
{
    public class ButtonCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<bool> _canExecuteFunc;

        /// <summary>
        ///     Default constructor that utilizes ICommand interface.
        /// </summary>
        /// <param name="executeAction"></param>
        /// <param name="canExecuteFunc"></param>
        public ButtonCommand(Action<object> executeAction, Func<bool> canExecuteFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc.Invoke();
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke(parameter);
        }
        #endregion
    }
}
