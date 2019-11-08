using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views.Map.Tiles
{
    public abstract class MapTile
    {
        public Action<object, OnMovedOverEventArgs> OnMovedOver { get; protected set; } 
        public BitmapImage Sprite { get; private set; }
        public Pen MiniMapPen { get; protected set; }
        public SolidColorBrush MiniMapBrush { get; protected set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        protected const double PEN_WIDTH = 0.75;

        /// <summary>
        /// Child classes call this ctor. Initializes properties
        /// </summary>
        /// <param name="x">X coordinate on the map</param>
        /// <param name="y">Y coordinate on the map</param>
        /// <param name="image">BitmapImage to be printed on screen representing this tile</param>
        protected MapTile(int x, int y , BitmapImage image)
        {
            X = x;
            Y = y;
            Sprite = image;
        }

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

        private static bool IsChairTile(string str)
        {
            return new string(str.Take(5).ToArray()) == "Table" &&
                   str[5] != '6' && str[5] != '7';
        }

        private static MapTile CreateChairTile(int x, int y, string tileType, bool rotate)
        {
            return new ChairTile(x, y , GetChairOrTableImage(tileType, rotate));
        }

        private static BitmapImage GetChairOrTableImage(string tileType, bool rotate)
        {
            int index = int.Parse(tileType.Substring(5)) - 1;
            return rotate ? Tiles.GetRotatedTableBitmapImage(index) :
                                         Tiles.GetTableBitmapImage(index);
        }

        private static bool IsTableTile(string str)
        {
            return str == "Table6" || str == "Table7";
        }

        private static MapTile CreateTableTile(int x, int y, string tileType, bool rotate)
        {
            return new TableTile(x, y, GetChairOrTableImage(tileType, rotate));
        }
    }
}
