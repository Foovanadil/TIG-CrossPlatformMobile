using System;
using TIG.Todo.Common;
using Android.Widget;
using Android.App;
using System.Collections.ObjectModel;
using Android.Views;

namespace TIG.Todo.Android
{
	public class TaskListAdapter : BaseAdapter<TodoItem> {
		protected Activity context = null;
		protected ObservableCollection<TodoItem> tasks;

		public TaskListAdapter (Activity context, ObservableCollection<TodoItem> tasks) : base ()
		{
			//TODO: 4.0 - Implement TaskListAdapter ctor
			//	Save the Activity named context in a private field named context
			//	Save the OC<TodoItem> named tasks in a private field named tasks
			//	Subscribe to tasks CollectionChangedEvent - in the event delegate call this.NotifyDataSetChanged();
		}

		//TODO: 4.1 - Override the class indexer: public override TodoItem this[int position]
		//	in the get return tasks[position]; (The get Looks like C# property syntaxt get{ return [logic]; })

		//TODO: 4.2 - Override the method GetItemId(int position)
		//	return position;

		//TODO: 4.3 - Override the Count property
		//	return tasks.Count;

		//TODO: 4.4 - Override the GetView method
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			//TODO: 4.5 - Using position index into tasks to retrieve the item desired

			//TODO: 4.6 - Implement Resources/layout/TodoItemView.axml...
			//	Open TodoItemView.axml - On the bottom of the view click "Source" to view the xml representation of the UI.

			//TODO: 6.0 - Now that TodoItemView.axml is implemented...

			//TODO: 6.1 - Inflate a TodoItemView for the retrieved item
			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()

			//Cannot just return convertView if it exists. When items are removed the views need to be
			//	updated otherwise the UI data will be wrong. Adding items after removal also results in
			//	an incorrect UI.
			var view = (convertView ?? 
				context.LayoutInflater.Inflate(
					Resource.Layout.TodoItemView,
					parent, 
					false));

			//TODO: 6.2 - Find the TextView named textView (HINT: see TODO: 2.1)
			//	Call SetText() on textView passing parameters item.Text and TextView.BufferType.Normal
			//	Subscribe to textView.TextChanged
			//	In the event delegate set item.Text with textView.Text

			//TODO: 6.3 - Find the CheckBox named checkBoxDone
			//	Set checkBoxDone.Checked with item.IsCompleted
			//	Subscribe to checkBoxDone.CheckChanged
			//	In the event delegate set item.IsCompleted with checkBoxDone.Checked

			//TODO: 6.4 - Find the Button named deleteButton
			//	Subscribe to deleteButton.Click
			//	In the event delegate call tasks.Remove(item)

			//Finally return the view
			return view;
		}

	}
}

