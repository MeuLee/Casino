using System.Drawing;

namespace CasinoUI.Views.Map.Tiles
{
    public class SlotMachineTile : MapTile 
    {
        public SlotMachineTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            OnMovedOver += MovedOver;
            MiniMapBrush = System.Windows.Media.Brushes.Yellow;
            MiniMapPen = new System.Windows.Media.Pen(MiniMapBrush, PEN_WIDTH);
        }

        private void MovedOver(object sender, OnMovedOverEventArgs e)
        {
            
        }
    }
}
