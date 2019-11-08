using System.Windows.Media;

namespace CasinoUI.Views.Map.Tiles
{
    public class BarTile : MapTile
    {
        internal BarTile(int x, int y) : base(x, y, Tiles.GetBitmapImage(Tiles.TileType.BarTile))
        {
            MiniMapBrush = Brushes.DeepSkyBlue;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
