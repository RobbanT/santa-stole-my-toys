using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using RPG.TileEngine;
using RPG.SpriteClasses;
using RPG.TileEngine.Tiles;

namespace TileEditor
{
    //Huvudformen för tile editorn.
    public partial class MainForm : Form
    {
        #region Fields

        //SpriteBatch så att vi kan måla upp kartan.
        private SpriteBatch spriteBatch;
        //Själva kartan som man hanterar.
        private Map map;
        //Texturer som editorn avnänder sig av.
        private Texture2D shadowTexture, lineTexture;
        //Färgen på rutnätet (både för kartan och tile sheeten).
        private Color gridColor = Color.White;
        //Variabel som håller reda på vilken tile vi befinner oss i med muspilen(på kartan).
        private Vector2 currentTilePosition = Vector2.Zero;
        //Storleken på tileborsten.
        private int tileBrushSize = 1;
        //Listan innehåller tile sheeten som pictureboxen använder sig av.
        private List<System.Drawing.Image> tileSheetImages = new List<System.Drawing.Image>();
        //Indexet till tilen som har markerats  på tilesheetet.
        private Point? currentTileSheetSourceRectangleIndex = null;
        //Muspositionen för musen(när den är på kartan).
        private Point mousePositionInsideMapDisplay = new Point();
        //Bool som kontrollerar om vänster musknapp är nere(på kartan).
        private bool leftMouseButtonDownInsideMapDisplay = false;
        //Bool som kontrollerar om höger musknapp är nere på kartan.
        private bool rightMouseButtonDownInsideMapDisplay = false;
        //Bool som kontrollerar om muspilen är på kartan.
        private bool mouseInsideMapDisplay = false;
        //Textur för rutnätet hos alla tile sheet.
        private System.Drawing.Pen gridPen = new System.Drawing.Pen(System.Drawing.Color.White, 1);
        //Bool som kontrollerar om vi befinner oss på en existerande tile.
        private bool onExistingTile = false;

        #endregion 

        #region Properties

        //Property som returnerar mapDisplayens graphics device.
        public GraphicsDevice GraphicsDevice { get { return mapDisplay.GraphicsDevice; } }

        #endregion 

        #region Constructor
 
        //Alla komponenter initieras.
        public MainForm() { InitializeComponent(); }

        #endregion

        #region Form Events

        //När man går ut från editorn så startar RPG-projektet.
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { Process.Start("RPG"); }

        //Map displayen uppdateras/målas upp med ett litet mellanrum.
        private void mapDisplayTimer_Tick(object sender, EventArgs e) { mapDisplay.Invalidate(); }

        #endregion

        #region Map Tool Strip Menu Clicks

        //Metod som körs när man trycker på ny kartan-knappen.
        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NewMapForm newMapForm = new NewMapForm())
            {
                //Det som händer när spelaren har tryckt på OK-knappen.
                if (newMapForm.ShowDialog() == DialogResult.OK)
                {
                    ComponentReset();
                    currentLayerCheckedListBox.Items.Add(currentLayerCheckedListBox.Items.Count);
                    currentLayerCheckedListBox.SetItemChecked(currentLayerCheckedListBox.Items.Count - 1, true);
                    currentLayerCheckedListBox.SelectedItem = currentLayerCheckedListBox.Items.Count - 1;
                    //Kartan skapas med de ifyllda värdena.
                    map = new Map(newMapForm.MapName, newMapForm.TileWidth, newMapForm.TileHeight, newMapForm.TilesHorizontal, newMapForm.TilesVertical,
                        new Viewport(new Rectangle(mapDisplay.Location.X, mapDisplay.Location.Y, mapDisplay.Width, mapDisplay.Height)), true);
                }
            }
        }

        //Metod som körs när vi laddar en karta.
        private void openMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //En ny form öppnas där spelaren får ange sökvägen till sitt tile sheet.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string mapPath = openFileDialog.FileName;
                ComponentReset();
                map = MapFileManager.LoadMap(mapPath, GraphicsDevice, true);
                //Tile sheets för tile editorn laddas in.
                Stream stream = null;
                try
                {
                    for (int i = 0; i < map.NumberOfTileSheets; i++)
                    {
                        stream = new FileStream(Path.GetDirectoryName(mapPath) + "\\Tile Sheet " + i + ".png", FileMode.Open);
                        tileSheetImages.Add(System.Drawing.Image.FromStream(stream));
                        currentTileSheetUpDown.Items.Add(currentTileSheetUpDown.Items.Count);
                        stream.Close();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error, could not load tile sheets for tile editor!\n" + exception.Message.ToString());
                }
                finally
                {
                    stream.Close();
                }
                currentTileSheetUpDown.SelectedIndex = currentTileSheetUpDown.Items.Count - 1;
                if (!tileSheetPictureBox.Enabled)
                {
                    removeTileSheetToolStripMenuItem.Enabled = true;
                    tileSheetPictureBox.Enabled = true;
                    currentTileModeUpDown.Enabled = true;
                    currentTileModeUpDown.SelectedIndex = 0;
                    currentTileSheetUpDown.Enabled = true;
                }

                for (int i = 0; i < map.NumberOfLayers; i++)
                {
                    currentLayerCheckedListBox.Items.Add(currentLayerCheckedListBox.Items.Count);
                    currentLayerCheckedListBox.SetItemChecked(currentLayerCheckedListBox.Items.Count - 1, true);
                }
                currentLayerCheckedListBox.SelectedIndex = currentLayerCheckedListBox.Items.Count - 1;
                if (!currentLayerCheckedListBox.Enabled)
                {
                    saveMapToolStripMenuItem.Enabled = true;
                    currentLayerCheckedListBox.Enabled = true;
                    removeLayerToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Metod som körs när man trycker på spara kartan-knappen.
        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //En ny form öppnas där spelaren får välja i vilken katalog han vill spara kartan i.
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select Save Path";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string mapPath = Path.Combine(folderBrowserDialog.SelectedPath, map.MapName + "\\");
                    Directory.CreateDirectory(Path.Combine(mapPath));
                    MapFileManager.SaveMap(map, mapPath);
                }
            }
        }

        //Metod som gör gör så att komponenterna får sitt standardstatus.
        private void ComponentReset()
        {
            //Komponenter förbereds.
            if (map == null)
            {
                layerToolStripMenuItem.Enabled = true;
                tileSheetToolStripMenuItem.Enabled = true;
                removeTileSheetToolStripMenuItem.Enabled = false;
                gridToolStripMenuItem.Enabled = true;
                tileBrushToolStripMenuItem.Enabled = true;
                zoomToolStripMenuItem.Enabled = true;
                currentLayerCheckedListBox.Enabled = true;
                saveMapToolStripMenuItem.Enabled = true;
            }
            else
            {
                x1ToolStripMenuItem_Click(null, null);
                zoom100ToolStripMenuItem_Click(null, null);
                tileSheetImages.Clear();
                tileSheetPictureBox.Image = null;
                tileSheetPictureBox.Enabled = false;
                currentTileModeUpDown.SelectedIndex = 0;
                currentTileModeUpDown.Enabled = false;
                currentTileSheetUpDown.Items.Clear();
                currentTileSheetUpDown.Enabled = false;
                currentLayerCheckedListBox.Items.Clear();
                currentTileSheetUpDown.ResetText();
            }
        }

        #endregion 

        #region Layer Tool Strip Menu Clicks

        //Ska ett nytt lager läggas till så körs den här metoden.
        private void newLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Ett nytt lager skapas.
            map.AddLayer();
            //Komponenter förbereds.
            currentLayerCheckedListBox.Items.Add(currentLayerCheckedListBox.Items.Count);
            currentLayerCheckedListBox.SetItemChecked(currentLayerCheckedListBox.Items.Count - 1, true);
            currentLayerCheckedListBox.SelectedIndex = currentLayerCheckedListBox.Items.Count - 1;
            if (!currentLayerCheckedListBox.Enabled)
            {
                saveMapToolStripMenuItem.Enabled = true;
                currentLayerCheckedListBox.Enabled = true;
                removeLayerToolStripMenuItem.Enabled = true;
            }

        }

        //Ska valt lager tas bort så körs den här metoden.
        private void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Valt lager tas bort.
            map.RemoveLayer(currentLayerCheckedListBox.SelectedIndex);
            currentLayerCheckedListBox.Items.RemoveAt(currentLayerCheckedListBox.SelectedIndex);

            //Komponenter förbereds.
            if (currentLayerCheckedListBox.Items.Count == 0)
            {
                currentLayerCheckedListBox.ResetText();
                currentLayerCheckedListBox.Enabled = false;
                removeLayerToolStripMenuItem.Enabled = false;
                saveMapToolStripMenuItem.Enabled = false;
                return;
            }

            for (int i = 0; i < currentLayerCheckedListBox.Items.Count; i++)
                currentLayerCheckedListBox.Items[i] = i;
            currentLayerCheckedListBox.SelectedIndex = currentLayerCheckedListBox.Items.Count - 1;
        }

        #endregion

        #region Tile Sheet Tool Strip Menu Clicks

        //Ska ett nytt tile sheet lägas till så körs den här metoden.
        private void newTileSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (NewTileSheetForm newTileSheetForm = new NewTileSheetForm())
            {
                Stream stream = null;
                if (newTileSheetForm.ShowDialog() == DialogResult.OK)
                {
                    //Vi kontrollerar om tile sheetet är delbart med kartans tile- bredd och höjd.
                    //Är den inte det så fångas en exception.
                    try
                    {
                        stream = new FileStream(newTileSheetForm.TileSheetPath, FileMode.Open, FileAccess.Read);
                        map.AddTileSheet(Texture2D.FromStream(GraphicsDevice, stream));
                        tileSheetImages.Add(System.Drawing.Image.FromStream(stream));
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                        return;
                    }
                    finally { if (stream != null) stream.Close(); }

                    //Komponenter ändras.
                    currentTileSheetUpDown.Items.Add(currentTileSheetUpDown.Items.Count);
                    currentTileSheetUpDown.SelectedIndex = currentTileSheetUpDown.Items.Count - 1;
                    if (!tileSheetPictureBox.Enabled)
                    {
                        removeTileSheetToolStripMenuItem.Enabled = true;
                        tileSheetPictureBox.Enabled = true;
                        currentTileModeUpDown.Enabled = true;
                        currentTileModeUpDown.SelectedIndex = 0;
                        currentTileSheetUpDown.Enabled = true;
                    }
                }
            }
        }

        //Ska ett tile sheet tas bort så körs den här metoden.
        private void removeTileSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map.RemoveTileSheet(currentTileSheetUpDown.SelectedIndex);
            tileSheetImages.RemoveAt(currentTileSheetUpDown.SelectedIndex);
            currentTileSheetUpDown.Items.RemoveAt(currentTileSheetUpDown.SelectedIndex);

            if (currentTileSheetUpDown.Items.Count == 0)
            {
                tileSheetPictureBox.Image = null;
                tileSheetPictureBox.Enabled = false;
                currentTileSheetUpDown.ResetText();
                currentTileSheetUpDown.Enabled = false;
                removeTileSheetToolStripMenuItem.Enabled = false;
                return;
            }

            for (int i = 0; i < currentTileSheetUpDown.Items.Count; i++)
                currentTileSheetUpDown.Items[i] = i;
            currentTileSheetUpDown.SelectedIndex = currentTileSheetUpDown.Items.Count - 1;
            currentTileSheetUpDown_SelectedItemChanged(null, null);
        }

        #endregion

        #region Grid Tool Strip Menu Clicks

        //Metoderna nedanför används för att ändra färgen på kartans och tilesheetens rutnät.
        //Vit
        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.White;
            gridPen.Color = System.Drawing.Color.White;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = true;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = false;
        }

        //Blå
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Blue;
            gridPen.Color = System.Drawing.Color.Blue;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = true;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = false;
        }

        //Gul
        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Yellow;
            gridPen.Color = System.Drawing.Color.Yellow;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = true;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = false;
        }

        //Grön
        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Green;
            gridPen.Color = System.Drawing.Color.Green;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = true;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = false;
        }

        //Röd
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Red;
            gridPen.Color = System.Drawing.Color.Red;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = true;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = false;
        }

        //Svart
        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Black;
            gridPen.Color = System.Drawing.Color.Black;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = true;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = false;
        }

        //Magenta
        private void magentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Magenta;
            gridPen.Color = System.Drawing.Color.Magenta;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = true;
            cyanToolStripMenuItem.Checked = false;
        }

        //Turkos
        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Cyan;
            gridPen.Color = System.Drawing.Color.Cyan;
            tileSheetPictureBox.Invalidate();
            whiteToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            magentaToolStripMenuItem.Checked = false;
            cyanToolStripMenuItem.Checked = true;
        }

        #endregion

        #region Tile Brush Tool Strip Menu Clicks

        //Metoderna nedanför används för att ändra storleken på tile-borsten.
        //1x1
        private void x1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tileBrushSize = 1;
            x1ToolStripMenuItem.Checked = true;
            x2ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x8ToolStripMenuItem.Checked = false;
            x16ToolStripMenuItem.Checked = false;
        }

        //2x2
        private void x2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tileBrushSize = 2;
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = true;
            x4ToolStripMenuItem.Checked = false;
            x8ToolStripMenuItem.Checked = false;
            x16ToolStripMenuItem.Checked = false;
        }

        //4x4
        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tileBrushSize = 4;
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = true;
            x8ToolStripMenuItem.Checked = false;
            x16ToolStripMenuItem.Checked = false;
        }

        //8x8
        private void x8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tileBrushSize = 8;
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x8ToolStripMenuItem.Checked = true;
            x16ToolStripMenuItem.Checked = false;
        }

        //16x16
        private void x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tileBrushSize = 16;
            x1ToolStripMenuItem.Checked = false;
            x2ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = false;
            x8ToolStripMenuItem.Checked = false;
            x16ToolStripMenuItem.Checked = true;
        }

        #endregion

        #region Zoom Tool Strip Menu Clicks

        //Metoder som körs när zoomen ändras.

        //50% zoom.
        private void zoom50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map.Camera.Zoom = 0.50f;
            zoom50ToolStripMenuItem.Checked = true;
            zoom75ToolStripMenuItem.Checked = false;
            zoom100ToolStripMenuItem.Checked = false;
        }
        //75% zoom.
        private void zoom75ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map.Camera.Zoom = 0.75f;
            zoom50ToolStripMenuItem.Checked = false;
            zoom75ToolStripMenuItem.Checked = true;
            zoom100ToolStripMenuItem.Checked = false;
        }
        //100% zoom.
        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map.Camera.Zoom = 1.00f;
            zoom50ToolStripMenuItem.Checked = false;
            zoom75ToolStripMenuItem.Checked = false;
            zoom100ToolStripMenuItem.Checked = true;
        }

        #endregion

        #region Current Tile Sheet UpDown

        //Bilden i picture boxen ändras.
        private void currentTileSheetUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            if (currentTileSheetUpDown.Items.Count > 0 && currentTileSheetUpDown.SelectedIndex >= 0)
                tileSheetPictureBox.Image = tileSheetImages[currentTileSheetUpDown.SelectedIndex];
            currentTileSheetSourceRectangleIndex = null;
        }

        #endregion

        #region Tile Sheet Picture Box

        //Metoden målar upp något ovanför bilden i bildboxen.
        private void tileSheetPictureBox_Paint(object sender, PaintEventArgs e)
        {
            //Ett rutnät målas upp ovanpå tilesheetet
            if (displayGridToolStripMenuItem.Checked && tileSheetPictureBox.Image != null)
            {
                for (int x = 0; x < map.GetTileSheet(currentTileSheetUpDown.SelectedIndex).TilesHorizontal; x++)
                {
                    e.Graphics.DrawLine(gridPen, x * map.TileWidth, 0, x * map.TileWidth, tileSheetPictureBox.Image.Height);
                    for (int y = 0; y < map.GetTileSheet(currentTileSheetUpDown.SelectedIndex).TilesVertical; y++)
                        e.Graphics.DrawLine(gridPen, 0, y * map.TileHeight, tileSheetPictureBox.Image.Width, y * map.TileHeight);
                }
                e.Graphics.DrawLine(gridPen, tileSheetPictureBox.Image.Width-1, 0, tileSheetPictureBox.Image.Width - 1, tileSheetPictureBox.Image.Height);
                e.Graphics.DrawLine(gridPen, 0, tileSheetPictureBox.Image.Height - 1, tileSheetPictureBox.Image.Width, tileSheetPictureBox.Image.Height - 1);
            }
        }

        //Klickar vi på en tile i bildboxen så vill vi ha dess sourceRectangleIndex.
        private void tileSheetPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            currentTileSheetSourceRectangleIndex = new Point(e.X / map.TileWidth, e.Y / map.TileHeight);
        }

        private void displayGridToolStripMenuItem_Click(object sender, EventArgs e) { tileSheetPictureBox.Invalidate(); }

        #endregion

        #region Map Display

        //Initiering av komponent (variabler tilldelas ett objekt).
        private void mapDisplay_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            shadowTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Color[] data = new Color[shadowTexture.Width * shadowTexture.Height];
            for (int i = 0; i < shadowTexture.Width * shadowTexture.Height; i++)
                data[i] = Color.White;
            shadowTexture.SetData<Color>(data);

            lineTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            data = new Color[lineTexture.Width * lineTexture.Height];
            for (int i = 0; i < lineTexture.Width * lineTexture.Height; i++)
                data[i] = Color.White;
            lineTexture.SetData<Color>(data);
        }

        //Komponenten kör sin draw-metod(logic och render).
        private void mapDisplay_OnDraw(object sender, EventArgs e)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Har en karta skapats så ska vi uppdatera och måla upp den.
            if (map != null)
            {
                Logic();
                Render();
            }
        }

        //Här uppdateras editorns logik.
        private void Logic()
        {
            //Vilket lager som ska synas uppdateras här.
            for (int i = 0; i < map.NumberOfLayers; i++)
                if (currentLayerCheckedListBox.GetItemChecked(i))
                    map.GetLayer(i).Visible = true;
                else
                    map.GetLayer(i).Visible = false;

            //Får inte kartan plats på skärmen så ska den börja skrolla när man håller musen vid någon av kanterna.
            if (mousePositionInsideMapDisplay.X < 32 && mouseInsideMapDisplay)
                map.Camera.Position = new Vector2(map.Camera.Position.X - map.Camera.TransformFloat(map.TileWidth), map.Camera.Position.Y);

            if (mousePositionInsideMapDisplay.X > mapDisplay.Width - 32 && mouseInsideMapDisplay)
                map.Camera.Position = new Vector2(map.Camera.Position.X + map.Camera.TransformFloat(map.TileWidth), map.Camera.Position.Y);

            if (mousePositionInsideMapDisplay.Y < 32 && mouseInsideMapDisplay)
                map.Camera.Position = new Vector2(map.Camera.Position.X, map.Camera.Position.Y - map.Camera.TransformFloat(map.TileHeight));

            if (mousePositionInsideMapDisplay.Y > mapDisplay.Height - 32 && mouseInsideMapDisplay)
                map.Camera.Position = new Vector2(map.Camera.Position.X, map.Camera.Position.Y + map.Camera.TransformFloat(map.TileHeight));

            //Vi räknar ut i vilken tile som muspilen finns i.
            Point tile = map.TransformedVectorToCell(new Vector2(mousePositionInsideMapDisplay.X + map.Camera.Position.X, mousePositionInsideMapDisplay.Y + map.Camera.Position.Y));

            //Vi kontrollerar om den angivna tile-positionen är giltlig.
            if (tile.X < 0 || tile.Y < 0 || tile.X >= map.TilesHorizontal || tile.Y >= map.TilesVertical)
            {
                tileCoordinatesStatusLabel.Text = "Tile Coordinates: ";
                onExistingTile = false;
                return;
            }
            onExistingTile = true;

            //Tile-koordinater skrivs ut.
            if (mouseInsideMapDisplay)
            {
                currentTilePosition = (new Vector2(tile.X, tile.Y));
                tileCoordinatesStatusLabel.Text = "Tile Coordinates: " + "( " + tile.X.ToString() + ", " + tile.Y.ToString() + " )";
            }

            //Vi vill bara sätta ut tiles om åtminstone ett lager existerar.
            if (map.NumberOfLayers > 0)
            {
                //Nya tiles läggs eventuellt ut.
                if (leftMouseButtonDownInsideMapDisplay && currentTileSheetSourceRectangleIndex != null)
                    AddTiles(tile, (Point)currentTileSheetSourceRectangleIndex);
                //Tiles tas bort.
                else if (rightMouseButtonDownInsideMapDisplay)
                    RemoveTiles(tile);
            }
        }

        //Metod som målar upp kartan(även andra saker som är unikt för editorn).
        private void Render()
        {
            //Vi vill inte måla upp något om kartan är lika med null.
            if (map == null)
                return;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, map.Camera.TransformationMatrix);
            //Kartan målar upp sina lager.
            map.Draw(spriteBatch);
            //Här målar vi upp "tile-mörkret" om muspilen befinner sig på kartan.
            if (mouseInsideMapDisplay && onExistingTile)
                spriteBatch.Draw(shadowTexture,
                new Rectangle((int)currentTilePosition.X * map.TileWidth, (int)currentTilePosition.Y * map.TileHeight, tileBrushSize * map.TileWidth, tileBrushSize * map.TileHeight),
                new Color(0, 0, 255, 128));

            //Rutnätet målas upp(eventuellt)
            if (displayGridToolStripMenuItem.Checked)
                for (int x = map.CameraPoint.X; x < map.ViewPoint.X; x++)
                    for (int y = map.CameraPoint.Y; y < map.ViewPoint.Y; y++)
                    {
                        spriteBatch.Draw(lineTexture, new Rectangle(x * map.TileWidth, y * map.TileHeight, map.TileWidth, (int)((1 / map.Camera.Zoom) / 1)), gridColor);
                        spriteBatch.Draw(lineTexture, new Rectangle(x * map.TileWidth, y * map.TileHeight + map.TileHeight, map.TileWidth, (int)((1 / map.Camera.Zoom) / 1)), gridColor);
                        spriteBatch.Draw(lineTexture, new Rectangle(x * map.TileWidth, y * map.TileHeight, (int)((1 / map.Camera.Zoom) / 1), map.TileHeight), gridColor);
                        spriteBatch.Draw(lineTexture, new Rectangle(x * map.TileWidth + map.TileWidth, y * map.TileHeight, (int)((1 / map.Camera.Zoom) / 1), map.TileHeight), gridColor);
                    }
            spriteBatch.End();


            spriteBatch.Begin();
            //Här målar vi upp den temporära tilen(den som har markerats i valt tile sheet) på muspilens position.
            if (mouseInsideMapDisplay && currentTileSheetSourceRectangleIndex != null)
            {
                Point tempPoint = (Point)currentTileSheetSourceRectangleIndex;
                spriteBatch.Draw(map.GetTileSheet(currentTileSheetUpDown.SelectedIndex).Texture,
                    new Vector2(mousePositionInsideMapDisplay.X, mousePositionInsideMapDisplay.Y),
                map.GetTileSheet(currentTileSheetUpDown.SelectedIndex).GetSourceRectangle(tempPoint.X, tempPoint.Y), Color.White);
            }
            spriteBatch.End();
        }


        #region Map Display Mouse
 
        //Är muspilen på kartan så markerar vi det.
        private void mapDisplay_MouseEnter(object sender, EventArgs e) { mouseInsideMapDisplay = true; }

        //Är muspilen utanför kartan så markerar vi det.
        private void mapDisplay_MouseLeave(object sender, EventArgs e) { mouseInsideMapDisplay = false; }

        //Rör man på muspilen(på kartan) så vill vi ha muspilens nya position.
        private void mapDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            mousePositionInsideMapDisplay.X = e.X;
            mousePositionInsideMapDisplay.Y = e.Y;
        }

        //Har vänster/höger musknapp släppts så markerar vi det.
        private void mapDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMouseButtonDownInsideMapDisplay = false;

            if (e.Button == MouseButtons.Right)
                rightMouseButtonDownInsideMapDisplay = false;
        }

        //Har vänster/höger musknapp tryckts ned så markerar vi det.
        private void mapDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMouseButtonDownInsideMapDisplay = true;
            else if (e.Button == MouseButtons.Right)
                rightMouseButtonDownInsideMapDisplay = true;
        }

        #endregion

        #region Map Display Size Changed

        //Ändrar vi storleken på editor-fönstret så måste vi uppdatera kartans kamera.
        private void mapDisplay_SizeChanged(object sender, EventArgs e)
        {
            //Hanterar editorn en karta så ska dess kamera uppdateras.
            if (map != null)
                map.Camera.Viewport = new Viewport(mapDisplay.Location.X, mapDisplay.Location.Y, mapDisplay.Width, mapDisplay.Height);
            mapDisplay.Invalidate();
        }

        #endregion

        #region Tile Methods

        //Metoden används om vi vill lägga till en eller fler tiles på valt lager.
        private void AddTiles(Point tileCoordinates, Point tileSheetSourceRectangleIndex)
        {
            for (int x = 0; x < tileBrushSize; x++)
            {
                //Kontroll så att tilen vi försöker lägga till verkligen existerar.
                if (tileCoordinates.X + x >= map.TilesHorizontal)
                    break;

                for (int y = 0; y < tileBrushSize; y++)
                {
                    //Kontroll så att tilen vi försöker lägga till verkligen existerar.
                    if (tileCoordinates.Y + y >= map.TilesVertical)
                        break;

                    //Switch som avgör vilken sorts tile som ska sättas ut.
                    switch (currentTileModeUpDown.SelectedIndex)
                    {
                        //Passable
                        case 0:
                            //Inga begränsningar för vanliga tiles.
                            map.GetLayer(currentLayerCheckedListBox.SelectedIndex).SetTile(tileCoordinates.X + x, tileCoordinates.Y + y,
                                new Tile(currentTileSheetUpDown.SelectedIndex, new Point(tileSheetSourceRectangleIndex.X, tileSheetSourceRectangleIndex.Y), TileMode.Passable));
                            break;
                        //Impassable
                        case 1:
                            //Man ska endast kunna placera impassable tiles i lager 0 och 1.
                            if (currentLayerCheckedListBox.SelectedIndex > 1)
                            {
                                leftMouseButtonDownInsideMapDisplay = false;
                                MessageBox.Show("Error, you can only place impassable tiles in layer 0 and 1.");
                                return;
                            }
                            else if (currentLayerCheckedListBox.SelectedIndex == 0)
                            {
                                //Är valt lager 0 så måste vi kontrollerar vad tilen i lagret över är för sort (så att man t.ex. inte kan placera en impassable tile under en impassable tile).
                                if (currentLayerCheckedListBox.Items.Count >= 2)
                                {
                                    Tile tileAbove = map.GetLayer(1).GetTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                                    if (tileAbove != null)
                                    {
                                        if (tileAbove is DoorTile || tileAbove is EnemyStartPositionTile || tileAbove is EntranceTile ||
                                            tileAbove is PlayerStartPositionTile || tileAbove.TileMode == TileMode.Impassable)
                                        {
                                            leftMouseButtonDownInsideMapDisplay = false;
                                            MessageBox.Show("Error, you can't place a impassable tile here because of the tile in layer 1 (at the same coordinate).");
                                            return;
                                        }
                                    }
                                }
                            }
                            else if (currentLayerCheckedListBox.SelectedIndex == 1)
                            {
                                //Är valt lager 1 så måste vi kontrollerar vad tilen i lagret under är för sort (så att man t.ex. inte kan placera en impassable tile på en impassable tile).
                                Tile tileBelow = map.GetLayer(0).GetTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                                if (tileBelow != null)
                                {
                                    if (tileBelow is DoorTile || tileBelow is EnemyStartPositionTile || tileBelow is EntranceTile ||
                                        tileBelow is PlayerStartPositionTile || tileBelow.TileMode == TileMode.Impassable)
                                    {
                                        leftMouseButtonDownInsideMapDisplay = false;
                                        MessageBox.Show("Error, you can't place a impassable tile here because of the tile in layer 0 (at the same coordinate).");
                                        return;
                                    }
                                }
                            }
                            map.GetLayer(currentLayerCheckedListBox.SelectedIndex).SetTile(tileCoordinates.X + x, tileCoordinates.Y + y,
                                new Tile(currentTileSheetUpDown.SelectedIndex, new Point(tileSheetSourceRectangleIndex.X, tileSheetSourceRectangleIndex.Y), TileMode.Impassable));
                            break;
                        //Doortile
                        case 2:
                            //Man ska endast kunna placera door tiles i lager 0 och 1.
                            if (currentLayerCheckedListBox.SelectedIndex > 1)
                            {
                                leftMouseButtonDownInsideMapDisplay = false;
                                MessageBox.Show("Error, you can only place door tiles in layer 0 and 1.");
                                return;
                            }
                            else if (currentLayerCheckedListBox.SelectedIndex == 0)
                            {
                                //Är valt lager 0 så måste vi kontrollerar vad tilen i lagret över är för sort (så att man t.ex. inte kan placera en door tile under en door tile).
                                if (currentLayerCheckedListBox.Items.Count >= 2)
                                {
                                    Tile tileAbove = map.GetLayer(1).GetTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                                    if (tileAbove != null)
                                    {
                                        if (tileAbove is DoorTile || tileAbove is EnemyStartPositionTile || tileAbove is EntranceTile ||
                                            tileAbove is PlayerStartPositionTile || tileAbove.TileMode == TileMode.Impassable)
                                        {
                                            leftMouseButtonDownInsideMapDisplay = false;
                                            MessageBox.Show("Error, you can't place a door tile tile here because of the tile in layer 1 (at the same coordinate).");
                                            return;
                                        }
                                    }
                                }
                            }
                            else if (currentLayerCheckedListBox.SelectedIndex == 1)
                            {
                                //Är valt lager 1 så måste vi kontrollerar vad tilen i lagret under är för sort (så att man t.ex. inte kan placera en door tile på en door tile).
                                Tile tileBelow = map.GetLayer(0).GetTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                                if (tileBelow != null)
                                {
                                    if (tileBelow is DoorTile || tileBelow is EnemyStartPositionTile || tileBelow is EntranceTile ||
                                        tileBelow is PlayerStartPositionTile || tileBelow.TileMode == TileMode.Impassable)
                                    {
                                        leftMouseButtonDownInsideMapDisplay = false;
                                        MessageBox.Show("Error, you can't place a door tile here because of the tile in layer 0 (at the same coordinate).");
                                        return;
                                    }
                                }
                            }
                            map.GetLayer(currentLayerCheckedListBox.SelectedIndex).SetTile(tileCoordinates.X + x, tileCoordinates.Y + y,
                                new DoorTile(currentTileSheetUpDown.SelectedIndex, new Point(tileSheetSourceRectangleIndex.X, tileSheetSourceRectangleIndex.Y)));
                            break;
                        //Enemy start position tile
                        case 3:
                            //Man ska endast kunna placera start position tiles för fiender i lager 0.
                            if (currentLayerCheckedListBox.SelectedIndex != 0)
                            {
                                leftMouseButtonDownInsideMapDisplay = false;
                                MessageBox.Show("Error, you can only place start position tiles for enemies in layer 0");
                                return;
                            }
                            map.GetLayer(currentLayerCheckedListBox.SelectedIndex).SetTile(tileCoordinates.X + x, tileCoordinates.Y + y,
                                new EnemyStartPositionTile(currentTileSheetUpDown.SelectedIndex, new Point(tileSheetSourceRectangleIndex.X, tileSheetSourceRectangleIndex.Y)));
                            break;
                        //Entrance tile
                        case 4:
                            //Man ska endast kunna placera entrance tiles i lager 0 och 1.
                            if (currentLayerCheckedListBox.SelectedIndex > 1)
                            {
                                leftMouseButtonDownInsideMapDisplay = false;
                                MessageBox.Show("Error, you can only place entrance tiles in layer 0 and 1.");
                                return;
                            }
                            else if (currentLayerCheckedListBox.SelectedIndex == 0)
                            {
                                //Är valt lager 0 så måste vi kontrollerar vad tilen i lagret över är för sort (så att man t.ex. inte kan placera en entrance tile under en entrance tile).
                                if (currentLayerCheckedListBox.Items.Count >= 2)
                                {
                                    Tile tileAbove = map.GetLayer(1).GetTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                                    if (tileAbove != null)
                                    {
                                        if (tileAbove is DoorTile || tileAbove is EnemyStartPositionTile || tileAbove is EntranceTile ||
                                            tileAbove is PlayerStartPositionTile || tileAbove.TileMode == TileMode.Impassable)
                                        {
                                            leftMouseButtonDownInsideMapDisplay = false;
                                            MessageBox.Show("Error, you can't place a entrance tile here because of the tile in layer 1 (at the same coordinate).");
                                            return;
                                        }
                                    }
                                }
                            }
                            else if (currentLayerCheckedListBox.SelectedIndex == 1)
                            {
                                //Är valt lager 1 så måste vi kontrollerar vad tilen i lagret under är för sort (så att man t.ex. inte kan placera en entrance tile på en entrance tile).
                                Tile tileBelow = map.GetLayer(0).GetTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                                if (tileBelow != null)
                                {
                                    if (tileBelow is DoorTile || tileBelow is EnemyStartPositionTile || tileBelow is EntranceTile ||
                                        tileBelow is PlayerStartPositionTile || tileBelow.TileMode == TileMode.Impassable)
                                    {
                                        leftMouseButtonDownInsideMapDisplay = false;
                                        MessageBox.Show("Error, you can't place a entrance tile here because of the tile in layer 0 (at the same coordinate).");
                                        return;
                                    }
                                }
                            }
                            leftMouseButtonDownInsideMapDisplay = false;
                            //Ska en entrance tile skapas måste man ange vilka banor som tilen ska vara ingång till.
                            using (NewEntranceTileForm newEntranceTileForm = new NewEntranceTileForm())
                            {
                                //Det som händer när spelaren har tryckt på OK-knappen.
                                if (newEntranceTileForm.ShowDialog() == DialogResult.OK)
                                {
                                    map.GetLayer(currentLayerCheckedListBox.SelectedIndex).SetTile(tileCoordinates.X + x, tileCoordinates.Y + y,
                                        new EntranceTile(currentTileSheetUpDown.SelectedIndex, new Point(tileSheetSourceRectangleIndex.X, tileSheetSourceRectangleIndex.Y), newEntranceTileForm.MapNames));
                                }
                            }
                            break;
                        //Player start position tile
                        case 5:
                            //Man ska endast kunna placera EN(1) player start position tile i lager 0.
                            if (currentLayerCheckedListBox.SelectedIndex != 0)
                            {
                                leftMouseButtonDownInsideMapDisplay = false;
                                MessageBox.Show("Error, you can only place a start position tile for the player in layer 0");
                                return;
                            }
                            else if (map.StartPositionTileExist() && !(map.GetLayer(currentLayerCheckedListBox.SelectedIndex).GetTile(tileCoordinates.X + x,
                                tileCoordinates.Y + y) is PlayerStartPositionTile))
                            {
                                //Kontroll så att ingen annan start position tile för spelaren existerar.
                                leftMouseButtonDownInsideMapDisplay = false;
                                MessageBox.Show("Error, you can't place more than 1 start position tile for the player.");
                                return;
                            }
                            map.GetLayer(currentLayerCheckedListBox.SelectedIndex).SetTile(tileCoordinates.X + x, tileCoordinates.Y + y,
                                new PlayerStartPositionTile(currentTileSheetUpDown.SelectedIndex, new Point(tileSheetSourceRectangleIndex.X, tileSheetSourceRectangleIndex.Y)));
                            break;
                    }
                }
            }
        }

        //Metoden används om vi vill ta bort en eller fler tiles på valt lager.
        private void RemoveTiles(Point tileCoordinates)
        {
            for (int x = 0; x < tileBrushSize; x++)
            {
                //Kontroll så att tilen vi försöker ta bort verkligen existerar.
                if (tileCoordinates.X + x >= map.TilesHorizontal)
                    break;

                for (int y = 0; y < tileBrushSize; y++)
                {
                    //Kontroll så att tilen vi försöker ta bort verkligen existerar.
                    if (tileCoordinates.Y + y >= map.TilesVertical)
                        break;
                        //Tile tas bort.
                        map.GetLayer(currentLayerCheckedListBox.SelectedIndex).RemoveTile(tileCoordinates.X + x, tileCoordinates.Y + y);
                }
            }
        }

        #endregion

        #endregion
    }
}
