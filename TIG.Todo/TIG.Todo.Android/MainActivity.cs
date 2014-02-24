using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TIG.Todo.Common;

namespace TIG.Todo.Android
{
	[Activity (Label = "TIG.Todo.Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private TaskManager taskManager;
		ListView taskListView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			taskManager = new TaskManager();

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.addButton);
			taskListView = FindViewById<ListView> (Resource.Id.listTasks);

			button.Click += delegate {
				var todoText = FindViewById<EditText> (Resource.Id.todoItemText);
				taskManager.NewTodoItem.Text = todoText.Text;
				taskManager.AddTodoItem();
			};
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			var taskListAdapter = new TaskListAdapter (this, taskManager.TodoItems);
			taskListView.Adapter = taskListAdapter;
		}
	}
}


