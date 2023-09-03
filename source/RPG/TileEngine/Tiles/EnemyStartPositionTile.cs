using Microsoft.Xna.Framework;
using System;

namespace RPG.TileEngine.Tiles
{
    [Serializable]//Denna tile kommer att markera var fiender börjar på varje karta.
    public class EnemyStartPositionTile : Tile
    {
        #region Constructor

        public EnemyStartPositionTile(int tileSheetIndex, Point tileSheetSourceRectangleIndex)
            : base(tileSheetIndex, tileSheetSourceRectangleIndex, TileMode.Passable) { }

        #endregion
    }
}
