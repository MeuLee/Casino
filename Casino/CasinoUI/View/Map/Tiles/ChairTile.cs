using System.Drawing;
using System.Windows;

namespace CasinoUI.View.Map.Tiles
{
    public class ChairTile : MapTile
    {
        public ChairTile(int x, int y, Bitmap image, bool rotate) : base(x, y, image, rotate)
        {
            OnMovedOver += MovedOver;
            MiniMapBrush = System.Windows.Media.Brushes.Brown;
            MiniMapPen = new System.Windows.Media.Pen(MiniMapBrush, PEN_WIDTH);
        }

        private void MovedOver(object sender, OnMovedOverEventArgs e)
        {
            var result = MessageBox.Show("Wanna play poker?", "nice title m8", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("epic gamer poker simulation");
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
