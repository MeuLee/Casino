using CasinoUI.Utils;
using System.Windows;

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour Poker.xaml
    /// </summary>
    public partial class Poker : Window
    {
        public Poker()
        {
            InitializeComponent();
            TableBlueBackground.ImageSource = ToBitmapImage(Properties.Resources.TableNeuve);
            CarteJoueurGauche.Source = ToBitmapImage(Properties.Resources._13S);
            CarteJoueurDroite.Source = ToBitmapImage(Properties.Resources._13H);
            CarteRetourne.Source = ToBitmapImage(Properties.Resources.Carte_Dos);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Méthode pour convertir en bitmap les images
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
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
