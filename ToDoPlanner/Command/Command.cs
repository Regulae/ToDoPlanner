using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoPlanner.Command
{
    // @TODO Regula 08.12.19: Implement Commands for the EditView
    /*
    class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;

        public DelegateCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _executeAction(parameter);

        public event EventHandler CanExecuteChanged;
    
    }*/
}