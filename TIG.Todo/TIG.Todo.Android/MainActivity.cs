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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			taskManager = new TaskManager();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.addButton);

			button.Click += delegate {
				var todoText = FindViewById<EditText> (Resource.Id.todoItemText);
				taskManager.NewTodoItem.Text = todoText.Text;
				taskManager.AddTodoItem();
				todoText.Text = "";
			};

			var taskListView = FindViewById<ListView> (Resource.Id.listTasks);
			var taskListAdapter = new TaskListAdapter (this, taskManager.TodoItems);
			taskListView.Adapter = taskListAdapter;
		}

//		protected override void OnResume ()
//		{
//			base.OnResume ();
//		}
	}
}


