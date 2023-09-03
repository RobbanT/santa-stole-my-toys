using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPG.SpriteClasses.AnimatedSprites;
using RPG.SpriteClasses.Sprites;
using RPG.TileEngine.Tiles;

namespace RPG.TileEngine
{
    //Detta är klassen som ska agera som en karta (den hanterar bland annat alla lager och tile sheets som klassen behöver).
    public class Map
    {
        #region Fields

        //Alla tile sheets som kartan ska använda sig av finns i den här listan.
        private List<TileSheet> tileSheets = new List<TileSheet>();
        //Alla lager med tiles som kartan ska bestå av finns i den här listan.
        private List<Layer> layers = new List<Layer>();

        #endregion

        #region Properties

        //Namnet på kartan.
        public string MapName { get; private set; }
        //Bredden som kartan har på sina tiles.
        public int TileWidth { get; private set; }
        //Höjden som kartan har på sina tiles.
        public int TileHeight { get; private set; }
        //Hur många tiles kartan består av horisontellt.
        public int TilesHorizontal { get; private set; }
        //Hur många tiles kartan består av vertikalt.
        public int TilesVertical { get; private set; }
        //Hur bred kartan är i pixlar.
        public int MapWidthInPixels { get { return TileWidth * TilesHorizontal; } }
        //Hur hög kartan är i pixlar.
        public int MapHeightInPixels { get { return TileHeight * TilesVertical; } }
        //Kameran som kommer att vara ovanför kartan.
        public Camera Camera { get; set; }
        //Antalet lager returneras.
        public int NumberOfLayers { get { return layers.Count; } }
        //Antalet tile sheets returneras.
        public int NumberOfTileSheets { get { return tileSheets.Count; } } 
        //Vill vi att t.ex. Impassable tiles ska synas med röd färg så ska den här sättas till true;
        public bool Debug { get; private set; }
        //Här räknar vi ut vilka tiles som befinner sig i spelfönstret och ska målas upp (Tilesen utanför spelfönstret kommer alltså inte att målas upp).
        public Point CameraPoint
        {
            get
            {
                return new Point(Math.Max(0, VectorToCell(Camera.Position * (1 / Camera.Zoom)).X - 1),
                    Math.Max(0, VectorToCell(Camera.Position * (1 / Camera.Zoom)).Y - 1));
            }
        }
        public Point ViewPoint
        {
            get
            {
                return new Point(Math.Min(VectorToCell(new Vector2((Camera.Position.X + Camera.Viewport.Width) * (1 / Camera.Zoom))).X + 1, TilesHorizontal),
                    Math.Min(VectorToCell(new Vector2((Camera.Position.Y + Camera.Viewport.Height) * (1 / Camera.Zoom))).Y + 1, TilesVertical));
            }
        }
        #endregion

        #region Constructor

        //Konstruktor som körs när man vill skapa en helt ny karta.
        public Map(string mapName, int tileWidth, int tileHeight, int tilesHorizontal, int tilesVertical, 
            Viewport viewport, bool debug = false)
        {
            MapName = mapName;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesHorizontal = tilesHorizontal;
            TilesVertical = tilesVertical;
            Camera = new Camera(viewport, new Rectangle(0, 0, MapWidthInPixels, MapHeightInPixels), Vector2.Zero);
            Debug = debug;
            AddLayer();
        }

        #endregion

        #region Methods

        //Metoden målar upp alla lager
        public void Draw(SpriteBatch spriteBatch)
        {
            //Alla tile-lager målar upp sina tiles som befinner sig i spelfönstret.
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch);
        }
        //Metod som returnerar valt lager.
        public Layer GetLayer(int index) { return layers[index]; }
        //Metod som lägger till ett lager.
        public void AddLayer() { layers.Add(new Layer(this)); }
        //Metod som tar bort valt lager.
        public void RemoveLayer(int index) { layers.RemoveAt(index); }
        //Metod som returnerar valt tile sheet.
        public TileSheet GetTileSheet(int index) { return tileSheets[index]; }
        //Metod som lägger till ett tile sheet.
        public void AddTileSheet(Texture2D texture) { tileSheets.Add(new TileSheet(texture, TileWidth, TileHeight)); }
        //Metod som tar bort valt tile sheet.
        public void RemoveTileSheet(int index)
        {
            //Innan vi tar bort ett tile sheet kontrollerar vi om någon tile använder sig av det (är det så kommer även den tilen att tas bort).
            foreach (Layer layer in layers)
                for (int x = 0; x < TilesHorizontal; x++)
                    for (int y = 0; y < TilesVertical; y++)
                        if (layer.GetTile(x, y) != null && layer.GetTile(x, y).TileSheetIndex == index)
                            layer.RemoveTile(x, y);
            tileSheets.RemoveAt(index);
        }
        //Metod som returnerar en point som pekar ut tilen som en vektor (position) befinner sig i.
        public Point VectorToCell(Vector2 position) { return new Point((int)position.X / TileWidth, (int)position.Y / TileHeight); }
        //Metod som returnerar en point som pekar ut tilen som en vektor (position) befinner sig i (skalad).
        public Point TransformedVectorToCell(Vector2 position) { return new Point((int)(position.X / Camera.TransformFloat(TileWidth)), (int)(position.Y / Camera.TransformFloat(TileHeight))); }
        //Metod som returnerar en vector med hjälp av en tiles koordinater.
        public Vector2 CellToVector(Point tileCoordinates) { return new Vector2(tileCoordinates.X * TileWidth + TileWidth / 2, tileCoordinates.Y * TileHeight + TileHeight / 2); }
        //Metod som returnerar samtliga tiles i vald cell.
        public Tile[] GetTilesInCell(Point tileCoordinates)
        {
            Tile[] tilesInCell = new Tile[NumberOfLayers];
            for (int i = 0; i < NumberOfLayers; i++)
                tilesInCell[i] = GetLayer(i).GetTile(tileCoordinates.X, tileCoordinates.Y);
            return tilesInCell;
        }
        //Metod som returnerar spelarens startposition.
        public Vector2 GetPlayerStartPositionTilePosition()
        {
            //Söker efter start position tilen i lager 0 (max 1 får existera).
            for (int x = 0; x < TilesHorizontal; x++)
                for (int y = 0; y < TilesVertical; y++)
                    if (layers[0].GetTile(x, y) is PlayerStartPositionTile)
                        return CellToVector(new Point(x, y));
            throw new Exception("PlayerStartPositionTile not found");//Hittas ingen startpostion för spelaren så kastas en exception.
        }
        //Metod som returnerar alla fienders startpositioner i form av en lista.
        public List<Vector2> GetEnemyStartPositionTilePositions()
        {
            List<Vector2> enemyStartPositionTilesPositions = new List<Vector2>();
            //Även fiendernas start positioner får bara existera i lager 0.
                for (int x = 0; x < TilesHorizontal; x++)
                    for (int y = 0; y < TilesVertical; y++)
                        if (layers[0].GetTile(x, y) is EnemyStartPositionTile)
                            enemyStartPositionTilesPositions.Add(CellToVector(new Point(x, y)));
            return enemyStartPositionTilesPositions;
        }
        //Metod som kontrollerar om det finns en startpositions-tile utsatt.
        public bool StartPositionTileExist()
        {
                for (int x = 0; x < TilesHorizontal; x++)
                    for (int y = 0; y < TilesVertical; y++)
                        if (GetLayer(0).GetTile(x, y) is PlayerStartPositionTile)
                            return true;
            return false;
        }
        //Metod som returnera en rektangel om man kanske vill testa kollision med rektangel.
        public Rectangle GetTileCollisionRectangle(Point tileCoordinates)
        {
            return new Rectangle((int)CellToVector(tileCoordinates).X - TileWidth / 2,
                (int)CellToVector(tileCoordinates).Y - TileHeight / 2, TileWidth, TileHeight);
        }
        //Metod som returnera en rektangel om man kanske vill testa kollision med rektangel.
        public Rectangle GetTileCollisionRectangle(Vector2 position)
        {
            return new Rectangle((int)CellToVector(VectorToCell(position)).X - TileWidth / 2,
                (int)CellToVector(VectorToCell(position)).Y - TileHeight / 2, TileWidth, TileHeight);
        }
        //Metod som kontrollerar kollision mellan tile och karaktär.
        public void CheckTileCollisionWithCharacters(Character character)
        {
            //Vi sparar ner tilen som karaktären står på och vi sparar dessutom ner alla tiles runt karaktären (vänster, höger, framför och bakom). 
            Dictionary<string, Point> characterTiles = new Dictionary<string, Point>();
            characterTiles.Add("Center", new Point((int)MathHelper.Clamp(VectorToCell(character.Position).X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(VectorToCell(character.Position).Y, 0, TilesVertical - 1)));
            characterTiles.Add("Left", new Point((int)MathHelper.Clamp(characterTiles["Center"].X - 1, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(characterTiles["Center"].Y, 0, TilesVertical - 1)));
            characterTiles.Add("Right", new Point((int)MathHelper.Clamp(characterTiles["Center"].X + 1, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(characterTiles["Center"].Y, 0, TilesVertical - 1)));
            characterTiles.Add("Front", new Point((int)MathHelper.Clamp(characterTiles["Center"].X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(characterTiles["Center"].Y - 1, 0, TilesVertical - 1)));
            characterTiles.Add("Behind", new Point((int)MathHelper.Clamp(characterTiles["Center"].X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(characterTiles["Center"].Y + 1, 0, TilesVertical - 1)));
            //Kör själva kollisionstestet här (tiles som är impassable finns endast i lager 0 och 1).
            for (int i = 0; i < 2; i++)
            {
                //Vi kollar först på tilen till vänster om karaktären.
                if (GetTilesInCell(characterTiles["Left"])[i] != null && GetTilesInCell(characterTiles["Left"])[i].TileMode == TileMode.Impassable)
                {
                    if (character.CollisionRectangle.Left < GetTileCollisionRectangle(characterTiles["Left"]).Right)
                    {
                        if(!character.Damaged)
                            character.Position = new Vector2(character.Position.X + character.Speed.X, character.Position.Y);
                        else
                            character.Position = new Vector2(character.Position.X + character.DamagedSpeed.X, character.Position.Y);
                        return;//Vi bryr oss inte om att kolla kollision med övriga tiles om kollision uppstår redan här.
                    }
                }
                //Vi kollar sedan på tilen till höger om karaktären.
                if (GetTilesInCell(characterTiles["Right"])[i] != null && GetTilesInCell(characterTiles["Right"])[i].TileMode == TileMode.Impassable)
                {
                    if (character.CollisionRectangle.Right > GetTileCollisionRectangle(characterTiles["Right"]).Left)
                    {
                        if (!character.Damaged)
                            character.Position = new Vector2(character.Position.X - character.Speed.X, character.Position.Y);
                        else
                            character.Position = new Vector2(character.Position.X - character.DamagedSpeed.X, character.Position.Y);
                        return;//Vi bryr oss inte om att kolla kollision med övriga tiles om kollision uppstår redan här.
                    }
                }
                //Vi kollar sedan på tilen framför karaktären.
                if (GetTilesInCell(characterTiles["Front"])[i] != null && GetTilesInCell(characterTiles["Front"])[i].TileMode == TileMode.Impassable)
                {
                    if (character.CollisionRectangle.Top < GetTileCollisionRectangle(characterTiles["Front"]).Bottom)
                    {
                        if (!character.Damaged)
                            character.Position = new Vector2(character.Position.X, character.Position.Y + character.Speed.Y);
                        else
                            character.Position = new Vector2(character.Position.X, character.Position.Y + character.DamagedSpeed.Y);
                        return;//Vi bryr oss inte om att kolla kollision med övriga tiles om kollision uppstår redan här.
                    }
                }
                //Vi kollar sedan på tilen bakom karaktären.
                if (GetTilesInCell(characterTiles["Behind"])[i] != null && GetTilesInCell(characterTiles["Behind"])[i].TileMode == TileMode.Impassable)
                {
                    if (character.CollisionRectangle.Bottom > GetTileCollisionRectangle(characterTiles["Behind"]).Top)
                    {
                        if (!character.Damaged)
                            character.Position = new Vector2(character.Position.X, character.Position.Y - character.Speed.Y);
                        else
                            character.Position = new Vector2(character.Position.X, character.Position.Y - character.DamagedSpeed.Y);
                        return;//Vi bryr oss inte om att kolla kollision med övriga tiles om kollision uppstår redan här.
                    }
                }
            }
        }
        //Metod som kontrollerar kollision mellan tile och arrow-objekt.
        public bool CheckTileCollisionWithArrow(Arrow arrow)
        {
            //Arrow-objekt textur.
            Texture2D arrowTexture = arrow.Texture;
            //Pixlarna hos arrow-objektets textur.
            Color[] arrowTextureColors = new Color[arrowTexture.Width * arrowTexture.Height];
            //Information om texturen som arrow-objektet använder hämtas.
            arrowTexture.GetData(0, null, arrowTextureColors, 0, arrowTextureColors.Length);

            //Vi sparar ner tilen som arrow-objektet befinner sig på och vi sparar dessutom ner alla tiles runt arrow-objektet (vänster, höger, framför, bakom). 
            List<Point> arrowTiles = new List<Point>();
            arrowTiles.Add(new Point((int)MathHelper.Clamp(VectorToCell(arrow.Position).X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(VectorToCell(arrow.Position).Y, 0, TilesVertical - 1)));//Center
            arrowTiles.Add(new Point((int)MathHelper.Clamp(arrowTiles[0].X - 1, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(arrowTiles[0].Y, 0, TilesVertical - 1)));//Vänster
            arrowTiles.Add(new Point((int)MathHelper.Clamp(arrowTiles[0].X + 1, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(arrowTiles[0].Y, 0, TilesVertical - 1)));//Höger
            arrowTiles.Add(new Point((int)MathHelper.Clamp(arrowTiles[0].X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(arrowTiles[0].Y - 1, 0, TilesVertical - 1)));//Framför
            arrowTiles.Add(new Point((int)MathHelper.Clamp(arrowTiles[0].X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(arrowTiles[0].Y + 1, 0, TilesVertical - 1)));//Bakom
            
            //Kör själva kollisionstestet här (tiles som är impassable finns endast i lager 0 och 1).
            for (int i = 0; i < 2; i++)
            {
                //Vi kontrollerar samtliga tiles.
                for (int j = 0; j < arrowTiles.Count; j++)
                {
                    if (GetTilesInCell(arrowTiles[j])[i] != null && GetTilesInCell(arrowTiles[j])[i].TileMode == TileMode.Impassable)
                    {
                        //Tilens textur.
                        Texture2D tileTexture = GetTileSheet(GetTilesInCell(arrowTiles[j])[i].TileSheetIndex).Texture;
                        //Antalet pixlar hos tilens textur.
                        Color[] tileTextureColors = new Color[TileWidth * TileHeight];
                        //Information om texturen som tilen använder hämtas.
                        tileTexture.GetData(0,
                            new Rectangle(GetTileSheet(GetTilesInCell(arrowTiles[j])[i].TileSheetIndex).SourceRectangles[GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.X,
                            GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.Y].X,
                            GetTileSheet(GetTilesInCell(arrowTiles[j])[i].TileSheetIndex).SourceRectangles[GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.X,
                            GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.Y].Y,
                        GetTileSheet(GetTilesInCell(arrowTiles[j])[i].TileSheetIndex).SourceRectangles[GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.X,
                        GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.Y].Width,
                        GetTileSheet(GetTilesInCell(arrowTiles[j])[i].TileSheetIndex).SourceRectangles[GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.X,
                        GetTilesInCell(arrowTiles[j])[i].TileSheetSourceRectangleIndex.Y].Height),
                        tileTextureColors, 0, TileWidth * TileHeight);

                        //Snitt
                        int Top = Math.Max(arrow.CollisionRectangle.Top, GetTileCollisionRectangle(arrowTiles[j]).Top);
                        int Left = Math.Max(arrow.CollisionRectangle.Left, GetTileCollisionRectangle(arrowTiles[j]).Left);
                        int Right = Math.Min(arrow.CollisionRectangle.Right, GetTileCollisionRectangle(arrowTiles[j]).Right);
                        int Bottom = Math.Min(arrow.CollisionRectangle.Bottom, GetTileCollisionRectangle(arrowTiles[j]).Bottom);

                        //Själva pixelkontrollen.
                        for (int y = Top; y < Bottom; y++)
                        {
                            for (int x = Left; x < Right; x++)
                            {
                                Color colorA = tileTextureColors[(x - GetTileCollisionRectangle(arrowTiles[j]).Left) +
                                    (y - GetTileCollisionRectangle(arrowTiles[j]).Top) * GetTileCollisionRectangle(arrowTiles[j]).Width];
                                Color colorB = arrowTextureColors[(x - arrow.CollisionRectangle.Left) + (y - arrow.CollisionRectangle.Top) * GetTileCollisionRectangle(arrowTiles[j]).Width];
                                if (colorA.A != 0 && colorB.A != 0)
                                    return true;//Vi bryr oss inte om att kolla kollision med övriga tiles om kollision uppstår redan här.
                            }
                        }
                    }
                }
            }
            return false;
        }
        //Metod som kontrollerar om det är en doorTile framför spelaren.
        public void OpenDoorTile(Player player)
        {
            //Vi sparar ner tilen som spelaren står på och vi sparar dessutom ner alla tiles runt spelaren (vänster, höger, framför och bakom). 
            Dictionary<string, Point> playerTiles = new Dictionary<string, Point>();
            playerTiles.Add("Center", new Point((int)MathHelper.Clamp(VectorToCell(player.Position).X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(VectorToCell(player.Position).Y, 0, TilesVertical - 1)));
            playerTiles.Add("Left", new Point((int)MathHelper.Clamp(playerTiles["Center"].X - 1, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(playerTiles["Center"].Y, 0, TilesVertical - 1)));
            playerTiles.Add("Right", new Point((int)MathHelper.Clamp(playerTiles["Center"].X + 1, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(playerTiles["Center"].Y, 0, TilesVertical - 1)));
            playerTiles.Add("Front", new Point((int)MathHelper.Clamp(playerTiles["Center"].X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(playerTiles["Center"].Y - 1, 0, TilesVertical - 1)));
            playerTiles.Add("Behind", new Point((int)MathHelper.Clamp(playerTiles["Center"].X, 0, TilesHorizontal - 1), (int)MathHelper.Clamp(playerTiles["Center"].Y + 1, 0, TilesVertical - 1)));
            //Kör en kontroll och kollar om spelaren står och tittar på en doorTile.
            for (int i = 0; i < 2; i++)
            {
                //Vi kollar först på tilen till vänster om spelaren.
                if (GetTilesInCell(playerTiles["Left"])[i] != null && GetTilesInCell(playerTiles["Left"])[i] is DoorTile && player.MovingDirectionState == MovingDirectionState.Left)
                {
                    //Visar det sig att spelaren faktiskt står och kollar på en doorTile till vänster om sig så tar vi bort denna tile och returnerar därefter.
                    layers[i].RemoveTile(playerTiles["Left"].X, playerTiles["Left"].Y);
                    return;
                }
                //Vi kollar sedan på tilen till höger om spelaren.
                if (GetTilesInCell(playerTiles["Right"])[i] != null && GetTilesInCell(playerTiles["Right"])[i] is DoorTile && player.MovingDirectionState == MovingDirectionState.Right)
                {
                    //Visar det sig att spelaren faktiskt står och kollar på en doorTile till höger om sig så tar vi bort denna tile och returnerar därefter.
                    layers[i].RemoveTile(playerTiles["Right"].X, playerTiles["Right"].Y);
                    return;
                }
                //Vi kollar sedan på tilen framför spelaren.
                if (GetTilesInCell(playerTiles["Front"])[i] != null && GetTilesInCell(playerTiles["Front"])[i] is DoorTile && player.MovingDirectionState == MovingDirectionState.Up)
                {
                    //Visar det sig att spelaren faktiskt står och kollar på en doorTile framför sig så tar vi bort denna tile och returnerar därefter.
                    layers[i].RemoveTile(playerTiles["Front"].X, playerTiles["Front"].Y);
                    return;
                }
                //Vi kollar sedan på tilen bakom spelaren.
                if (GetTilesInCell(playerTiles["Behind"])[i] != null && GetTilesInCell(playerTiles["Behind"])[i] is DoorTile && player.MovingDirectionState == MovingDirectionState.Down)
                {
                    //Visar det sig att spelaren faktiskt står och kollar på en doorTile bakom sig så tar vi bort denna tile och returnerar därefter.
                    layers[i].RemoveTile(playerTiles["Behind"].X, playerTiles["Behind"].Y);
                    return;
                }
            }
        }
        //Metod som returnerar ett namn på en karta som en entrance tile har (vilket namn det blir slumpas fram) om nu spelaren står på en sådan tile.
        public string IsPlayerOnEntranceTile(Player player)
        {
            //Står spelaren på en entrence tile returnerar vi ett framslumpat namn som tilen innehåller.
            for (int i = 0; i < 2; i++)
                if (GetTilesInCell(VectorToCell(player.Position))[i] is EntranceTile)
                    return ((EntranceTile)GetTilesInCell(VectorToCell(player.Position))[i]).MapNames[Common.Random.Next(((EntranceTile)GetTilesInCell(VectorToCell(player.Position))[i]).MapNames.Length)];   
            return null;//Annars null.
        }
        #endregion 
    }
}
