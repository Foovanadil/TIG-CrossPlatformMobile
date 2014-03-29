using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TIG.Todo.Common;

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
			taskManager = new TaskManager();
			base.ViewDidLoad ();

			addButton.TouchUpInside += (object sender, EventArgs e) => {
				AddNewTask ();
			};

			newTaskText.EditingDidEnd += (object sender, EventArgs e) => {
				AddNewTask ();
			};
			newTaskText.Delegate = new CatchEnterDelegate ();

			tableTasks.Source = new TasksTableViewSource (tableTasks, taskManager.TodoItems);
		}

		void AddNewTask ()
		{
			taskManager.NewTodoItem.Text = newTaskText.Text;
			taskManager.AddTodoItem ();
			newTaskText.Text = "";
		}
	}
}

