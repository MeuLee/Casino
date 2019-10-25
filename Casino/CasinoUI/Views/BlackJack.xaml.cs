using CasinoUI.Utils;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views
{
    /// <summary>
    /// Interaction logic for BlackJack.xaml
    /// </summary>
    public partial class BlackJack : Window
    {
        private static BitmapImage _blackjackTable = Properties.Resources.blackjackTable.ToBitmapImage();
        private static BitmapImage _greenTable = Properties.Resources.table.ToBitmapImage();
        public BlackJack()
        {
            InitializeComponent();
            SetImages();
        }

        private void SetImages()
        {
            ImgBackground.ImageSource = _greenTable;
            ImgBlackJackTable.Source = _blackjackTable;
        }
    }
}
