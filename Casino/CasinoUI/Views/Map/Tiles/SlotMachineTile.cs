using System.Drawing;
using System.Windows;

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
            var result = MessageBox.Show("Wanna play slot machine?", "nice title m8", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("epic gamer slot machine simulation");
            }
            else if (result == MessageBoxResult.No)
            {
                MapRenderer.PlayerX = e.OldX;
                MapRenderer.PlayerY = e.OldY;
                (sender as CasinoGame).OnPlayerMoved(e.OldX, e.OldY);
            }
        }
    }
}
