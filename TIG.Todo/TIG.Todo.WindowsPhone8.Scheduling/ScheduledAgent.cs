//TODO: 9.5.1 #define DEBUG_AGENT

using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using TIG.Todo.Common;
using TIG.Todo.DataProviders;
using System.Linq;

namespace TIG.Todo.WindowsPhone8.Scheduling
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        //TODO: 9.0 - Implement ScheduledAgent - Used by PeriodicTask

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        //TODO: 9.1 - Create static readonly field of array of TodoItems named incompleteItems by calling DataProvider.RetriveValue then selecting only those that are incomplete
        #region Solution 9.1
        //private static readonly TodoItem[] incompleteItems = DataProvider.RetrieveValue<TodoItem[]>().Where(t => !t.IsCompleted).ToArray(); 
        #endregion

        private void NotifyViaToast()
        {
            //TODO: 9.2 - Create a ShellToast instance named toast
            //  Set Title to App name
            //  Set Content to string.Format("You have {0} incomplete todos.", incompleteItems.Length),
            //  Set NavigationUri with /MainPage.xaml using UriKind.Relative
            //  Use the toast by calling toast.Show()
            
            #region Solution 9.2
            //ShellToast toast = new ShellToast
            //{
            //    Title = "TIG Todo",
            //    Content = string.Format("You have {0} incomplete todos.", incompleteItems.Length),
            //    NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative)
            //};

            //toast.Show(); 
            #endregion
        }


        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            //TODO: 9.3 - Call LiveTileHelper.UpdateLiveTile passing false and incompleteItems
          
            #region Solution 9.3
            //LiveTileHelper.UpdateLiveTile(false, incompleteItems); 
            #endregion

            //TODO: 9.4 - Call NotifyViaToast
            #region Solution 9.4
            //NotifyViaToast(); 
            #endregion

            //TODO: 9.5.0 - Define DEBUG_AGENT at the top of the file (see 9.5.1) (hint 8.5.0)
#if(DEBUG_AGENT)
            //TODO: 9.6 - Launch the PeriodicTask every 60 seconds
            //  Use following code:
            //  TodoItem[] allItems = DataProvider.RetrieveValue<TodoItem[]>().Concat(new[] { new TodoItem { Text = "from test..." } }).ToArray();
            //  bool successfullySaved = DataProvider.SaveValue<TodoItem[]>(allItems);
            //  ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));
#endif

            NotifyComplete();
        }

    }
}