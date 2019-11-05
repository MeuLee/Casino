using CasinoUI.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views.Map.Tiles
{
    internal static class Tiles
    {
        public enum TileType
        {
            RedFloor,
            BlackFloor,
            SlotMachine,
            BarTile
        }

        private static readonly BitmapImage _redFloorImage = Properties.Resources.redfloor.ToBitmapImage();
        private static readonly BitmapImage _blackFloorImage = Properties.Resources.blackfloor.ToBitmapImage();
        private static readonly BitmapImage _slotMachineImage = Properties.Resources.slotmachinetemp.ToBitmapImage();
        private static readonly BitmapImage _barTileImage = Properties.Resources.bartemp.ToBitmapImage();
        private static readonly BitmapImage[] _table;
        private static readonly BitmapImage[] _rotatedTable;
        
        static Tiles()
        {
            _table = new BitmapImage[12];
            _rotatedTable = new BitmapImage[12];
            FillTableArray();
            FillRotateTableArray();
        }

        private static void FillTableArray()
        {
            for (int i = 1; i <= 12; i++)
            {
                _table[i - 1] = (Properties.Resources.ResourceManager.GetObject($"table{i}") as Bitmap).ToBitmapImage();
            }
        }

        private static void FillRotateTableArray()
        {
            for (int i = 1; i <= 12; i++)
            {
                _rotatedTable[i - 1] = (Properties.Resources.ResourceManager.GetObject($"table{i}") as Bitmap).Rotate90().ToBitmapImage();
            }
        }

        public static BitmapImage GetBitmapImage(TileType type)
        {
            switch (type)
            {
                case TileType.RedFloor:
                    return _redFloorImage;
                case TileType.BlackFloor:
                    return _blackFloorImage;
                case TileType.SlotMachine:
                    return _slotMachineImage;
                case TileType.BarTile:
                    return _barTileImage;
            }
            throw new ArgumentException("Could not find the specified BitmapImage.");
        }

        public static BitmapImage GetTableBitmapImage(int index)
        {
            return _table[index];
        }

        public static BitmapImage GetRotatedTableBitmapImage(int index)
        {
            return _rotatedTable[index];
        }
    }
}
