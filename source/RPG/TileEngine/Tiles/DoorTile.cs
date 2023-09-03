using System;
using Microsoft.Xna.Framework;

namespace RPG.TileEngine.Tiles
{
    [Serializable]//Tile som ska fungera som en form av dörr som spelaren ska kunna öppna.
    public class DoorTile : Tile
    {
        #region Constructor

        public DoorTile(int tileSheetIndex, Point tileSheetSourceRectangleIndex)
            : base(tileSheetIndex, tileSheetSourceRectangleIndex, TileMode.Impassable) { }

        #endregion
    }
}
