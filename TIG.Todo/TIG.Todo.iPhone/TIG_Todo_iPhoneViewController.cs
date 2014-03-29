using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TIG.Todo.Common;

namespace TIG.Todo.iOS
{
	public partial class TIG_Todo_iPhoneViewController : UIViewController
	{
		public TIG_Todo_iPhoneViewController () : base ("TIG_Todo_iPhoneViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			//STEP 1 : Create a task Manager

			base.ViewDidLoad ();

			//STEP 2: Hooks up the addButton TouchUpInside handler so that new tasks are added on TouchUpInside

			//STEP 3: Hook up the tableTasks View Source

			//OPTIONAL STEP 4: Hook up the EditingDidEnd handler to support Enter key press

			//OPTIONAL STEP 4: Wire up newTaskText Delegate to use CatchEnterDelegate
		}	
	}
}

