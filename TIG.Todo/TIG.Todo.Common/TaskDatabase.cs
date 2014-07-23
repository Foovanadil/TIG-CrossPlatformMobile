using System;
using System.Linq;
using System.Collections.Generic;
using TIG.Todo.Common.SQLiteBase;

namespace TIG.Todo.Common
{
	/// <summary>
	/// TaskDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
	/// It contains methods for retrieval and persistance as well as db creation, all based on the 
	/// underlying ORM.
	/// </summary>
	public class TaskDatabase 
	{
		static object lockObj = new object ();

        SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
        public TaskDatabase(SQLiteConnection conn, string path)
		{
            database = conn;
			// create the tables
            database.CreateTable<TodoItem>();
		}
		
		public IEnumerable<T> GetItems<T> () where T : IHaveID, new ()
		{
            lock (lockObj) {
                return (from i in database.Table<T>() select i).ToList();
            }
		}

		public T GetItem<T> (int id) where T : IHaveID, new ()
		{
            lock (lockObj) {
                return database.Table<T>().FirstOrDefault(x => x.ID == id);
                // Following throws NotSupportedException - thanks aliegeni
                //return (from i in Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();
            }
		}

		public int SaveItem<T> (T item) where T : IHaveID
		{
            lock (lockObj) {
                if (item.ID != 0) {
                    database.Update(item);
                    return item.ID;
                } else {
                    return database.Insert(item);
                }
            }
		}
		
		public int DeleteItem<T>(int id) where T : IHaveID, new ()
		{
            lock (lockObj) {
                return database.Delete<T>(new T() { ID = id });
            }
		}
	}
}