using System.Drawing;

namespace CasinoUI.View.Map.Tiles
{
    public class BlackFloorTile : MapTile
    {
        public BlackFloorTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            MiniMapBrush = System.Windows.Media.Brushes.Black;
            MiniMapPen = new System.Windows.Media.Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
