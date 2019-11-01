using System.Drawing;

namespace CasinoUI.Views.Map.Tiles
{
    public class RedFloorTile : MapTile
    {
        public RedFloorTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            MiniMapBrush = System.Windows.Media.Brushes.Red;
            MiniMapPen = new System.Windows.Media.Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
