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
			// Get our object for position
			var item = tasks[position];			

			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
			var view = (convertView ?? 
				context.LayoutInflater.Inflate(
					global::Android.Resource.Layout.SimpleListItemChecked,
					parent, 
					false)) as CheckedTextView;

			view.SetText (item.Text, TextView.BufferType.Normal);
			view.Checked = item.IsCompleted;

			//Finally return the view
			return view;
		}
	}
}

