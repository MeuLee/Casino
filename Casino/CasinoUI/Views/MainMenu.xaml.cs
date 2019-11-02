using CasinoUI.Models.Poker;
using CasinoUI.Utils;
using CasinoUI.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();
        public MainWindow()
        {
            InitializeComponent();
            SetImages();
        }

        // S/O answers did not work as you can see.
        private void SetImages()
        {
            foreach (var btn in Grid.Children.OfType<Button>())
            {
                if (btn.Content is StackPanel sp)
                {
                    foreach (Image img in sp.Children.OfType<Image>())
                    {
                        img.Source = _casinoChip1;
                    }
                }
            }
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            new SelectGame(new CasinoGame(), 0, 0).Show();
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
