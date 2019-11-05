using CasinoUI.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views.Map.Tiles
{
    public abstract class MapTile
    {
        public Action<object, OnMovedOverEventArgs> OnMovedOver { get; set; } 
        public BitmapImage Sprite { get; private set; }
        public System.Windows.Media.Pen MiniMapPen { get; protected set; }
        public SolidColorBrush MiniMapBrush { get; protected set; }

        protected const double PEN_WIDTH = 0.75;

        /// <summary>
        /// Child classes call this ctor. Initializes properties and rotates the image if needed
        /// </summary>
        /// <param name="x">X coordinate on the map</param>
        /// <param name="y">Y coordinate on the map</param>
        /// <param name="image">Bitmap to be printed on screen representing this tile</param>
        protected MapTile(int x, int y , BitmapImage image, bool rotate) // change rotate type to int (angle) ? or create child class to bitmap + rotate property?
        {
            X = x;
            Y = y;
            if (rotate)
            {
                
            }
            Sprite = image;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        /// <summary>
        /// MapTile Factory method
        /// </summary>
        public static MapTile CreateMapTile(int x, int y, string tileType, bool rotate)
        {
            switch (tileType)
            {
                case "Floor1":
                    return new RedFloorTile(x, y);
                case "Floor2":
                    return new BlackFloorTile(x, y);
                case "SlotMachine":
                    return new SlotMachineTile(x, y);
                case "Bar":
                    return new BarTile(x, y);
                case string str when IsChairTile(str):
                    return CreateChairTile(x, y, tileType, rotate);
                case string str when IsTableTile(str):
                    return CreateTableTile(x, y, tileType, rotate);
            }
            throw new ArgumentException($"{tileType} argument is invalid");
        }

        private static MapTile CreateChairTile(int x, int y, string tileType, bool rotate)
        {
            FirstCharToLower(ref tileType);
            int index = int.Parse(tileType[5].ToString());
            BitmapImage image = rotate ? Tiles.GetRotatedTableBitmapImage(index) :
                                         Tiles.GetTableBitmapImage(index);
            return new ChairTile(x, y , image, rotate);
        }

        private static void FirstCharToLower(ref string str)
        {
            str = char.ToLower(str[0]) + str.Substring(1);
        }

        private static MapTile CreateTableTile(int x, int y, string tileType, bool rotate)
        {
            FirstCharToLower(ref tileType);
            int index = int.Parse(tileType[5].ToString());
            BitmapImage image = rotate ? Tiles.GetRotatedTableBitmapImage(index) :
                                         Tiles.GetTableBitmapImage(index);
            return new TableTile(x, y, image, rotate);
        }

        private static bool IsChairTile(string str)
        {
            return new string(str.Take(5).ToArray()) == "Table" &&
                   str[5] != '6' && str[5] != '7';
        }

        private static bool IsTableTile(string str)
        {
            return str == "Table6" || str == "Table7";
        }
    }
}
