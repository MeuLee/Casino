using CasinoUI.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CasinoUI.Models.PlayerModel.PlayerSkin
{
    class SkinsGenerator
    {
        private const int IMAGE_WIDTH = 32,
                          IMAGE_HEIGHT = 32;
        public static int SKIN_COUNT = 9;
        private static Bitmap _personnages = Properties.Resources.personnages;

        internal static Dictionary<Skins, Skin> Skins { get; } = new Dictionary<Skins, Skin>();

        static SkinsGenerator()
        {
            GenerateSkins();
        }

        private static void GenerateSkins()
        {
            var list = Enum.GetValues(typeof(Skins)).Cast<Skins>().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Skins skinType = list[i];
                BitmapImage[] bmps = new BitmapImage[10];
                for (int j = 0; j < bmps.Length; j++)
                {
                    bmps[j] = LoadBitmapImage(j, i);
                }
                Skins.Add(skinType, new Skin(bmps, skinType.ToString()));
            }
        }

        private static BitmapImage LoadBitmapImage(int x, int y)
        {
            Rectangle crop = new Rectangle(x * IMAGE_WIDTH, y * IMAGE_HEIGHT, IMAGE_WIDTH, IMAGE_HEIGHT);

            var bmp = new Bitmap(crop.Width, crop.Height);

            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(_personnages, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }

            return bmp.ToBitmapImage();
        }


    }
}
