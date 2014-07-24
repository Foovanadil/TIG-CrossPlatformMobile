using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;
using Android.App;

namespace TIG.Todo.AndroidApp
{
	[Service]
	[IntentFilter(new[] { 
		CustomActions.TODO_START_LOCATION_MONITORING, 
		CustomActions.TODO_SET_GEOFENCE })]
	public class GeofencingHelper : Service, Android.Locations.ILocationListener
		//, IGooglePlayServicesClientConnectionCallbacks
		//, IGooglePlayServicesClientOnConnectionFailedListener
		//, LocationClient.IOnAddGeofencesResultListener
	{
		public override Android.OS.IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override void OnStart (Intent intent, int startId)
		{
			if (intent.Action == CustomActions.TODO_START_LOCATION_MONITORING)
			{
				IsPlayServiceAvailable();
				InitializeLocationManager();
			}
			if (intent.Action == CustomActions.TODO_SET_GEOFENCE)
			{
				SetFence();
			}
		}
			
		bool IsPlayServiceAvailable ()
		{
			var resultCode = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
			// If Google Play services is available
			return (ConnectionResult.Success == resultCode);
		}

		private string _locationProvider;
		private LocationManager _locationManager;

		void InitializeLocationManager()
		{
			_locationManager = (LocationManager)GetSystemService(LocationService);
			var criteriaForLocationService = new Criteria { Accuracy = Accuracy.Fine };
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);
			_locationProvider = acceptableLocationProviders.FirstOrDefault() ?? "";
			_locationManager.RequestLocationUpdates(_locationProvider, 5000, 100, this);
		}

		#region ILocationListener implementation

		public void OnLocationChanged(Location location)
		{
			currentLatitude = location.Latitude;
			currentLongitude = location.Longitude;
		}
		public void OnProviderDisabled(string provider)
		{
			// throw new NotImplementedException ();
		}

		public void OnProviderEnabled(string provider)
		{
			// throw new NotImplementedException ();
		}

		public void OnStatusChanged(string provider, Availability status, Android.OS.Bundle extras)
		{
			// throw new NotImplementedException ();
		}

		#endregion

		private double currentLatitude;
		private double currentLongitude;
		private const float radiusInMeters = 100.0f;
		List<Location> fences = new List<Location>(); 

		public void SetFence()
		{
			bool isWithinRadius = false;
			foreach (var fence in fences)
			{
				float[] results = new float[1];
				Location.DistanceBetween(fence.Latitude, fence.Longitude, currentLatitude, currentLongitude, results);
				float distanceInMeters = results[0];
				if (distanceInMeters < radiusInMeters)
				{
					isWithinRadius = true;
					break;
				}
			}

			if (!isWithinRadius)
			{
				var intent = new Intent(CustomActions.TODO_WITHIN_PROXIMITY);
				PendingIntent pendingIntent = PendingIntent.GetService(this, 0, intent, 
					PendingIntentFlags.UpdateCurrent);
				_locationManager.AddProximityAlert(currentLatitude, currentLongitude, 
					radiusInMeters, -1, pendingIntent);
			}
		}
	}
}

