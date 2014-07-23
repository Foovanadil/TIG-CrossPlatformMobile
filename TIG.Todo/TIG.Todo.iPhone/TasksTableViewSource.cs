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
		private readonly TaskManager _taskManager;

		public TasksTableViewSource (UITableView tableView, TaskManager taskManager)
		{
			_taskManager = taskManager;
			_taskManager.TodoItems.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
//				if (e.OldItems != null)
//				{
//					NSIndexPath[] oldPaths = e.OldItems.OfType<TodoItem>().Select(oi => NSIndexPath.FromRowSection(tasks.IndexOf(oi), 0)).ToArray();
//					tableView.DeleteRows(oldPaths, UITableViewRowAnimation.Top);
//				}

				if (e.NewItems != null)
				{
					var newPaths = e.NewItems.OfType<TodoItem>().Select(ni => NSIndexPath.FromRowSection(_taskManager.TodoItems.Count - 1, 0)).ToArray();
					tableView.InsertRows(newPaths, UITableViewRowAnimation.Top);
				}
			};
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _taskManager.TodoItems.Count;
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) {
				_taskManager.RemoveItem(_taskManager.TodoItems[indexPath.Row]);
				tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Top);
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			TodoItem todoItem = _taskManager.TodoItems[indexPath.Row];
			_taskManager.ToggleItemCompletion(todoItem);
			var cell = tableView.CellAt (indexPath);
			cell.Accessory = todoItem.IsCompleted ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
			cell.TextLabel.TextColor = todoItem.IsCompleted ? UIColor.Green : UIColor.Black;
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
			TodoItem todoItem = _taskManager.TodoItems[indexPath.Row];
			cell.TextLabel.Text = todoItem.Text;
			cell.Accessory = todoItem.IsCompleted ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
			cell.TextLabel.TextColor = todoItem.IsCompleted ? UIColor.Green : UIColor.Black;
			return cell;
		}
	}
}

