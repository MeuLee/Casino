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
        public SlotMachineTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            OnMovedOver += MovedOver;
        }

        private void MovedOver()
        {
            Console.WriteLine("SlotMachine");
        }

        public override bool CanBeMovedOver => throw new NotImplementedException();
    }
}
