//#define DEBUG_AGENT

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

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
//            UpdateLiveTile(false);

//            NotifyViaToast();

//#if DEBUG_AGENT
//            ScheduledActionService.LaunchForTest( task.Name, TimeSpan.FromSeconds( 60 ) );
//#endif

            NotifyComplete();
        }

        //private void NotifyViaToast()
        //{
        //    ShellToast toast = new ShellToast
        //    {
        //        Title = "TIG Todo",
        //        Content = incompleteTodoItems[0].Text,
        //        NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative)
        //    };

        //    toast.Show();
        //}

        //private void UpdateLiveTile(bool createIfNotExists)
        //{
        //    TodoItem[] incompleteTodoItems =
        //        ViewModel.TodoItems.Where(item => !item.IsCompleted).ToArray();

        //    var iconicTileData = new IconicTileData()
        //    {
        //        Title = "TIG Todo",
        //        Count = incompleteTodoItems.Length,
        //        SmallIconImage = new Uri("Assets/Tiles/IconicTileSmall.png", UriKind.Relative),
        //        IconImage = new Uri("Assets/Tiles/IconicTileMediumLarge.png", UriKind.Relative),
        //    };

        //    switch (ViewModel.TodoItems.Count)
        //    {
        //        case 0:
        //            iconicTileData.WideContent1 = string.Empty;
        //            iconicTileData.WideContent2 = string.Empty;
        //            iconicTileData.WideContent3 = string.Empty;
        //            break;
        //        case 1:
        //            iconicTileData.WideContent1 = incompleteTodoItems[0].Text;
        //            iconicTileData.WideContent2 = string.Empty;
        //            iconicTileData.WideContent3 = string.Empty;
        //            break;
        //        case 2:
        //            iconicTileData.WideContent1 = incompleteTodoItems[0].Text;
        //            iconicTileData.WideContent2 = incompleteTodoItems[1].Text;
        //            iconicTileData.WideContent3 = string.Empty;
        //            break;
        //        default:
        //            iconicTileData.WideContent1 = incompleteTodoItems[0].Text ?? string.Empty;
        //            iconicTileData.WideContent2 = incompleteTodoItems[1].Text ?? string.Empty;
        //            iconicTileData.WideContent3 = incompleteTodoItems[2].Text ?? string.Empty;
        //            break;
        //    }


        //    UpdateOrCreateTile("home", iconicTileData, createIfNotExists);
        //}

        //private void UpdateOrCreateTile(string tileId, ShellTileData tileData, bool createIfNotExists)
        //{
        //    ShellTile tileToUpdate =
        //        ShellTile.ActiveTiles.FirstOrDefault(t => t.NavigationUri.OriginalString.Contains(tileId));

        //    if (tileToUpdate == null && createIfNotExists)
        //    {
        //        //NOTE: The main tile always exists, even if it's not pinned, so we don't need to worry about reaching this code for the main tile.
        //        ShellTile.Create(new Uri(string.Format("/MainPage.xaml?id={0}", tileId), UriKind.Relative),
        //                         tileData, true);
        //    }
        //    else if (tileToUpdate != null)
        //    {
        //        tileToUpdate.Update(tileData);
        //    }
        //}

    }
}