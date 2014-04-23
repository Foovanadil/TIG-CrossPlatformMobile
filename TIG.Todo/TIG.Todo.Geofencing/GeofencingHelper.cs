using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.ApplicationModel.Background;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Core;

namespace TIG.Todo.Geofencing
{
    public class GeofencingHelper
    {
        private static Geolocator geolocator;
        private static CancellationTokenSource cts;
        private static DeviceAccessInformation accessInfo;
        private static Geoposition currentLocation;

        private static async Task<bool> Initialize()
        {
            bool initializedSuccessfully = false;

            accessInfo = DeviceAccessInformation.CreateFromDeviceClass(DeviceClass.Location);
            try
            {
                if (geolocator == null)
                {
                    geolocator = new Geolocator();
                }

                // Get cancellation token
                cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                currentLocation = await geolocator.GetGeopositionAsync().AsTask(token);

                // other initialization for your app could go here

                accessInfo.AccessChanged += OnAccessChanged;
                GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
                initializedSuccessfully = true;
            }
            catch (UnauthorizedAccessException)
            {
                if (DeviceAccessStatus.DeniedByUser == accessInfo.CurrentStatus)
                {
                    MessageBox.Show("Location has been disabled by the user. Enable access through the settings charm.");
                }
                else if (DeviceAccessStatus.DeniedBySystem == accessInfo.CurrentStatus)
                {
                    MessageBox.Show("Location has been disabled by the system. The administrator of the device must enable location access through the location control panel.");
                }
                else if (DeviceAccessStatus.Unspecified == accessInfo.CurrentStatus)
                {
                    MessageBox.Show("Location has been disabled by unspecified source. The administrator of the device may need to enable location access through the location control panel, then enable access through the settings charm.");
                }
            }
            catch (TaskCanceledException)
            {
                // task cancelled
            }
            catch (Exception)
            {
                if (geolocator.LocationStatus == PositionStatus.Disabled)
                {
                    // On Windows Phone, this exception will be thrown when you call 
                    // GetGeopositionAsync if the user has disabled locaton in Settings.
                    MessageBox.Show("Location has been disabled in Settings.");
                }
            }
            finally
            {
                cts = null;
            }

            return initializedSuccessfully;
        }

        private static void OnAccessChanged(DeviceAccessInformation sender, DeviceAccessChangedEventArgs args)
        {
            if (Dispatcher != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    OnAccessChanged(args);
                });
            }
            else
            {
                OnAccessChanged(args);
            }
        }

        private static void OnAccessChanged(DeviceAccessChangedEventArgs args)
        {
            if (DeviceAccessStatus.DeniedByUser == args.Status)
            {
                MessageBox.Show("Location has been disabled by the user. Enable access through the settings charm.");
            }
            else if (DeviceAccessStatus.DeniedBySystem == args.Status)
            {
                MessageBox.Show("Location has been disabled by the system. The administrator of the device must enable location access through the location control panel.");
            }
            else if (DeviceAccessStatus.Unspecified == args.Status)
            {
                MessageBox.Show("Location has been disabled by unspecified source. The administrator of the device may need to enable location access through the location control panel, then enable access through the settings charm.");
            }
            else
            {
                MessageBox.Show("Unknown device access information status");
            }
        }


        public static Dispatcher Dispatcher
        {
            get
            {
                //var d = Windows.UI.Core.CoreWindow.GetForCurrentThread();
                //if (d != null)
                //{
                //    return d.Dispatcher;
                //}
                //else
                //{
                //    return null;
                //}

                return Deployment.Current.Dispatcher;
            }
        }

        private static void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            var reports = sender.ReadReports();
            if (Dispatcher != null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    OnGeofenceStateChanged(reports);
                });
            }
            else
            {
                OnGeofenceStateChanged(reports);
            }
        }

        private static void OnGeofenceStateChanged(System.Collections.Generic.IReadOnlyList<GeofenceStateChangeReport> reports)
        {
            foreach (GeofenceStateChangeReport report in reports)
            {
                GeofenceState state = report.NewState;

                Geofence geofence = report.Geofence;

                if (state == GeofenceState.Removed)
                {
                    // remove the geofence from the geofences collection
                    GeofenceMonitor.Current.Geofences.Remove(geofence);
                    MessageBox.Show(string.Format("Geofence Id: {0}, was removed.", geofence.Id));
                }
                else if (state == GeofenceState.Entered)
                {
                    // Your app takes action based on the entered event

                    // NOTE: You might want to write your app to take particular
                    // action based on whether the app has internet connectivity.
                    MessageBox.Show(string.Format("Geofence Id: {0}, just entered.", geofence.Id));
                }
                else if (state == GeofenceState.Exited)
                {
                    // Your app takes action based on the exited event

                    // NOTE: You might want to write your app to take particular
                    // action based on whether the app has internet connectivity.
                    MessageBox.Show(string.Format("Geofence Id: {0}, just exited.", geofence.Id));
                }
            }
        }

        public static async Task TryCreateGeofence()
        {
            await Initialize();

            string fenceKey = "TIG.Todo";

            Geofence geofence = null;

            BasicGeoposition position;
            position.Latitude = currentLocation.Coordinate.Point.Position.Latitude;
            position.Longitude = currentLocation.Coordinate.Point.Position.Longitude;
            position.Altitude = 0.0;
            double radius = 100; //Suggested >50

            // the geofence is a circular region
            Geocircle geocircle = new Geocircle(position, radius);

            bool singleUse = true;

            // want to listen for enter geofence, exit geofence and remove geofence events
            // you can select a subset of these event states
            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;
            mask |= MonitoredGeofenceStates.Removed;

            // setting up how long you need to be in geofence for enter event to fire
            //TimeSpan dwellTime = TimeSpan.FromMinutes(1);
            TimeSpan dwellTime = TimeSpan.FromSeconds(1);

            // setting up how long the geofence should be active
            //TimeSpan duration = TimeSpan.FromHours(24);
            TimeSpan duration = TimeSpan.FromMinutes(2);

            // setting up the start time of the geofence
            DateTimeOffset startTime = DateTimeOffset.Now;

            geofence = new Geofence(fenceKey, geocircle, mask, singleUse, dwellTime, startTime, duration);
            GeofenceMonitor.Current.Geofences.Add(geofence);

        }



    }
}
