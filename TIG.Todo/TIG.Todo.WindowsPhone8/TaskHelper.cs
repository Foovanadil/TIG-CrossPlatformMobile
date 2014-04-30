//TODO: 8.5.1 - #define DEBUG_AGENT

using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TIG.Todo.WindowsPhone8
{
    public static class TaskHelper
    {
        //TODO: 8.0 - Implement TaskHelper


        private static readonly string TILE_UPDATER_TASK_NAME = "TIG.Todo.TILE_UPDATER_TASK_NAME";
        public static void StartTileUpdaterTask()
        {
            PeriodicTask existingTileUpdaterTask = null;
            //TODO: 8.1 - Find the PeriodicTask that has been scheduled if one exists.
            //  Use: existingTileUpdaterTask = ScheduledActionService.Find(TILE_UPDATER_TASK_NAME) as PeriodicTask;
            if (existingTileUpdaterTask != null)
            {
                try
                {
                    //TODO: 8.2 - If a scheduled PeriodicTask exists unschedule it
                    //  Use: ScheduledActionService.Remove(TILE_UPDATER_TASK_NAME);
                }
                catch (Exception ex)
                {
                    //NOTE: Exceptions during removal are unimportant
                }
            }

            //TODO: 8.3 - Create a new updated PeriodicTask with ID TILE_UPDATER_TASK_NAME
            //  Set Description to "Updates main tile."
            //  Set ExpirationTime to DateTime.Now.AddDays(14)
            #region Solution 8.3
            //PeriodicTask newTileUpdaterTask = new PeriodicTask(TILE_UPDATER_TASK_NAME)
            //{
            //    Description = "Updates main tile.",
            //    ExpirationTime = DateTime.Now.AddDays(14),
            //}; 
            #endregion

            try
            {
                //TODO: 8.4 - Schedule the new PeriodicTask
                //  Use: ScheduledActionService.Add(newTileUpdaterTask);

                //TODO: 8.5.0 - Define DEBUG_AGENT at the top of the file (see 8.5.1)
#if(DEBUG_AGENT)
                //TODO: 8.6 - Launch the PeriodicTask every 60 seconds
                //  Cannot be sooner. It Will not work.
                //  Only works in Debug builds
				//  Use: ScheduledActionService.LaunchForTest( TILE_UPDATER_TASK_NAME, TimeSpan.FromSeconds( 60 ) );
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }

                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
        }

    }
}
