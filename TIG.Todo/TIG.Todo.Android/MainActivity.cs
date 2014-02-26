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
	//TODO: 0.1 - Double click TIG.Todo.Android project to open it's options. Select "Android Build" and uncheck fast assembly deployment.
	//	(Not doing this may result in failed deployments)

	[Activity (Label = "TIG.Todo.Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//TODO: 1.0 - Create a TaskManager instance and store it in a private field

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//TODO: 1.1 - Create the UI in Resources/layout/Main.axml
			//	Open Main.axml - On the bottom of the view click "Source" to view the xml representation of the UI.

			//TODO: 3.0 - Now that the UI is created...

			//TODO: 3.1 - Find the Button named addButton by passing Resource.Id.addButton to the FindViewById<T> function
			//	store the result in a member variable named button

			//TODO: 3.2 - Subscribe to the button.Click event with an inline delegate
			//	The syntax is delegate{};
			//	Inside the delegate find the EditText control named todoItemText (HINT: see TODO: 2.1)
			//	Access the taskManager private field and set NewTodoItem.Text with the text of todoText (the found EditText)
			//	Call AddTodoItem on taskManager
			//	Clear the text value of todoText by setting it to "";

			//TODO: 3.3 - Find the ListView named listTasks

			//TODO: 3.4 - Implement Adapter/TaskListAdapter.cs used for populating the ListView named listTasks

			//TODO: 7.0 - Now that TaskListAdapter.cs is implemented...

			//TODO: 7.1 - Create an instance of TaskListAdapter with parameters(this, taskManager.TodoItems)

			//TODO: 7.2 - Set the ListView named listTasks Adapter property to the instance of TaskListAdapter
		}

//		protected override void OnResume ()
//		{
//			base.OnResume ();
//		}
	}
}


