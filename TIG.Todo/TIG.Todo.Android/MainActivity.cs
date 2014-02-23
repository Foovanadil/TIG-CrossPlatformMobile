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
		int count = 1;

		private TodoListViewModel viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			viewModel = CreateViewModel();

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.tigButton);
			
			button.Click += delegate {

				var todoText = FindViewById<EditText>(Resource.Id.todoItemText);

				viewModel.NewTodoItem.Text = todoText.Text;
				viewModel.AddCommand.Execute(null);


				button.Text = string.Format ("{0} clicks!", count++);
			};
		}

		TodoListViewModel CreateViewModel ()
		{
			return new TodoListViewModel();
		}
	}
}


