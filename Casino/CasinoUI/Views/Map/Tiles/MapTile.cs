using CasinoUI.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.View.Map.Tiles
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
        protected MapTile(int x, int y , Bitmap image, bool rotate) // change rotate type to int (angle) ? or create child class to bitmap + rotate property?
        {
            X = x;
            Y = y;
            if (rotate)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            Sprite = image.ToBitmapImage();
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
                    return new RedFloorTile(x, y , Properties.Resources.redfloor, rotate);
                case "Floor2":
                    return new BlackFloorTile(x, y, Properties.Resources.blackfloor, rotate);
                case "SlotMachine":
                    return new SlotMachineTile(x, y, Properties.Resources.slotmachinetemp, rotate);
                case "Bar":
                    return new BarTile(x, y, Properties.Resources.bartemp, rotate);
                case string str when IsChairTile(str):
                    return CreateChairTile(x, y, tileType, rotate);
                case string str when IsTableTile(str):
                    return CreateTableTile(x, y, tileType, rotate);
                default:
                    throw new ArgumentException($"Argument {tileType} is not valid");
            }
        }

        private static bool IsChairTile(string str)
        {
            return new string(str.Take(5).ToArray()) == "Table" &&
                   str[5] != '6' && str[5] != '7';
        }

        private static MapTile CreateChairTile(int x, int y, string tileType, bool rotate)
        {
            FirstCharToUpper(ref tileType);
            Bitmap image = Properties.Resources.ResourceManager.GetObject(tileType) as Bitmap;
            return new ChairTile(x, y , image, rotate);
        }

        private static void FirstCharToUpper(ref string str)
        {
            str = char.ToLower(str[0]) + str.Substring(1);
        }

        private static bool IsTableTile(string str)
        {
            return str == "Table6" || str == "Table7";
        }

        private static MapTile CreateTableTile(int x, int y, string tileType, bool rotate)
        {
            FirstCharToUpper(ref tileType);
            Bitmap image = Properties.Resources.ResourceManager.GetObject(tileType) as Bitmap;
            return new TableTile(x, y, image, rotate);
        }
    }
}
