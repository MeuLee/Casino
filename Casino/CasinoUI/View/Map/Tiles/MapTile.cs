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
        public bool Rotate { get; private set; }
        public abstract bool CanBeMovedOver { get; }

        /// <summary>
        /// Child classes call this ctor. Only initializes properties
        /// </summary>
        /// <param name="x">X coordinate on the map</param>
        /// <param name="y">Y coordinate on the map</param>
        /// <param name="image">Bitmap to be printed on screen representing this tile</param>
        protected MapTile(int x, int y , Bitmap image, bool rotate) // change rotate type to int (angle) ? or create child class to bitmap + rotate property?
        {
            X = x;
            Y = y;
            Sprite = image.ToBitmapImage();
            if (rotate)
            {
                //Sprite.Rotation = Rotation.Rotate90;
            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public static MapTile CreateMapTile(int x, int y, string tileType, bool rotate)
        {
            switch (tileType)
            {
                case "Floor1":
                    return new RedFloorTile(x, y , Properties.Resources.redfloor, rotate);
                case "Floor2":
                    return new GreyFloorTile(x, y, Properties.Resources.blackfloor, rotate);
                case string str when IsTableTile(str):
                    return CreateTableTile(x, y, tileType, rotate);
                case "SlotMachine":
                    return new SlotMachineTile(x, y , Properties.Resources.slotmachinetemp, rotate);
                case "Bar":
                    return new BarTile(x, y , Properties.Resources.bartemp, rotate);
                default:
                    throw new ArgumentException($"Argument {tileType} is not valid");
            }
        }

        private static bool IsTableTile(string str)
        {
            return new string(str.Take(5).ToArray()) == "Table";
        }

        private static MapTile CreateTableTile(int x, int y, string tileType, bool rotate)
        {
            FirstCharToUpper(ref tileType);
            Bitmap image = Properties.Resources.ResourceManager.GetObject(tileType) as Bitmap;
            return new TableTile(x, y , image, rotate);
        }

        private static void FirstCharToUpper(ref string str)
        {
            str = char.ToLower(str[0]) + str.Substring(1);
        }
    }
}
