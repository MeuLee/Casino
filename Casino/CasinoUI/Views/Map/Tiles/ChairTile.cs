using CasinoUI.Controllers;
using System.Drawing;

namespace CasinoUI.Views.Map.Tiles
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
            CasinoGameController cg = sender as CasinoGameController;
            cg.View.Hide();
            new SelectGame(cg, e.OldX, e.OldY).Show();
        }
    }
}
