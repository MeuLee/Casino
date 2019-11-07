using CasinoUI.Controllers;
using CasinoUI.Models;
using CasinoUI.Models.Settings;
using CasinoUI.Utils;
using CasinoUI.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            ApplicationSettings.Load();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            new CasinoGameController(this);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            new OptionsMenuController(this);
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
            btn.Cursor = Cursors.Hand;
        }

        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            if (btn.Content is StackPanel sp)
            {
                sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
            }
            btn.Cursor = Cursors.Arrow;
        }
    }
}
