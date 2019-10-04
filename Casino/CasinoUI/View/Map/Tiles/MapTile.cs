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
        public abstract BitmapImage Sprite { get; }

        public abstract bool CanBeMovedOver { get; }

        /// <summary>
        /// Child classes call this ctor. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected MapTile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public static MapTile CreateMapTile(int x, int y, string tileType)
        {
            switch (tileType)
            {
                case "Floor1":
                    return new RedFloorTile(x, y);
                case "Floor2":
                    return new GreyFloorTile(x, y);
                case "Table1":
                    return new BrownTableTile(x, y);
                case "Table2":
                    return new GreenTableTile(x, y);
                case "SlotMachine":
                    return new SlotMachineTile(x, y);
                case "Bar":
                    return new BarTile(x, y);
                default:
                    throw new ArgumentException($"Argument {tileType} is not valid");
            }
        }
    }
}
