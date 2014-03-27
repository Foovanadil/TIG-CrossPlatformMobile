using System;
using TIG.Todo.Common;
using Android.Widget;
using Android.App;
using System.Collections.ObjectModel;
using Android.Views;
using Android.Graphics;

namespace TIG.Todo.Android
{
	public class TaskListAdapter : BaseAdapter<TodoItem> {
		protected Activity context = null;
		protected ObservableCollection<TodoItem> tasks;

		public TaskListAdapter (Activity context, ObservableCollection<TodoItem> tasks) : base ()
		{
			this.context = context;
			this.tasks = tasks;
			tasks.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
				this.NotifyDataSetChanged();
			};
		}

		public override TodoItem this[int position]
		{
			get { return tasks[position]; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count
		{
			get { return tasks.Count; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			// flag so we don't add new event handlers
			bool newView = (convertView == null) ? true : false;

			// Get our object for position
			var item = tasks[position];			

			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
			var view = (convertView ?? 
				context.LayoutInflater.Inflate(
					Resource.Layout.TodoItemView,
					parent, 
					false));

			var textView = (TextView)view.FindViewById (Resource.Id.textView);
			textView.SetText (item.Text, TextView.BufferType.Normal);

			var checkBoxDone = (CheckBox)view.FindViewById (Resource.Id.checkBoxDone);
			checkBoxDone.Checked = item.IsCompleted;

			var deleteButton = (Button)view.FindViewById (Resource.Id.deleteButton);

			// only hookup handlers if this is the first time inflating our TodoItemView.
			if (newView) {
				checkBoxDone.CheckedChange += (object sender, CompoundButton.CheckedChangeEventArgs e) => {
					// figure out item when checkchange happens, not during creation of event handler
					tasks[position].IsCompleted = checkBoxDone.Checked;
					textView.SetTextColor(item.IsCompleted ? Color.Green : Color.WhiteSmoke);
				};

				deleteButton.Click += (object sender, EventArgs e) => {
					// figure out item when click happens, not during creation of event handler
					tasks.Remove(tasks[position]);
				};
			}

			//Finally return the view
			return view;
		}

	}
}

