#define DEBUG_AGENT

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
        private static readonly string TILE_UPDATER_TASK_NAME = 
            "TIG.Todo.TILE_UPDATER_TASK_NAME";
        public static void StartTileUpdaterTask()
        {
            PeriodicTask existingTileUpdaterTask = 
                ScheduledActionService.Find(TILE_UPDATER_TASK_NAME) as PeriodicTask;
            if (existingTileUpdaterTask != null)
            {
                try
                {
                    ScheduledActionService.Remove(TILE_UPDATER_TASK_NAME);
                }
                catch (Exception ex)
                {
                    //NOTE: Exceptions during removal are unimportant
                }
            }

            PeriodicTask newTileUpdaterTask = new PeriodicTask(TILE_UPDATER_TASK_NAME)
            {
                Description = "Updates main tile.",
                ExpirationTime = DateTime.Now.AddDays(14),
            };

            try
            {
                ScheduledActionService.Add(newTileUpdaterTask);

#if(DEBUG_AGENT)
				ScheduledActionService.LaunchForTest( TILE_UPDATER_TASK_NAME, TimeSpan.FromSeconds( 30 ) );
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
