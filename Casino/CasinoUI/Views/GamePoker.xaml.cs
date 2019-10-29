using CasinoUI.Utils;
using System;
using System.Windows;
using System.Windows.Media;

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour Poker.xaml
    /// </summary>
    public partial class GamePoker : Window
    {
        public GamePoker()
        {
            InitializeComponent();
            InitilizeCarte();
            InitilizeForm();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Utiliser, quand 2 cartes, carte 1 et 5 pour le joueur,
        /// carte 2 et 3 pour AI sur les bords gauche et droit et carte 3 et 4 pour ceux de face.
        /// </summary>
        private void InitilizeCarte()
        {
            CarteRetourne.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            CarteJoueur1.Source = Properties.Resources._13S.ToBitmapImage();
            CarteJoueur5.Source = Properties.Resources._13H.ToBitmapImage();

            CartePack.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            CarteRetourne_AI12.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI13.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            CarteRetourne_AI23.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI24.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            CarteRetourne_AI33.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI34.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            CarteRetourne_AI42.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI43.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
        }

        /// <summary>
        ///
        /// </summary>
        private void InitilizeForm()
        {
            TableBlueBackground.ImageSource = Properties.Resources.TableNeuve.ToBitmapImage();

            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = Properties.Resources.table.ToBitmapImage();
            RightRectangle.Fill = imgBrush;
            LeftRectangle.Fill = imgBrush;

            ImageBrush dealer = new ImageBrush();
            dealer.ImageSource = Properties.Resources.DEALER.ToBitmapImage();
            EllipseDealer.Fill = dealer;
        }
    }
}
