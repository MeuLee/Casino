using CasinoUI.Utils;
using CasinoUI.View.Map.Tiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
                int x1 = tile.Attribute("X1").Value.ToInteger();
                int y1 = tile.Attribute("Y1").Value.ToInteger();
                int x2 = tile.Attribute("X2").Value.ToInteger();
                int y2 = tile.Attribute("Y2").Value.ToInteger();
                string floorType = tile.Attribute("Terrain").Value;
                FillMap(x1, y1, x2, y2, floorType, map);
            }
        }

        private static void FillMap(int x1, int y1, int x2, int y2, string floorType, MapTile[,] map)
        {
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    map[i, j] = MapTile.CreateMapTile(i, j, floorType);
                }
            }
        }
    }
}
