using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework.Graphics;
using RPG.TileEngine.Tiles;

namespace RPG.TileEngine
{
    //Klass som enbart kommer att användas för att spara och ladda kartor.
    public static class MapFileManager
    {
        #region Methods

        //Metod för att spara ned kartan som objekt.
        public static void SaveMap(Map map, string mapPath)
        {
            FileStream fileStream = null;
            BinaryWriter binaryWriter = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                fileStream = File.Open(mapPath + "Map Settings" + ".txt", FileMode.Create);
                binaryWriter = new BinaryWriter(fileStream);
                //Kartans Tile-bredd spras ner.
                binaryWriter.Write(map.TileWidth);
                //Kartans Tile-höjd spras ner.
                binaryWriter.Write(map.TileHeight);
                //Kartans bredd i tiles sparas ner.
                binaryWriter.Write(map.TilesHorizontal);
                //Kartans höjd i tiles sparas ner.
                binaryWriter.Write(map.TilesVertical);
                //Hur många tile sheets kartan använder sig av sparas ner.
                binaryWriter.Write(map.NumberOfTileSheets);
                //Hur många lager som kartan består av spras ner.
                binaryWriter.Write(map.NumberOfLayers);
                fileStream.Close();
                binaryWriter.Close();
                //Alla lager sparas.
                for (int i = 0; i < map.NumberOfLayers; i++)
                {
                    fileStream = File.Open(mapPath + "Layer " + i + ".txt", FileMode.Create);
                    binaryFormatter.Serialize(fileStream, map.GetLayer(i).Tiles);
                    fileStream.Close();
                }
                //Vi skapar även kopior av alla tile sheets som kartan använder sig av.
                for (int i = 0; i < map.NumberOfTileSheets; i++)
                {
                    fileStream = File.OpenWrite(mapPath + "Tile Sheet " + i + ".png");
                    map.GetTileSheet(i).Texture.SaveAsPng(fileStream, map.GetTileSheet(i).Texture.Width, map.GetTileSheet(i).Texture.Height);
                    fileStream.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error, could not save map!\n "+ exception.Message.ToString());
            }
            finally
            {
                fileStream.Close();
                binaryWriter.Close();
            }
        }
        //Metod för att läsa in kartan som objekt.
        public static Map LoadMap(string mapPath, GraphicsDevice graphicsDevice, bool debug = false)
        {
            FileStream fileStream = null;
            BinaryReader binaryReader = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Map map = null;
            try
            {
                fileStream = new FileStream(mapPath, FileMode.Open);
                binaryReader = new BinaryReader(fileStream);
                //Map Settings-filens storlek.
                long mapSettingsFileLength = fileStream.Length;
                //Var vi befinner oss i Map Settings-filen.
                long mapSettingsFilePosition = 0;
                //Vi sparar alla kartinställningar tillfälligt i en lista.
                List<int> mapSettingInts = new List<int>();
                //Vi läser in kartinställningar.
                while (mapSettingsFilePosition < mapSettingsFileLength)
                {
                    mapSettingInts.Add(binaryReader.ReadInt32());
                    mapSettingsFilePosition += sizeof(int);
                }
                fileStream.Close();
                binaryReader.Close();
                //Vi har nu tillräckligt med information för att återskapa karta.
                map = new Map(Path.GetDirectoryName(mapPath), mapSettingInts[0], mapSettingInts[1], mapSettingInts[2], mapSettingInts[3], graphicsDevice.Viewport, debug);
                //Nu lägger vi till alla lager som kartan hade.
                for (int i = 0; i < mapSettingInts[5]; i++)
                {
                    if (i > 0)
                        map.AddLayer();
                    fileStream = File.Open(Path.GetDirectoryName(mapPath) + "/Layer " + i + ".txt", FileMode.Open);
                    Tile[,] tiles = (Tile[,])binaryFormatter.Deserialize(fileStream);
                    //Vi lägger till alla nersparade tiles till lagret.
                    for (int x = 0; x < map.TilesHorizontal; x++)
                        for (int y = 0; y < map.TilesVertical; y++)
                            if (tiles[x, y] != null)
                                map.GetLayer(i).SetTile(x, y, tiles[x, y]);
                    fileStream.Close();
                }
                //Till sist så lägger vi till kartans tile sheets.
                for (int i = 0; i < mapSettingInts[4]; i++)
                {
                    fileStream = File.Open(Path.GetDirectoryName(mapPath) + "/Tile Sheet " + i + ".png", FileMode.Open);
                    map.AddTileSheet(Texture2D.FromStream(graphicsDevice, fileStream));
                    fileStream.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error, could not load map!\n" + exception.Message.ToString());
            }
            finally
            {
                fileStream.Close();
                binaryReader.Close();
            }
            return map;
        }

        #endregion
    }
}
