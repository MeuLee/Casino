using System.Windows.Media;

namespace CasinoUI.Views.Map.Tiles
{
    public class SlotMachineTile : MapTile 
    {
        internal SlotMachineTile(int x, int y) : base(x, y, Tiles.GetBitmapImage(Tiles.TileType.SlotMachine))
        {
            OnMovedOver += MovedOver;
            MiniMapBrush = Brushes.Yellow;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }

        private void MovedOver(object sender, OnMovedOverEventArgs e)
        {
            
        }
    }
}
