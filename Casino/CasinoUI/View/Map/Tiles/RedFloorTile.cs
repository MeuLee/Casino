﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CasinoUI.View.Map.Tiles
{
    public class RedFloorTile : MapTile
    {
        public RedFloorTile(int x, int y) : base (x, y)
        {

        }

        public override BitmapImage Sprite => throw new NotImplementedException();
    }
}