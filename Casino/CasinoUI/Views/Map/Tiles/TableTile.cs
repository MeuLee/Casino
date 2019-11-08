using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views.Map.Tiles
{
    public class TableTile : MapTile
    {
        internal TableTile(int x, int y, BitmapImage image) : base(x, y, image)
        {
            MiniMapBrush = Brushes.Green;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
