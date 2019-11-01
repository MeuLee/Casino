using CasinoUI.Utils;
using CasinoUI.Views.Map.Tiles;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace CasinoUI.Views.Map
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
        /// <param name="width">2d array width</param>
        /// <param name="height">2d array height</param>
        /// <param name="topElem">XML file root element</param>
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

        /// <summary>
        /// Fills the 2d array passed in argument with the XML file content
        /// This method is slow as fuck.
        /// Either optimize it (not sure how tbh) or add a loading screen 
        /// </summary>
        /// <param name="topElem">XML file root element</param>
        /// <param name="map">The 2d array to be filled.</param>
        private static void FillMap(XElement topElem, MapTile[,] map)
        {
            var tilesArr = topElem.Elements().First(e => e.Name == "Tiles").Descendants().ToList();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < tilesArr.Count; i++)
            {
                var tile = tilesArr[i];
                int x = tile.Attribute("X").Value.ToInteger();
                int y = tile.Attribute("Y").Value.ToInteger();
                string floorType = tile.Attribute("Terrain").Value;
                bool rotate = tile.Attribute("Rotate").Value.ToBoolean();
                Stopwatch sw1 = new Stopwatch();
                sw1.Start();
                map[x, y] = MapTile.CreateMapTile(x, y, floorType, rotate);
                sw1.Stop();
                System.Console.WriteLine($"CreateMapTile: {sw1.ElapsedMilliseconds} ms. Type: {map[x, y].GetType()}");
            }
            sw.Stop();
            System.Console.WriteLine($"For loop: {sw.ElapsedMilliseconds / (double)1000} seconds");
        }
    }
}
