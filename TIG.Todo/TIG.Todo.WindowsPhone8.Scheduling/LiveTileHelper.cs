using Microsoft.Phone.Shell;
using System;
using System.Linq;
using TIG.Todo.Common;

namespace TIG.Todo.WindowsPhone8.Scheduling
{
    public static class LiveTileHelper
    {
        public static void UpdateLiveTile(bool createIfNotExists,
            TodoItem[] incompleteTodoItems)
        {
            var iconicTileData = new IconicTileData()
            {
                Title = "TIG Todo",
                Count = incompleteTodoItems.Length,
                SmallIconImage = new Uri("Assets/Tiles/IconicTileSmall.png",
                    UriKind.Relative),
                IconImage = new Uri("Assets/Tiles/IconicTileMediumLarge.png",
                    UriKind.Relative),
            };

            switch (incompleteTodoItems.Length)
            {
                case 0:
                    iconicTileData.WideContent1 = string.Empty;
                    iconicTileData.WideContent2 = string.Empty;
                    iconicTileData.WideContent3 = string.Empty;
                    break;
                case 1:
                    iconicTileData.WideContent1 = incompleteTodoItems[0].Text ?? string.Empty;
                    iconicTileData.WideContent2 = string.Empty;
                    iconicTileData.WideContent3 = string.Empty;
                    break;
                case 2:
                    iconicTileData.WideContent1 = incompleteTodoItems[0].Text ?? string.Empty;
                    iconicTileData.WideContent2 = incompleteTodoItems[1].Text ?? string.Empty;
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

        private static void UpdateOrCreateTile(string tileId,
            ShellTileData tileData, bool createIfNotExists)
        {
            ShellTile tileToUpdate =
                ShellTile.ActiveTiles.FirstOrDefault(t =>
                    t.NavigationUri.OriginalString.Contains(tileId));

            if (tileToUpdate == null && createIfNotExists)
            {
                //NOTE: The main tile always exists, even if it's not pinned, so we don't need to worry about reaching this code for the main tile.
                ShellTile.Create(new Uri(
                    string.Format("/MainPage.xaml?id={0}", tileId), UriKind.Relative),
                                 tileData, true);
            }
            else if (tileToUpdate != null)
            {
                tileToUpdate.Update(tileData);
            }
        }
    }
}
