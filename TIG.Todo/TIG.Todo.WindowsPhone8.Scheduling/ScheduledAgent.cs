#define DEBUG_AGENT

using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using TIG.Todo.Common;
using System.Linq;

namespace TIG.Todo.WindowsPhone8.Scheduling
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
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

        private static readonly TodoItem[] incompleteItems =
            DataProvider.RetrieveValue<TodoItem[]>()
                .Where(t => !t.IsCompleted).ToArray();

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
            LiveTileHelper.UpdateLiveTile(false, incompleteItems);

            NotifyViaToast();

#if DEBUG_AGENT
            TodoItem[] allItems = DataProvider.RetrieveValue<TodoItem[]>().Concat(new []{ new TodoItem{ Text = "from test..." }}).ToArray();
            bool successfullySaved = DataProvider.SaveValue<TodoItem[]>(allItems);

            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));
#endif

            NotifyComplete();
        }

        private void NotifyViaToast()
        {
            ShellToast toast = new ShellToast
            {
                Title = "TIG Todo",
                Content = string.Format("You have {0} incomplete todos.", incompleteItems.Length),
                NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative)
            };

            toast.Show();
        }
    }
}