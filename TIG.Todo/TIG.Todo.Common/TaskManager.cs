using System;
using System.Net;
using System.Collections.ObjectModel;

namespace TIG.Todo.Common
{
    public class TaskManager
    {
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
