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
            ImageJoueur.Source = ToBitmapImage(Properties.Resources.panda);
            TableBackground.ImageSource = ToBitmapImage(Properties.Resources.table);
            Hero.Source = ToBitmapImage(Properties.Resources.droite1);
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:
                    Hero.Source = ToBitmapImage(Properties.Resources.bas1);
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) + 10);
                    break;

                case Key.W:
                    Hero.Source = ToBitmapImage(Properties.Resources.haut1);
                    Canvas.SetTop(Hero, Canvas.GetTop(Hero) - 10);
                    break;

                case Key.A:
                    Hero.Source = ToBitmapImage(Properties.Resources.gauche1);
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) - 10);
                    break;

                case Key.D:
                    Hero.Source = ToBitmapImage(Properties.Resources.droite1);
                    Canvas.SetLeft(Hero, Canvas.GetLeft(Hero) + 10);
                    break;
            }
            
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }
}
