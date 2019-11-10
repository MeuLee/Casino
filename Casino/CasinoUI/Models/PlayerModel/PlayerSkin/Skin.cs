using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace CasinoUI.Models.PlayerModel.PlayerSkin
{
    public class Skin
    {
        public string Name { get; private set; }
        private List<BitmapImage> _downImages;
        private List<BitmapImage> _leftImages;
        private List<BitmapImage> _upImages;
        private List<BitmapImage> _rightImages;
        private int _downIndex = 0;
        private int _leftIndex = 0;
        private int _upIndex = 0;
        private int _rightIndex = 0;

        public BitmapImage BaseDownImage
        {
            get { return _downImages[0]; }
        }

        public BitmapImage NextDownImage
        {
            get
            {
                BitmapImage img = _downImages[_downIndex];
                SetIndex(ref _downIndex, _downImages.Count);
                return img;
            }
        }

        public BitmapImage NextLeftImage
        {
            get
            {
                BitmapImage img = _leftImages[_leftIndex];
                SetIndex(ref _leftIndex, _leftImages.Count);
                return img;
            }
        }

        public BitmapImage NextUpImage
        {
            get
            {
                BitmapImage img = _upImages[_upIndex];
                SetIndex(ref _upIndex, _upImages.Count);
                return img;
            }
        }

        public BitmapImage NextRightImage
        {
            get
            {
                BitmapImage img = _rightImages[_rightIndex];
                SetIndex(ref _rightIndex, _rightImages.Count);
                return img;
            }
        }

        private void SetIndex(ref int index, int count)
        {
            index++;
            if (index >= count)
            {
                index = 0;
            }
        }

        public Skin(BitmapImage[] bmps, string name)
        {
            AddImages(bmps);
            Name = name;
        }

        private void AddImages(BitmapImage[] bmps)
        {
            _downImages = new List<BitmapImage>()
                         { bmps[0], bmps[3] };

            _leftImages = new List<BitmapImage>()
                         { bmps[2], bmps[5],  bmps[6] };

            _upImages = new List<BitmapImage>()
                       { bmps[1], bmps[4] };

            _rightImages = new List<BitmapImage>()
                          { bmps[7],  bmps[8], bmps[9] };
        }
    }
}
