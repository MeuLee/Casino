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
            ImageJoueur.Source = Properties.Resources.panda.ToBitmapImage();
            TableBackground.ImageSource = Properties.Resources.table.ToBitmapImage();
            Hero.Source = Properties.Resources.droite1.ToBitmapImage();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:
                    Hero.Source = Properties.Resources.bas1.ToBitmapImage();
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + 10);
                    break;

                case Key.W:
                    Hero.Source = Properties.Resources.haut1.ToBitmapImage();
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) - 10);
                    break;

                case Key.A:
                    Hero.Source = Properties.Resources.gauche1.ToBitmapImage();
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - 10);
                    break;

                case Key.D:
                    Hero.Source = Properties.Resources.droite1.ToBitmapImage();
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + 10);
                    break;
            }
            
        }

        
    }
}
