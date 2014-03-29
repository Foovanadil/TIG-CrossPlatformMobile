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
			//STEP 1: wire up to the tasks.CollectionChanged event. When new items are added
			//add them to the tableView
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			//STEP 2: provide a count of the items in the table view

		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			//STEP 3: Find the Cell that we are asking for and provide the TextLabel.Text
			//for the cell so that the Task we just entered is the text for the tableView cell
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//SETP 4: When the row is selected set the IsCompleted flag on the TodoItem
			//If the ToDo item wasn't completed and we tapped on it, it should now be completed
			//If it was completed then it should be set to IsCompleted = false

			//STEP 5: If the TodoItem is completed, set the cell.Accessory to Checkmark

			//STEP 6: If the TodoItem is completed make the cell.TextLabel.TextColor to green
			//Otherwise set it to black
		}



		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			//STEP 7: If the editing style was delete then delete the task from the task list and delete the 
			//actual table row from the tableView
		}

	}
}

