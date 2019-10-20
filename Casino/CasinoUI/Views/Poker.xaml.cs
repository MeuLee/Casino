using CasinoUI.Utils;
using System.Windows;

namespace CasinoUI.Views
{
    /// <summary>
    /// Logique d'interaction pour Poker.xaml
    /// </summary>
    public partial class Poker : Window
    {
        public Poker()
        {
            InitializeComponent();
            TableBlueBackground.ImageSource = Properties.Resources.TableNeuve.ToBitmapImage();
            CarteJoueurGauche.Source = Properties.Resources._13S.ToBitmapImage();
            CarteJoueurDroite.Source = Properties.Resources._13H.ToBitmapImage();
            CarteRetourne.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
