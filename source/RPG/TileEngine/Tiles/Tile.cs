using System;
using Microsoft.Xna.Framework;

namespace RPG.TileEngine.Tiles
{
    //Enum som håller reda på om man kan gå på en tile.
    public enum TileMode { Passable, Impassable }

    [Serializable]//Klass som ska agera som en tile. 
    public class Tile
    {
        #region Properties

        //Variabel som håller reda på om man kan gå på tilen.
        public TileMode TileMode { get; private set; }
        //Vilket index som tilens TileSheet använder sig av.
        public int TileSheetIndex { get; private set; }
        //Vilket index som sourceRectangeln som tilen ska använda sig av har.
        public Point TileSheetSourceRectangleIndex { get; private set; }

        #endregion

        #region Constructor

        public Tile(int tileSheetIndex, Point tileSheetSourceRectangleIndex, TileMode tileState = TileMode.Passable)
        {
            TileSheetIndex = tileSheetIndex;
            TileSheetSourceRectangleIndex = tileSheetSourceRectangleIndex;
            TileMode = tileState;
        }

        #endregion
    }
}    
