using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CasinoUI.View.Map.Tiles
{
    public class BarTile : MapTile
    {
        public BarTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {

        }

        public override bool CanBeMovedOver => throw new NotImplementedException();
    }
}
