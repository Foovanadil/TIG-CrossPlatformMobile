using System.Collections.Generic;
using TIG.Todo.Common.SQLiteBase;

namespace TIG.Todo.Common
{
	public class TaskRepository
	{
		private TaskDatabase db;
		protected static string dbLocation;

		public TaskRepository(SQLiteConnection conn, string dbLocation)
		{
			db = new TaskDatabase(conn, dbLocation);
		}

		public TodoItem GetTask(int id)
		{
			return db.GetItem<TodoItem>(id);
		}

		public IEnumerable<TodoItem> GetTasks()
		{
			return db.GetItems<TodoItem>();
		}

		public int SaveTask(TodoItem item)
		{
			return db.SaveItem<TodoItem>(item);
		}

		public int DeleteTask(int id)
		{
			return db.DeleteItem<TodoItem>(id);
		}
	}
}

