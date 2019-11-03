using CasinoUI.Models;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Utils;
using CasinoUI.Views.Map;
using CasinoUI.Views.Map.Tiles;
using System.Windows;
using System.Windows.Input;

namespace CasinoUI.Views
{
    /// <summary>
    /// Logique d'interaction pour CasinoGame.xaml
    /// </summary>
    public partial class CasinoGame : Window
    {
        public CasinoGame()
        {
            InitializeComponent();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            HumanPlayer player = ApplicationSettings.HumanPlayer;
            var map = ApplicationSettings.Map;
            int oldPlayerX = player.X,
                oldPlayerY = player.Y;
            switch (e.Key)
            {
                case Key.A:
                    if (player.X > 0)
                    {
                        player.X--;
                    }
                    break;
                case Key.W:
                    if (player.Y > 0)
                    {
                        player.Y--;
                    }
                    break;
                case Key.S:
                    if (map.GetLength(1) - 1 > player.Y)
                    {
                        player.Y++;
                    }
                    break;
                case Key.D:
                    if (map.GetLength(0) - 1 > player.X)
                    {
                        player.X++;
                    }
                    break;
                default:
                    return;
            }

            OnPlayerMoved(oldPlayerX, oldPlayerY);
        }

        public void OnPlayerMoved(int oldPlayerX, int oldPlayerY)
        {
            var player = ApplicationSettings.HumanPlayer;
            GameCanvas.InvalidateVisual();
            MiniMapCanvas.InvalidateVisual();
            ApplicationSettings.Map[player.X, player.Y]
                               .OnMovedOver?
                               .Invoke(this, new OnMovedOverEventArgs(oldPlayerX, oldPlayerY));
        }
    }
}
