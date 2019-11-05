using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views.Map.Tiles
{
    public class TableTile : MapTile
    {
        internal TableTile(int x, int y, BitmapImage image, bool rotate) : base(x, y, image, rotate)
        {
            MiniMapBrush = Brushes.Green;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
