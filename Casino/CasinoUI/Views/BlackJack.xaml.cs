using CasinoUI.Utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();

        public BlackJack()
        {
            InitializeComponent();
            SetImages();
            SetCardImagesTemp();
        }

        private void SetCardImagesTemp()
        {
            Img1Player.Source = Properties.Resources._10C.ToBitmapImage();
            Img2Player.Source = Properties.Resources._9D.ToBitmapImage();
            Img1Dealer.Source = Properties.Resources._6S.ToBitmapImage();
            Img2Dealer.Source = Properties.Resources._11S.ToBitmapImage();
        }

        private void SetImages()
        {
            ImgBackground.ImageSource = _greenTable;
            ImgBlackJackTable.Source = _blackjackTable;
            foreach (var btn in Grid.Children.OfType<Button>())
            {
                if (btn.Content is StackPanel sp)
                {
                    sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
                }
            }
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
