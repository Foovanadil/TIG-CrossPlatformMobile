using System;
using System.Drawing;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TIG.Todo.Common;
using TIG.Todo.Common.SQLite;

namespace TIG.Todo.iOS
{
	public partial class TIG_Todo_iPhoneViewController : UIViewController
	{
		private TaskManager taskManager;

		public TIG_Todo_iPhoneViewController () : base ("TIG_Todo_iPhoneViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			var sqliteFilename = "TaskDB.db3";
			// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
			// (they don't want non-user-generated data in Documents)
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			var conn = new Connection(path);
			var taskRepository = new TaskRepository(conn, "");
			taskManager = new TaskManager(taskRepository);
			base.ViewDidLoad ();

			addButton.TouchUpInside += (object sender, EventArgs e) => {
				AddNewTask ();
			};

			newTaskText.EditingDidEnd += (object sender, EventArgs e) => {
				AddNewTask ();
			};
			newTaskText.Delegate = new CatchEnterDelegate ();

			tableTasks.Source = new TasksTableViewSource (tableTasks, taskManager);
		}

		void AddNewTask ()
		{
			taskManager.NewTodoItem.Text = newTaskText.Text;
			taskManager.AddTodoItem ();
			newTaskText.Text = "";
		}
	}
}

