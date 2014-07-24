using Android.App;
using Android.Content;

namespace TIG.Todo.AndroidApp
{
	[Service]
	[IntentFilter(new[] { CustomActions.TODO_WITHIN_PROXIMITY })]
	public class GeofenceIntentService : IntentService
	{

		protected override void OnHandleIntent (Intent intent)
		{
			int i = 17;
			//TODO: check LocationManager.KEY_PROXIMITY
		}




	}
}