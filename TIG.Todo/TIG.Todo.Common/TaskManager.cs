using System;
using System.Net;
using System.Collections.ObjectModel;

namespace TIG.Todo.Common
{
	public class TaskManager
	{
		private readonly TaskRepository _repository;

		public TaskManager(TaskRepository repository)
		{
			_repository = repository;
			var items = repository.GetTasks();
			_todoItems = new ObservableCollection<TodoItem>(items);
		}

		private ObservableCollection<TodoItem> _todoItems;
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
			_repository.SaveTask(NewTodoItem);
			TodoItems.Add(NewTodoItem);
			NewTodoItem = new TodoItem();
		}

		public void RemoveItem(TodoItem todoItem)
		{
			TodoItems.Remove(todoItem);
			_repository.DeleteTask(todoItem.ID);
		}

		public void ToggleItemCompletion(TodoItem todoItem)
		{
			todoItem.IsCompleted = !todoItem.IsCompleted;
			_repository.SaveTask(todoItem);
		}
	}
}
