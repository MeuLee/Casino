using CasinoUI.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void InitializeImage()
        {
            ImgPlayer.Source = Properties.Resources.panda.ToBitmapImage();
            TableBackground.ImageSource = Properties.Resources.table.ToBitmapImage();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    if (GameCanvas.PlayerX > 0)
                    {
                        GameCanvas.PlayerX--;
                        GameCanvas.InvalidateVisual();
                    }
                    break;
                case Key.W:
                    if (GameCanvas.PlayerY > 0)
                    {
                        GameCanvas.PlayerY--;
                        GameCanvas.InvalidateVisual();
                    }
                    break;
                case Key.S:
                    if (GameCanvas._map.GetLength(1) - 1 > GameCanvas.PlayerY)
                    {
                        GameCanvas.PlayerY++;
                        GameCanvas.InvalidateVisual();
                    }
                    break;
                case Key.D:
                    if (GameCanvas._map.GetLength(0) - 1 > GameCanvas.PlayerX)
                    {
                        GameCanvas.PlayerX++;
                        GameCanvas.InvalidateVisual();
                    }
                    break;
            }
        }
    }
}
