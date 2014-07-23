using System.Collections.ObjectModel;
using System.Diagnostics;
using TIG.Todo.Common;
using TIG.Todo.Common.SQLite;

namespace TIG.Todo.WindowsPhone8
{
    public class TodoListViewModel : Bindable
    {
        private readonly TaskManager taskManager;

        //TODO: 4.0 - Add 2 new ctors and instantiate taskManager in each.
        //  Don't create taskManager inline.
        //  First new ctor is a default ctor and instantiates taskManager
        //  Second new ctor accepts an array of TodoItems and instantiates taskManager passing its ctor the items

        public TodoListViewModel()
        {
            var taskRepository = new TaskRepository(new Connection("TaskDB.db3"), "");
            taskManager = new TaskManager(taskRepository);
        }

        //public TodoListViewModel(TodoItem[] items)
        //{
        //    taskManager = new TaskManager(items);
        //} 

        public ObservableCollection<TodoItem> TodoItems
        {
            get { return taskManager.TodoItems; }
        }

        public TodoItem NewTodoItem
        {
            get { return taskManager.NewTodoItem; }
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
            taskManager.AddTodoItem();
            OnPropertyChanged("NewTodoItem");
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
            taskManager.RemoveItem(todoItem);
        }
    }

}