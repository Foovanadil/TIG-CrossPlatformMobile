using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Gms.Location;
using Android.Locations;


namespace TIG.Todo.AndroidApp
{
	[Service]
	[IntentFilter(new[] { CustomActions.TODO_WITHIN_PROXIMITY })]
	public class GeofenceIntentService : IntentService
	{

		protected override void OnHandleIntent (Intent intent)
		{
			bool isEntering= intent.GetBooleanExtra(LocationManager.KeyProximityEntering, false);
			NotificationCompat.Builder builder = new NotificationCompat.Builder (this);
			var notification = builder
				.SetContentTitle("TODO")
				.SetContentText((isEntering? "Entering" : "Exiting") + " fence")
				.Build();
			var notificationService = (NotificationManager)GetSystemService (Context.NotificationService);
			notificationService.Notify (1, notification);
			int i = 17;
			//TODO: check LocationManager.KEY_PROXIMITY
		}




	}
}