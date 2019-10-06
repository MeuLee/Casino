using CasinoUI.Utils;
using CasinoUI.View.Map.Tiles;
using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace CasinoUI.View.Map
{
    public class MapGenerator
    {

        public static MapTile[,] LoadMapFromFile(string content)
        {
            XElement topElem = XElement.Parse(content);
            GetDimensions(out int width, out int height, topElem);
            MapTile[,] map = CreateEmptyMapTile2dArr(width, height);
            FillMap(topElem, map);
            return map;
        }

        /// <summary>
        /// Not gonna try catch this, not my problem is the xml is not correctly formatted
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="topElem"></param>
        private static void GetDimensions(out int width, out int height, XElement topElem)
        {
            var dimensions = topElem.Elements().First(e => e.Name == "Dimensions").Descendants();
            width = dimensions.First(e => e.Name == "Width").Attribute("X").Value.ToInteger();
            height = dimensions.First(e => e.Name == "Height").Attribute("Y").Value.ToInteger();
        }

        private static MapTile[,] CreateEmptyMapTile2dArr(int width, int height)
        {
            return new MapTile[width, height];
        }

        private static void FillMap(XElement topElem, MapTile[,] map)
        {
            var tilesArr = topElem.Elements().First(e => e.Name == "Tiles").Descendants();

            foreach (var tile in tilesArr)
            {
                int x = tile.Attribute("X").Value.ToInteger();
                int y = tile.Attribute("Y").Value.ToInteger();
                string floorType = tile.Attribute("Terrain").Value;
                bool rotate = tile.Attribute("Rotate").Value.ToBoolean();
                map[x, y] = MapTile.CreateMapTile(x, y, floorType, rotate);
            }
        }
    }
}
