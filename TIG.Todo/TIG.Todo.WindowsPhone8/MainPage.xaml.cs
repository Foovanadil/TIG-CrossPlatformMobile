using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using TIG.Todo.Common;

namespace TIG.Todo.WindowsPhone8
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel = new TodoListViewModel();
            DataContext = ViewModel;
            ViewModel.TodoItems.CollectionChanged += TodoItems_CollectionChanged;
        }

        public TodoListViewModel ViewModel { get; set; }

        private void TodoItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateLiveTile(false);
        }

        private void ActivateLiveTile(object sender, EventArgs e)
        {
            UpdateLiveTile(true);
        }

        private void UpdateLiveTile(bool createIfNotExists)
        {
            TodoItem[] incompleteTodoItems = 
                ViewModel.TodoItems.Where(item => !item.IsCompleted).ToArray();

            var iconicTileData = new IconicTileData()
            {
                Title = "TIG Todo",
                Count = incompleteTodoItems.Length,
                SmallIconImage = new Uri("Assets/Tiles/IconicTileSmall.png", UriKind.Relative),
                IconImage = new Uri("Assets/Tiles/IconicTileMediumLarge.png", UriKind.Relative),
            };

            switch (ViewModel.TodoItems.Count)
            {
                case 0:
                    iconicTileData.WideContent1 = string.Empty;
                    iconicTileData.WideContent2 = string.Empty;
                    iconicTileData.WideContent3 = string.Empty;
                    break;
                case 1:
                    iconicTileData.WideContent1 = incompleteTodoItems[0].Text;
                    iconicTileData.WideContent2 = string.Empty;
                    iconicTileData.WideContent3 = string.Empty;
                    break;
                case 2:
                    iconicTileData.WideContent1 = incompleteTodoItems[0].Text;
                    iconicTileData.WideContent2 = incompleteTodoItems[1].Text;
                    iconicTileData.WideContent3 = string.Empty;
                    break;
                default:
                    iconicTileData.WideContent1 = incompleteTodoItems[0].Text ?? string.Empty;
                    iconicTileData.WideContent2 = incompleteTodoItems[1].Text ?? string.Empty;
                    iconicTileData.WideContent3 = incompleteTodoItems[2].Text ?? string.Empty;
                    break;
            }


            UpdateOrCreateTile("/", iconicTileData, createIfNotExists);
        }

        private void UpdateOrCreateTile(string tileId, ShellTileData tileData, bool createIfNotExists)
        {
            ShellTile tileToUpdate =
                ShellTile.ActiveTiles.FirstOrDefault(t => t.NavigationUri.OriginalString.Contains(tileId));
            
            if (tileToUpdate == null && createIfNotExists)
            {
                //NOTE: The main tile always exists, even if it's not pinned, so we don't need to worry about reaching this code for the main tile.
                ShellTile.Create(new Uri(string.Format("/MainPage.xaml?id={0}", tileId), UriKind.Relative),
                                 tileData, true);
            }
            else if (tileToUpdate != null)
            {
                tileToUpdate.Update(tileData);
            }
        }


        private static readonly string TILE_UPDATER_TASK_NAME = "TILE_UPDATER_TASK_NAME";
        private void StartTileUpdaterTask()
        {
            PeriodicTask existingTileUpdaterTask = ScheduledActionService.Find(TILE_UPDATER_TASK_NAME) as PeriodicTask;
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
				ScheduledActionService.LaunchForTest( TILE_UPDATER_TASK_NAME, TimeSpan.FromSeconds( 60 ) );
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