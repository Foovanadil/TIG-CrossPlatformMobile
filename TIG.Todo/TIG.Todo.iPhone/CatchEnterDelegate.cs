using System;
using MonoTouch.UIKit;

namespace TIG.Todo.iOS
{
	public class CatchEnterDelegate : UITextFieldDelegate
	{
		public CatchEnterDelegate ()
		{
		}

		public override bool ShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}
	}
}

