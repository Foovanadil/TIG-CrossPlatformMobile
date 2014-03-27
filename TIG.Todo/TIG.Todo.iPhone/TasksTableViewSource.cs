using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using TIG.Todo.Common;

namespace TIG.Todo.iOS
{
	public class TasksTableViewSource : UITableViewSource
	{
		ObservableCollection<TodoItem> tasks;

		public TasksTableViewSource (UITableView tableView, ObservableCollection<TodoItem> tasks)
		{
			this.tasks = tasks;
			tasks.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
//				if (e.OldItems != null)
//				{
//					NSIndexPath[] oldPaths = e.OldItems.OfType<TodoItem>().Select(oi => NSIndexPath.FromRowSection(tasks.IndexOf(oi), 0)).ToArray();
//					tableView.DeleteRows(oldPaths, UITableViewRowAnimation.Top);
//				}

				if (e.NewItems != null)
				{
					var newPaths = e.NewItems.OfType<TodoItem>().Select(ni => NSIndexPath.FromRowSection(tasks.Count-1, 0)).ToArray();
					tableView.InsertRows(newPaths, UITableViewRowAnimation.Top);
				}
			};
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return tasks.Count;
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) {
				tasks.RemoveAt (indexPath.Row);
				tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Top);
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var newIsCompleted = !tasks [indexPath.Row].IsCompleted;
			tasks [indexPath.Row].IsCompleted = newIsCompleted;
			var cell = tableView.CellAt (indexPath);
			cell.Accessory = newIsCompleted ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
			cell.TextLabel.TextColor = newIsCompleted ? UIColor.Green : UIColor.Black;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			const string cellIdentifier = "TableCell";
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			}
			cell.TextLabel.Text = tasks[indexPath.Row].Text;
			return cell;
		}
	}
}

