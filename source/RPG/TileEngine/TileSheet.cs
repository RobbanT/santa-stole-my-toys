using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG.TileEngine
{
    //Klass som ska agera som ett TileSheet.
    public class TileSheet
    {
        #region Fields

        //Array som markerar alla tiles på texturen med en rektangel.
        private Rectangle[,] sourceRectangles;

        #endregion 

        #region Properties

        //Texturen som kommer att användas.
        public Texture2D Texture { get; private set; }
        //Hur många tiles texturen kommer att innehålla horisontellt och vertikalt.
        public int TilesHorizontal { get; private set; }
        public int TilesVertical { get; private set; }
        //En kopia med positionen/dimensionen på varje tile returneras.
        public Rectangle[,] SourceRectangles { get { return (Rectangle[,])sourceRectangles.Clone(); } }

        #endregion

        #region Constructor

        public TileSheet(Texture2D texture, int tileWidth, int tileHeight)
        {
            Texture = texture;

            //Vi kontrollerar om tile sheetet kan använda sig av tile storleken som används.
            if ((float)(Texture.Width % tileWidth) == 0 && (float)(Texture.Height % tileHeight) == 0)
            {
                TilesHorizontal = Texture.Width / tileWidth;
                TilesVertical = Texture.Height / tileHeight;
                sourceRectangles = new Rectangle[TilesHorizontal, TilesVertical];
                //Varje tile i tilesheetet markeras med en rektangel.
                for (int x = 0; x < TilesHorizontal; x++)
                    for (int y = 0; y < TilesVertical; y++)
                        sourceRectangles[x, y] = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
            }
            else
                throw new Exception("Error, Tile sheet width or height is not divisible with the current tile width or height!");
        }

        #endregion

        #region Methods

        //En vald sourceRectangle returneras.
        public Rectangle GetSourceRectangle(int x, int y) { return sourceRectangles[x, y]; }

        #endregion
    }
}
