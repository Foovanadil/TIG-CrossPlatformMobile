using System;
using System.Net;
using System.Collections.ObjectModel;

namespace TIG.Todo.Common
{
    public class TaskManager
    {
        //TODO: 3.0 - Add two constructors
        //  The first ctor should be a default ctor and does nothing
        //  The second ctor should accept an array of TodoItems
        //      Set the TodoItems to a new instance of ObservableCollection passing in items to it's ctor.
        
        #region Solution 3.0
        //public TaskManager()
        //{

        //}

        //public TaskManager(TodoItem[] items)
        //{
        //    if (items != null)
        //    {
        //        TodoItems = new ObservableCollection<TodoItem>(items);
        //    }
        //} 
        #endregion

        private ObservableCollection<TodoItem> _todoItems = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> TodoItems
        {
            get { return _todoItems; }
            set { _todoItems = value; }
        }

        private TodoItem _newTodoItem = new TodoItem();
        public TodoItem NewTodoItem
        {
            get { return _newTodoItem; }
            set { _newTodoItem = value; }
        }

        public void AddTodoItem()
        {
            TodoItems.Add(NewTodoItem);
            NewTodoItem = new TodoItem();
        }

        public void RemoveItem(TodoItem todoItem)
        {
            TodoItems.Remove(todoItem);
        }
    }
}
