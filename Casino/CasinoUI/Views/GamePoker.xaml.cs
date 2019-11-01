using CasinoUI.Utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.View
{
    /// <summary>
    /// Logique d'interaction pour Poker.xaml
    /// </summary>
    public partial class GamePoker : Window
    {
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();
        public GamePoker()
        {
            InitializeComponent();
            InitilizeCarte();
            InitilizeForm();
            SetImage();
        }

        private void SetImage()
        {
            ImgChip1.Source = _casinoChip1;
            ImgChip2.Source = _casinoChip1;
            ImgChip3.Source = _casinoChip1;
            ImgChip4.Source = _casinoChip1;
            ImgChip5.Source = _casinoChip1;
            ImgChip6.Source = _casinoChip1;
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

        private void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 4;
            if (btn.Content is StackPanel sp)
            {
                sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip2);
            }
        }

        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            if (btn.Content is StackPanel sp)
            {
                sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
            }
        }
    }
}
