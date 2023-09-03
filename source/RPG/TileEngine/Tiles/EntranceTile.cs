using System;
using Microsoft.Xna.Framework;

namespace RPG.TileEngine.Tiles
{
    [Serializable]//Klass som ska fungera som en ingång till en annan karta.
    public class EntranceTile : Tile
    {
        #region Properties

        //Vilka kartor som den här tilen är ingång till lagras i den här arrayen.
        public string[] MapNames { get; private set; }

        #endregion

        #region Constructor

        public EntranceTile(int tileSheetIndex, Point tileSheetSourceRectangleIndex, params string[] mapNames)
            : base(tileSheetIndex, tileSheetSourceRectangleIndex, TileMode.Passable)
        {
            MapNames = mapNames;
        }

        #endregion
    }
}
