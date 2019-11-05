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

        private static readonly BitmapImage RedFloorImage = Properties.Resources.redfloor.ToBitmapImage();
        private static readonly BitmapImage BlackFloorImage = Properties.Resources.blackfloor.ToBitmapImage();
        private static readonly BitmapImage SlotMachineImage = Properties.Resources.slotmachinetemp.ToBitmapImage();
        private static readonly BitmapImage BarTileImage = Properties.Resources.bartemp.ToBitmapImage();
        private static readonly BitmapImage[] Table;
        private static readonly BitmapImage[] RotatedTable;
        
        static Tiles()
        {
            Table = new BitmapImage[12];
            RotatedTable = new BitmapImage[12];
            FillTableArray();
            FillRotateTableArray();
        }

        private static void FillTableArray()
        {
            for (int i = 1; i <= 12; i++)
            {
                Table[i - 1] = (Properties.Resources.ResourceManager.GetObject($"table{i}") as Bitmap).ToBitmapImage();
            }
        }

        private static void FillRotateTableArray()
        {
            for (int i = 1; i <= 12; i++)
            {
                RotatedTable[i - 1] = (Properties.Resources.ResourceManager.GetObject($"table{i}") as Bitmap).Rotate90().ToBitmapImage();
            }
        }

        public static BitmapImage GetBitmapImage(TileType type)
        {
            switch (type)
            {
                case TileType.RedFloor:
                    return RedFloorImage;
                case TileType.BlackFloor:
                    return BlackFloorImage;
                case TileType.SlotMachine:
                    return SlotMachineImage;
                case TileType.BarTile:
                    return BarTileImage;
            }
            throw new ArgumentException("Could not find the specified BitmapImage.");
        }

        public static BitmapImage GetTableBitmapImage(int index)
        {
            return Table[index];
        }

        public static BitmapImage GetRotatedTableBitmapImage(int index)
        {
            return RotatedTable[index];
        }
    }
}
