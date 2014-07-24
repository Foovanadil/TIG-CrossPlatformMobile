using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TIG.Todo.AndroidApp
{
	public static class CustomActions
	{
		public const string TODO_WITHIN_PROXIMITY = "com.sdtig.Todo.WITHIN_PROXIMITY";
		public const string TODO_START_LOCATION_MONITORING = "com.sdtig.Todo.START_LOCATION";
		public const string TODO_SET_GEOFENCE = "com.sdtig.Todo.SET_GEOFENCE";
	}
}