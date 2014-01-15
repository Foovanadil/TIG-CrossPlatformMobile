using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TIG.Todo.Common
{
    public class TodoListViewModel : Bindable
    {
        private ObservableCollection<TodoItem> _todoItems = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> TodoItems
        {
            [DebuggerStepThrough]
            get { return _todoItems; }
            set
            {
                if (value == _todoItems)
                    return;

                _todoItems = value;
                OnPropertyChanged();
            }
        }

        private TodoItem _newTodoItem = new TodoItem();
        public TodoItem NewTodoItem
        {
            [DebuggerStepThrough]
            get { return _newTodoItem; }
            set
            {
                if (value == _newTodoItem)
                    return;

                _newTodoItem = value;
                OnPropertyChanged();
            }
        }

        private DelegateCommand<object> _addCommand;
        public DelegateCommand<object> AddCommand
        {
            get
            {
                return _addCommand = _addCommand ?? new DelegateCommand<object>(AddExecutedHandler, AddCanExecuteHandler);
            }
        }
        private bool AddCanExecuteHandler(object obj)
        {
            return true;
        }
        private void AddExecutedHandler(object obj)
        {
            TodoItems.Add(NewTodoItem);
            NewTodoItem = new TodoItem();
        }

        private DelegateCommand<TodoItem> _deleteCommand;
        public DelegateCommand<TodoItem> DeleteCommand
        {
            get
            {
                return _deleteCommand = _deleteCommand ?? new DelegateCommand<TodoItem>(DeleteExecutedHandler, DeleteCanExecuteHandler);
            }
        }
        private bool DeleteCanExecuteHandler(TodoItem todoItem)
        {
            return true;
        }
        private void DeleteExecutedHandler(TodoItem todoItem)
        {
            TodoItems.Remove(todoItem);
        }
    }

}