using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views.Map.Tiles
{
    public class BlackFloorTile : MapTile
    {
        internal BlackFloorTile(int x, int y) : base(x, y, Tiles.GetBitmapImage(Tiles.TileType.BlackFloor))
        {
            MiniMapBrush = Brushes.Black;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
