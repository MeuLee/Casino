using System.Drawing;
using System.Windows;

namespace CasinoUI.View.Map.Tiles
{
    public class SlotMachineTile : MapTile 
    {
        public SlotMachineTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            OnMovedOver += MovedOver;
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
                (sender as CasinoGame).GameCanvas.InvalidateVisual();
            }
        }
    }
}
