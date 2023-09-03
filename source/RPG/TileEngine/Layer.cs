using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.TileEngine.Tiles;

namespace RPG.TileEngine
{
    //Den här klassen ska fungera som ett lager med tiles (klassen uppdaterar och målar upp alla tiles).
    public class Layer
    {
        #region Fields

        //Alla tiles som lagret ska ha finns i den här arrayen.
        private Tile[,] tiles;

        #endregion

        #region Properties

        //Ska lagret målas upp?
        public bool Visible { get; set; }
        //Kartan som hanterar det här lagret.
        public Map Map { get; private set; }
        //Property som returnerar samtliga tiles som lagret använder sig utav.
        public Tile[,] Tiles { get { return (Tile[,])tiles.Clone(); } }

        #endregion

        #region Constructor

        public Layer(Map map) 
        {
            Visible = true;
            Map = map;
            tiles = new Tile[Map.TilesHorizontal, Map.TilesVertical];
        }

        #endregion

        #region Methods

        //Metoden målar upp alla tiles.
        public void Draw(SpriteBatch spriteBatch)
        {
            //Lagret ska bara målas upp om det ska vara synligt.
            if (Visible)
            {
                //CameraPoint och MapPoint anger vilka tiles som ska målas upp (de som är i spelfönstret).
                for (int x = Map.CameraPoint.X; x < Map.ViewPoint.X; x++)
                    for (int y = Map.CameraPoint.Y; y < Map.ViewPoint.Y; y++)
                        if (tiles[x, y] != null)
                        {
                            Color color = Color.White;
                            Tile tile = tiles[x, y];
                            //Är debug sant så ska varje tile-sort målas upp med en specifik färg.
                            if (Map.Debug)
                            {
                                if (tile is DoorTile)
                                    color = new Color(Color.Magenta.R, Color.Magenta.G, Color.Magenta.B, 127);
                                else if (tile is EntranceTile)
                                    color = color = new Color(Color.Green.R, Color.Green.G, Color.Green.B, 127);
                                else if (tile is PlayerStartPositionTile)
                                    color = new Color(Color.Blue.R, Color.Blue.G, Color.Blue.B, 127);
                                else if (tile is EnemyStartPositionTile)
                                    color = new Color(Color.Yellow.R, Color.Yellow.G, Color.Yellow.B, 127);
                                else if (tile.TileMode == TileMode.Impassable)
                                    color = new Color(Color.Red.R, Color.Red.G, Color.Red.B, 127);
                            }
                            spriteBatch.Draw(Map.GetTileSheet(tile.TileSheetIndex).Texture, new Vector2(x * Map.TileWidth, y * Map.TileHeight),
                                Map.GetTileSheet(tile.TileSheetIndex).GetSourceRectangle(tile.TileSheetSourceRectangleIndex.X, tile.TileSheetSourceRectangleIndex.Y),
                                color);
                        }
            }
        }
        //Metoden returnerar vald tile.
        public Tile GetTile(int x, int y) { return tiles[x, y]; }
        //Metod som ändrar vald tile.
        public void SetTile(int x, int y, Tile tile) { tiles[x, y] = tile; }
        //Metoden tar bort vald tile.
        public void RemoveTile(int x, int y) { tiles[x, y] = null; }

        #endregion
    }
}
