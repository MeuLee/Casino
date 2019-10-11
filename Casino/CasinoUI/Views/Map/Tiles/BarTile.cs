using System.Drawing;

namespace CasinoUI.View.Map.Tiles
{
    public class BarTile : MapTile
    {
        public BarTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            MiniMapBrush = System.Windows.Media.Brushes.DeepSkyBlue;
            MiniMapPen = new System.Windows.Media.Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
