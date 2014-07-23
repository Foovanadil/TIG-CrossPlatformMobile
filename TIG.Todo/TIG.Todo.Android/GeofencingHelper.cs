using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Gms;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;
using Android.App;

namespace TIG.Todo.AndroidApp
{
	[Service]
	public class GeofencingHelper : Service, Android.Locations.ILocationListener, IGooglePlayServicesClientConnectionCallbacks,
	IGooglePlayServicesClientOnConnectionFailedListener, LocationClient.IOnAddGeofencesResultListener
	{
		Android.Content.Intent _intent;

		public override void OnCreate ()
		{
			base.OnCreate ();
		}
		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			return base.OnStartCommand (intent, flags, startId);
		}

		public override Android.OS.IBinder OnBind (Android.Content.Intent intent)
		{
			return null;
		}

		public override void OnStart (Android.Content.Intent intent, int startId)
		{
			_intent = intent;
			PlayServiceAvailable ();
			InitializeLocationManager ();
		}
			
		bool PlayServiceAvailable ()
		{
			var resultCode = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
			//			// If Google Play services is available
			if (ConnectionResult.Success == resultCode) {
				// Continue
				return true;
				// Google Play services was not available for some reason
			}
			else {
				// Get the error code
				// Get the error dialog from Google Play services
				return false;
			}
		}

		private string _locationProvider;
		private LocationManager _locationManager;

		void InitializeLocationManager()
		{
			_locationManager = (LocationManager)this.GetSystemService(Context.LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = String.Empty;
			}


			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

		}

		private double Lat;
		private double Long;
		private LocationClient client;

		void SetFence()
		{
			client = new LocationClient (this, this,this);
			client.Connect ();
		}


		public void OnConnected (Android.OS.Bundle connectionHint)
		{
			var fence = new GeofenceBuilder ()
				.SetRequestId ("1")
				.SetTransitionTypes (GeofenceConsts.GeofenceTransitionExit)
				.SetCircularRegion (Lat, Long, 100)
				.SetExpirationDuration (99999)
				.Build ();

			var pendingIntent = PendingIntent.GetService (this, 0, new Intent (this, typeof(GeofencingHelper)), PendingIntentFlags.UpdateCurrent);
			client.AddGeofences(new List<IGeofence>() { fence } ,pendingIntent,this);

		}

		public void OnAddGeofencesResult (int statusCode, string[] geofenceRequestIds)
		{
			//throw new NotImplementedException ();
		}

		public void OnDisconnected ()
		{
			//throw new NotImplementedException ();
		}


		public void OnConnectionFailed (ConnectionResult result)
		{
			//throw new NotImplementedException ();
		}

		public void OnProviderDisabled (string provider)
		{
//			throw new NotImplementedException ();
		}

		public void OnProviderEnabled (string provider)
		{
//			throw new NotImplementedException ();
		}

		public void OnStatusChanged (string provider, Availability status, Android.OS.Bundle extras)
		{
//			throw new NotImplementedException ();
		}

		#region ILocationListener implementation

		public void OnLocationChanged (Location location)
		{
			Lat = location.Latitude;
			Long = location.Longitude;
			_locationManager.RemoveUpdates (this);
			SetFence ();
		}

		#endregion

	}
}

