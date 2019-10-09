using CasinoUI.Utils;
using CasinoUI.View.Map;
using CasinoUI.View.Map.Tiles;
using System.Windows;
using System.Windows.Input;

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour CasinoGame.xaml
    /// </summary>
    public partial class CasinoGame : Window
    {
        public CasinoGame()
        {
            InitializeComponent();
            InitializeImage();
        }

        private void InitializeImage()
        {
            ImgPlayer.Source = Properties.Resources.panda.ToBitmapImage();
            TableBackground.ImageSource = Properties.Resources.table.ToBitmapImage();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            int oldPlayerX = MapRenderer.PlayerX,
                oldPlayerY = MapRenderer.PlayerY;
            switch (e.Key)
            {
                case Key.A:
                    if (MapRenderer.PlayerX > 0)
                    {
                        MapRenderer.PlayerX--;
                    }
                    break;
                case Key.W:
                    if (MapRenderer.PlayerY > 0)
                    {
                        MapRenderer.PlayerY--;
                    }
                    break;
                case Key.S:
                    if (MapRenderer.Map.GetLength(1) - 1 > MapRenderer.PlayerY)
                    {
                        MapRenderer.PlayerY++;
                    }
                    break;
                case Key.D:
                    if (MapRenderer.Map.GetLength(0) - 1 > MapRenderer.PlayerX)
                    {
                        MapRenderer.PlayerX++;
                    }
                    break;
                default:
                    return;
            }

            // could be called in set property
            OnPlayerMoved(oldPlayerX, oldPlayerY);
        }

        public void OnPlayerMoved(int oldPlayerX, int oldPlayerY)
        {
            GameCanvas.InvalidateVisual();
            MiniMapCanvas.InvalidateVisual();
            MapRenderer.Map[MapRenderer.PlayerX, MapRenderer.PlayerY]
                       .OnMovedOver?
                       .Invoke(this, new OnMovedOverEventArgs(oldPlayerX, oldPlayerY));
        }
    }
}
