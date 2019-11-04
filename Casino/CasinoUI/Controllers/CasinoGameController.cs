using CasinoUI.Models;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.WindowModels;
using CasinoUI.Views;
using CasinoUI.Views.Map.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CasinoUI.Controllers
{
    public class CasinoGameController
    {
        private Window _parent;
        public CasinoGame View { get; private set; }
        public CasinoGameModel Model { get; private set; }

        private static Thickness _smallThick = new Thickness(3);
        private static Thickness _bigThick = new Thickness(1);
        public CasinoGameController(Window parent)
        {
            _parent = parent;
            Model = new CasinoGameModel();
            View = new CasinoGame();
            View.Show();
            _parent.Hide();
            ApplicationSettings.SoundPlayer.Start();
            AddEvents();
        }

        private void AddEvents()
        {
            View.BtnBack.MouseEnter += BtnBack_MouseEnter;
            View.BtnBack.MouseLeave += BtnBack_MouseLeave;
            View.BtnBack.Click += BtnBack_Click;
            View.Closed += View_Closed;
            View.KeyDown += Grid_KeyDown;
        }

        private void View_Closed(object sender, EventArgs e)
        {
            ApplicationSettings.SoundPlayer.Stop();
        }

        private void BtnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            View.ImgBack.Margin = _bigThick;
        }

        private void BtnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            View.ImgBack.Margin = _smallThick;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            View.Close();
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
            View.GameCanvas.InvalidateVisual();
            View.MiniMapCanvas.InvalidateVisual();
            ApplicationSettings.Map[player.X, player.Y]
                               .OnMovedOver?
                               .Invoke(this, new OnMovedOverEventArgs(oldPlayerX, oldPlayerY));
        }
    }
}
