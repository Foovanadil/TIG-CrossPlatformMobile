using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TIG.Todo.Common
{
    public class TodoListViewModel //TODO: 3.0 - Inherit from Bindable in order get support for INotifyPropertyChanged
    {
        //TODO: 3.1 - Create an ObservableCollection (OC) with generic type TodoItem property named TodoItems that notifies when it has changed (HINT: same process as TODO: 2.1)
        //  Reading about ObservableCollection (OC) http://msdn.microsoft.com/en-us/library/ms668604(v=vs.110).aspx
        //TODO: 3.2 - Create a TodoItem property named NewTodoItem that notifies when it has changed


        //TODO: 3.3 - Create a DelegateCommand with generic type object readonly property named AddCommand
        //  The getter of the AddCommand property should always return the same instance of field _addCommand
        //  (HINT: _addCommand = _addCommand ?? new DelegateCommand<object>(AddExecutedHandler, AddCanExecuteHandler);)
        //TODO: 3.4 - Create the private method bool AddCanExecuteHandler(object obj)
        //  The method should always return true
        //TODO: 3.5 - Create the private method void AddExecutedHandler(object obj)
        //  The method should first add NewTodoItem to the OC TodoItems then set NewTodoItem with a new instance of TodoItem


        //TODO: 3.6 - Create a DelegateCommand with generic type TodoItem readonly property named DeleteCommand
        //  The getter of the DeleteCommand property should always return the same instance of field _deleteCommand
        //  (HINT: TODO: 3.3)
        //TODO: 3.7 - Create the private method bool DeleteCanExecuteHandler(TodoItem todoItem)
        //  The method should always return true
        //TODO: 3.8 - Create the private method void DeleteExecutedHandler(TodoItem todoItem)
        //  The method should remove the todoItem paramerter from the OC TodoItems
    }

}