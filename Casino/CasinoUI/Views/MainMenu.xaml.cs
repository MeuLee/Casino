using CasinoUI.Utils;
using CasinoUI.Views;
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
            ImgChip1.Source = _casinoChip1;
            ImgChip2.Source = _casinoChip1;
            ImgChip3.Source = _casinoChip1;
            ImgChip4.Source = _casinoChip1;
            ImgChip5.Source = _casinoChip1;
            ImgChip6.Source = _casinoChip1;
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

        private void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = _casinoChip2;
            (sp.Children[2] as Image).Source = _casinoChip2;
        }

        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = _casinoChip1;
            (sp.Children[2] as Image).Source = _casinoChip1;
        }
    }
}
