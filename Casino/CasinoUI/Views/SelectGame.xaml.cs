using CasinoUI.Utils;
using CasinoUI.View;
using CasinoUI.Views.Map;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views
{
    /// <summary>
    /// Interaction logic for SelectGame.xaml
    /// </summary>
    public partial class SelectGame : Window
    {
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();
        private static readonly BitmapImage _backArrow = Properties.Resources.backArrow.ToBitmapImage();
        private readonly CasinoGame _cg;
        private readonly int _oldPlayerX;
        private readonly int _oldPlayerY;

        public SelectGame(CasinoGame cg, int oldPlayerX, int oldPlayerY)
        {
            InitializeComponent();
            SetImages();
            _cg = cg;
            _oldPlayerX = oldPlayerX;
            _oldPlayerY = oldPlayerY;
        }

        private void SetImages()
        {
            ImgChip1.Source = _casinoChip1;
            ImgChip2.Source = _casinoChip1;
            ImgChip3.Source = _casinoChip1;
            ImgChip4.Source = _casinoChip1;
            ImgChip5.Source = _casinoChip1;
            ImgChip6.Source = _casinoChip1;
            ImgChip7.Source = _casinoChip1;
            ImgChip8.Source = _casinoChip1;
            ImgBackArrow.Source = _backArrow;
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = _casinoChip2;
            (sp.Children[2] as Image).Source = _casinoChip2;
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = _casinoChip1;
            (sp.Children[2] as Image).Source = _casinoChip1;
        }

        private void BtnMap_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 2;
        }

        private void BtnMap_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 2;
        }

        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            MapRenderer.PlayerX = _oldPlayerX;
            MapRenderer.PlayerY = _oldPlayerY;
            _cg.OnPlayerMoved(_oldPlayerX, _oldPlayerY);
            Close();
            _cg.Show();
        }

        private void BtnPoker_Click(object sender, RoutedEventArgs e)
        {
            new GamePoker().Show();
            Close();
        }

        private void BtnBlackJack_Click(object sender, RoutedEventArgs e)
        {
            new BlackJack().Show();
            Close();
        }
    }
}
