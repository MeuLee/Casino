using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:
                    Hero.Source = GetImage("SpriteJoueur/bas1.gif");
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + 10);
                    break;

                case Key.W:
                    Hero.Source = GetImage("SpriteJoueur/haut1.gif");
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) - 10);
                    break;

                case Key.A:
                    Hero.Source = GetImage("SpriteJoueur/gauche1.gif");
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - 10);
                    break;

                case Key.D:
                    Hero.Source = GetImage("SpriteJoueur/droite1.gif");
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + 10);
                    break;
            }
            
        }

        private static BitmapImage GetImage(string imageUri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/" + imageUri, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
