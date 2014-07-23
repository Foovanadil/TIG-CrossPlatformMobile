using Microsoft.Phone.Shell;
using System;
using System.Linq;
using TIG.Todo.Common;

namespace TIG.Todo.WindowsPhone8.Scheduling
{
    public static class LiveTileHelper
    {
        //TODO: 2.0 - Implment LiveTileHelper

        public static void UpdateLiveTile(bool createIfNotExists, TodoItem[] incompleteTodoItems)
        {
            //TODO: 2.1 - Instantiat an instance of IconicTileData
            //  For indecies 0-2 if they exist set in order iconicTileData.WideContent1-3
            //  Call UpdateOrCreateTile passing "/", the tile data instance, and forwarding the argument createIfNotExists that was passed to this method

            var iconicTileData = new IconicTileData
            {
                Title = "TIG Todo",
                Count = incompleteTodoItems.Length,
                SmallIconImage = new Uri("Assets/Tiles/IconicTileSmall.png", UriKind.Relative),
                IconImage = new Uri("Assets/Tiles/IconicTileMediumLarge.png", UriKind.Relative),
            };

            if (incompleteTodoItems.Length > 0)
            {
                iconicTileData.WideContent1 = incompleteTodoItems[0].Text ?? "";
            }
            if (incompleteTodoItems.Length > 1)
            {
                iconicTileData.WideContent2 = incompleteTodoItems[1].Text ?? "";
            }
            if (incompleteTodoItems.Length > 2)
            {
                iconicTileData.WideContent3 = incompleteTodoItems[2].Text ?? "";
            }
            
            UpdateOrCreateTile("/", iconicTileData, createIfNotExists);
        }

        private static void UpdateOrCreateTile(string tileId, ShellTileData tileData, bool createIfNotExists)
        {
            //TODO: 2.2 - Update or Create the desired live tile
            //  Retrieve the ShellTile tileToUpdate by calling ShellTile.ActiveTiles and grabing the first where shellTile.NavigationUri.OriginalString.Contains(tileId)
            //  If the tile to update is null and createIfNotExists == true then use ShellTile.Create() to create the tile.
            //      Use string.Format("/MainPage.xaml?id={0}", tileId) for the navigationUri
            //      Pass tileData for the initialData arg
            //      Pass true for supportsWideTile
            //  Else if tileToUpdate != null
            //      Call tileToUpdate.Update passing tileData

            ShellTile tileToUpdate =
                ShellTile.ActiveTiles.FirstOrDefault(t => t.NavigationUri.OriginalString.Contains(tileId));

            if (tileToUpdate == null && createIfNotExists)
            {
                //NOTE: The main tile always exists, even if it's not pinned, so we don't need to worry about reaching this code for the main tile.
                ShellTile.Create(new Uri(string.Format("/MainPage.xaml?id={0}", tileId), UriKind.Relative), tileData, true);
            }
            else if (tileToUpdate != null)
            {
                tileToUpdate.Update(tileData);
            }
        }
    }
}
