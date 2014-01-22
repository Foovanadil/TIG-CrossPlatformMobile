using System;
using System.Windows.Input;

namespace TIG.Todo.Common
{
    //TODO: 1 - Reading about ICommand and DelegateCommand<T>
    //http://msdn.microsoft.com/en-us/library/system.windows.input.icommand(v=vs.110).aspx
    //http://stackoverflow.com/questions/11960488/any-winrt-icommand-commandbinding-implementaiton-samples-out-there
    public class DelegateCommand<T> : ICommand
    {
        readonly Func<T, bool> canExecute;
        readonly Action<T> executeAction;

        bool canExecuteCache;

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action<T> executeAction)
        {
            this.executeAction = executeAction;
        }

        #region ICommand Members
        
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                bool temp = canExecute((T)parameter);
                if (canExecuteCache != temp)
                {
                    canExecuteCache = temp;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                }
                return canExecuteCache;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
        #endregion

    }
}