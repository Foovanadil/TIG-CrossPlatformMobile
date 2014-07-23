using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TIG.Todo.Common;
using Android.Content.PM;
using TIG.Todo.Common.SQLite;

namespace TIG.Todo.AndroidApp
{
	[Activity (Label = "TIG.Todo.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
	public class MainActivity : Activity
	{
		private TaskManager taskManager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var sqliteFilename = "TaskDB.db3";
			string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(libraryPath, sqliteFilename);
			var conn = new Connection(path);

			var taskRepository = new TaskRepository(conn, "");
			taskManager = new TaskManager(taskRepository);

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
			var taskListAdapter = new TaskListAdapter (this, taskManager);
			taskListView.Adapter = taskListAdapter;

			Intent intent = new Intent(this, typeof(GeofencingHelper));
			StartService(intent);
		}

//		protected override void OnResume ()
//		{
//			base.OnResume ();
//		}
	}
}


