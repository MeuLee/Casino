using CasinoUI.Utils;
using CasinoUI.View;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly BitmapImage _casinoChip = Properties.Resources.casinoChip.ToBitmapImage();
        public MainWindow()
        {
            InitializeComponent();
            SetImages();
        }

        // S/O answers did not work.
        private void SetImages()
        {
            ImgChip1.Source = _casinoChip;
            ImgChip2.Source = _casinoChip;
            ImgChip3.Source = _casinoChip;
            ImgChip4.Source = _casinoChip;
            ImgChip5.Source = _casinoChip;
            ImgChip6.Source = _casinoChip;
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            new CasinoGame().Show();
            Close();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            new OptionMenu().Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
