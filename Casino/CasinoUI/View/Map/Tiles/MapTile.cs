using CasinoUI.Model;
using CasinoUI.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CasinoUI.View.Map.Tiles
{
    public abstract class MapTile
    {
        public delegate void MovedOver(HumanPlayer human); // would it work? 
        public MovedOver OnMovedOver { get; set; } // ^
        public BitmapImage Sprite { get; private set; }
        public abstract bool CanBeMovedOver { get; }

        /// <summary>
        /// Child classes call this ctor. Only initializes properties
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected MapTile(int x, int y /*, BitmapImage image*/)
        {
            X = x;
            Y = y;
            //Sprite = image;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public static MapTile CreateMapTile(int x, int y, string tileType)
        {
            switch (tileType)
            {
                case "Floor1":
                    return new RedFloorTile(x, y /*, Properties.Resources.*/);
                case "Floor2":
                    return new GreyFloorTile(x, y /*, Properties.Resources.*/);
                case string str when IsTableTile(str):
                    return CreateTableTile(x, y, tileType);
                case "SlotMachine":
                    return new SlotMachineTile(x, y /*, Properties.Resources.*/);
                case "Bar":
                    return new BarTile(x, y /*, Properties.Resources.*/);
                default:
                    throw new ArgumentException($"Argument {tileType} is not valid");
            }
        }

        private static bool IsTableTile(string str)
        {
            return new string(str.Take(5).ToArray()) == "Table";
        }

        private static MapTile CreateTableTile(int x, int y, string tileType)
        {
            FirstCharToUpper(ref tileType);
            BitmapImage image = (Properties.Resources.ResourceManager.GetObject(tileType) as Bitmap).ToBitmapImage();
            return new TableTile(x, y /*, image*/);
        }

        private static void FirstCharToUpper(ref string str)
        {
            str = char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
