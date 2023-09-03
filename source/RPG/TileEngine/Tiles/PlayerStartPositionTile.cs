using Microsoft.Xna.Framework;
using System;

namespace RPG.TileEngine.Tiles
{
    [Serializable]//Denna tile kommer att markera var spelaren börjar på varje karta.
    public class PlayerStartPositionTile : Tile
    {
        #region Constructor

        public PlayerStartPositionTile(int tileSheetIndex, Point tileSheetSourceRectangleIndex)
            : base(tileSheetIndex, tileSheetSourceRectangleIndex, TileMode.Passable) { }

        #endregion
    }
}
