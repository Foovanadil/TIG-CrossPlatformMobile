using System;
using System.Collections.Generic;
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
        //TODO: 10.0 - Implement GeofencingHelper

        private static Geolocator geolocator;
        private static CancellationTokenSource cts;
        private static DeviceAccessInformation accessInfo;
        private static Geoposition currentLocation;

        private static async Task<bool> Initialize()
        {
            bool initializedSuccessfully = false;

            //TODO: 10.1 - Get accessInfo
            // Set the field accessInfo with DeviceAccessInformation.CreateFromDeviceClass(DeviceClass.Location)

            #region Solution 10.1
            //accessInfo = DeviceAccessInformation.CreateFromDeviceClass(DeviceClass.Location); 
            #endregion

            try
            {
                //TODO: 10.2 - Get current location
                //  If field geolocator == null then instantiate it
                //  Instantiate cts with a new CancellationTokenSource
                //  Create a local variable CancellationToken token and set it with cts.Token
                //  Set current Location by calling await geolocator.GetGeopositionAsync().AsTask(token)
                #region Solution 10.2
                //if (geolocator == null)
                //{
                //    geolocator = new Geolocator();
                //}

                //cts = new CancellationTokenSource();
                //CancellationToken token = cts.Token;

                //currentLocation = await geolocator.GetGeopositionAsync().AsTask(token); 
                #endregion

                //TODO: 10.3 - Subscribe to required Events
                //  Subscribe to accessInfo.AccessChanged
                //  Subscribe to GeofenceMonitor.Current.GeofenceStateChanged

                #region Solution 10.3
                //accessInfo.AccessChanged += OnAccessChanged;
                //GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged; 
                #endregion

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

        public static Dispatcher Dispatcher
        {
            get
            {
                return Deployment.Current.Dispatcher;
            }
        }

        private static void OnAccessChanged(DeviceAccessInformation sender, DeviceAccessChangedEventArgs args)
        {
            //TODO: 10.4 - Note processing must be dispatched
            Dispatcher.BeginInvoke(() =>
            {
                OnAccessChanged(args);
            });
        }

        private static void OnAccessChanged(DeviceAccessChangedEventArgs args)
        {
            //TODO: 10.5 - Note access can change so handle the changes and notify the users
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

        private static void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            //TODO: 10.6 - Get Geofence reports and Note processing must be dispatched
            IReadOnlyList<GeofenceStateChangeReport> reports = sender.ReadReports();
            Dispatcher.BeginInvoke(() =>
            {
                OnGeofenceStateChanged(reports);
            });
        }

        private static void OnGeofenceStateChanged(IReadOnlyList<GeofenceStateChangeReport> reports)
        {
            //TODO: 10.7 - Process each report
            //  Foreach report do work
            foreach (GeofenceStateChangeReport report in reports)
            {
                //TODO: 10.8 - Process single report
                //  Store report.NewState in local member named state
                //  Store report.Geofence in local member named geofence
                //  If state == GeofenceState.Removed
                //      Then remove the geofence by calling GeofenceMonitor.Current.Geofences.Remove(geofence) and notify the user
                //          Use: MessageBox.Show(string.Format("Geofence Id: {0}, was removed.", geofence.Id));
                //  Else If state == GeofenceState.Entered
                //      Then Notify user using MessageBox.Show(string.Format("Geofence Id: {0}, just entered.", geofence.Id))
                //  Else If state == GeofenceState.Exited
                //      Then Notify user using MessageBox.Show(string.Format("Geofence Id: {0}, just exited.", geofence.Id))

                #region Solution 10.8
                //GeofenceState state = report.NewState; 
                //Geofence geofence = report.Geofence;

                //if (state == GeofenceState.Removed)
                //{
                //    // remove the geofence from the geofences collection
                //    GeofenceMonitor.Current.Geofences.Remove(geofence);
                //    MessageBox.Show(string.Format("Geofence Id: {0}, was removed.", geofence.Id));
                //}
                //else if (state == GeofenceState.Entered)
                //{
                //    // Your app takes action based on the entered event

                //    // NOTE: You might want to write your app to take particular
                //    // action based on whether the app has internet connectivity.
                //    MessageBox.Show(string.Format("Geofence Id: {0}, just entered.", geofence.Id));
                //}
                //else if (state == GeofenceState.Exited)
                //{
                //    // Your app takes action based on the exited event

                //    // NOTE: You might want to write your app to take particular
                //    // action based on whether the app has internet connectivity.
                //    MessageBox.Show(string.Format("Geofence Id: {0}, just exited.", geofence.Id));
                //}
                #endregion

            }
        }

        public static async Task TryCreateGeofence()
        {
            //TODO: 10.9 - Try to Create a Geofence
            //  Call await Initialize()
            //  Specify a local string member named fenceKey and set it "TIG.Todo"
            //  Specify a null Geofence local member named geofence
            //  Specify a BasicGeoposition local member named position
            //      Set position.Latitude with the Latitude of the current location
            //          Use: currentLocation.Coordinate.Point.Position.Latitude
            //      Set position.Longitude with the Longitude of the current location
            //          Use: currentLocation.Coordinate.Point.Position.Longitude
            //      Set position.Altitude to 0.0
            //  Specify a double local member named radius with value 100
            //  Specify a Geocircle local member named geocircle and set it to a new Geocircle passing in position and radius
            //  Specify a bool local member named singleUse set to true
            //  Specify a MonitoredGeofenceStates local member named mask set to 0
            //      Use the following code to set up the mask to pay attention to all actions (can be fewer if desired):
            //          mask |= MonitoredGeofenceStates.Entered;
            //          mask |= MonitoredGeofenceStates.Exited;
            //          mask |= MonitoredGeofenceStates.Removed;
            //  Specify a TimeSpan local member named dwellTime set to TimeSpan.FromSeconds(1) - can configure as desired
            //  Specify a TimeSpan local member named duration set to TimeSpan.FromMinutes(2) - can configure as desired
            //  Specify a DateTimeOffset local member named startTime set to DateTimeOffset.Now
            //  Set local member geofence to new Geofence(fenceKey, geocircle, mask, singleUse, dwellTime, startTime, duration)
            //  Register the geofence by calling GeofenceMonitor.Current.Geofences.Add(geofence)

            #region Solution 10.9
            //await Initialize();

            //string fenceKey = "TIG.Todo";

            //Geofence geofence = null;

            //BasicGeoposition position;
            //position.Latitude = currentLocation.Coordinate.Point.Position.Latitude;
            //position.Longitude = currentLocation.Coordinate.Point.Position.Longitude;
            //position.Altitude = 0.0;
            //double radius = 100; //Suggested >50

            //Geocircle geocircle = new Geocircle(position, radius);

            //bool singleUse = true;
            //MonitoredGeofenceStates mask = 0;

            //mask |= MonitoredGeofenceStates.Entered;
            //mask |= MonitoredGeofenceStates.Exited;
            //mask |= MonitoredGeofenceStates.Removed;

            //TimeSpan dwellTime = TimeSpan.FromSeconds(1);

            //TimeSpan duration = TimeSpan.FromMinutes(2);

            //DateTimeOffset startTime = DateTimeOffset.Now;

            //geofence = new Geofence(fenceKey, geocircle, mask, singleUse, dwellTime, startTime, duration);
            //GeofenceMonitor.Current.Geofences.Add(geofence); 
            #endregion

        }



    }
}
