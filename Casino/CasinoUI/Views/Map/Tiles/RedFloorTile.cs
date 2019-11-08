using System.Windows.Media;

namespace CasinoUI.Views.Map.Tiles
{
    public class RedFloorTile : MapTile
    {
        internal RedFloorTile(int x, int y) : base(x, y, Tiles.GetBitmapImage(Tiles.TileType.RedFloor))
        {
            MiniMapBrush = Brushes.Red;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }
    }
}
