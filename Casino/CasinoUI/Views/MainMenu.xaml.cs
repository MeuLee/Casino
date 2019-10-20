using CasinoUI.Utils;
using CasinoUI.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly BitmapImage _casinoChip = Properties.Resources.redChip.ToBitmapImage();
        public MainWindow()
        {
            InitializeComponent();
            SetImages();
        }

        // S/O answers did not work as you can see.
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

        private void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = Properties.Resources.blueChip.ToBitmapImage();
            (sp.Children[2] as Image).Source = Properties.Resources.blueChip.ToBitmapImage();
        }

        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = Properties.Resources.redChip.ToBitmapImage();
            (sp.Children[2] as Image).Source = Properties.Resources.redChip.ToBitmapImage();
        }
    }
}
