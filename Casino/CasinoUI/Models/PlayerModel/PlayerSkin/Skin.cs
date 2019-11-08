using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace CasinoUI.Models.PlayerModel.PlayerSkin
{
    public class Skin
    {
        public string Name { get; private set; }
        public List<BitmapImage> DownImages { get; private set; }
        public List<BitmapImage> LeftImages { get; private set; }
        public List<BitmapImage> UpImages { get; private set; }
        public List<BitmapImage> RightImages { get; private set; }

        public Skin(BitmapImage[] bmps, string name)
        {
            AddImages(bmps);
            Name = name;
        }

        private void AddImages(BitmapImage[] bmps)
        {
            DownImages = new List<BitmapImage>()
                         { bmps[0], bmps[3] };

            LeftImages = new List<BitmapImage>()
                         { bmps[2], bmps[5],  bmps[6] };

            UpImages = new List<BitmapImage>()
                       { bmps[1], bmps[4] };

            RightImages = new List<BitmapImage>()
                          { bmps[7],  bmps[8], bmps[9] };
        }
    }
}
