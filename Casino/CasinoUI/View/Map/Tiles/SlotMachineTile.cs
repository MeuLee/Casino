using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CasinoUI.View.Map.Tiles
{
    public class SlotMachineTile : MapTile 
    {
        public SlotMachineTile(int x, int y, Bitmap image) : base(x, y, image)
        {

        }

        public override bool CanBeMovedOver => throw new NotImplementedException();
    }
}
