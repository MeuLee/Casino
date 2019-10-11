using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.View.Map.Tiles
{
    public class TableTile : MapTile
    {
        public TableTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            MiniMapBrush = System.Windows.Media.Brushes.Green;
            MiniMapPen = new System.Windows.Media.Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
