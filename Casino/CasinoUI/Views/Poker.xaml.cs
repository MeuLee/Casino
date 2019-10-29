using CasinoUI.Utils;
using System;
using System.Windows;
using System.Windows.Media;

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
            //CarteJoueur2.Source = Properties.Resources._13H.ToBitmapImage();
            //CarteJoueur3.Source = Properties.Resources._13H.ToBitmapImage();
            //CarteJoueur4.Source = Properties.Resources._13H.ToBitmapImage();
            CarteJoueur5.Source = Properties.Resources._13H.ToBitmapImage();

            CartePack.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            //CarteRetourne_AI11.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI12.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI13.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI14.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI15.Source = Properties.Resources.Carte_Dos.ToBitmapImage();

            //CarteRetourne_AI21.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI22.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI23.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI24.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI25.Source = Properties.Resources.Carte_Dos.ToBitmapImage();


            //CarteRetourne_AI31.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI32.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI33.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI34.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI35.Source = Properties.Resources.Carte_Dos.ToBitmapImage();


            //CarteRetourne_AI41.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI42.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            CarteRetourne_AI43.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI44.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteRetourne_AI45.Source = Properties.Resources.Carte_Dos.ToBitmapImage();


            //CarteJeu1.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteJeu2.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteJeu3.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteJeu4.Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            //CarteJeu5.Source = Properties.Resources.Carte_Dos.ToBitmapImage();


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
