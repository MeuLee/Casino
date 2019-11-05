using CasinoUI.Controllers;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CasinoUI.Views.Map.Tiles
{
    public class ChairTile : MapTile
    {
        internal ChairTile(int x, int y, BitmapImage image) : base(x, y, image)
        {
            OnMovedOver += MovedOver;
            MiniMapBrush = System.Windows.Media.Brushes.Brown;
            MiniMapPen = new Pen(MiniMapBrush, PEN_WIDTH);
        }

        private void MovedOver(object sender, OnMovedOverEventArgs e)
        {
            CasinoGameController cg = sender as CasinoGameController;
            cg.View.Hide();
            new SelectGame(cg, e.OldX, e.OldY).Show();
        }
    }
}
