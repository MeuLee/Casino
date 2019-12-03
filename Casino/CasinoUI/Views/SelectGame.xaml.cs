using CasinoUI.Controllers;
using CasinoUI.Models.Poker;
using CasinoUI.Models.Profiles;
using CasinoUI.Models.Settings;
using CasinoUI.Utils;
using CasinoUI.View;
using System.Linq;
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
        private readonly CasinoGameController _cg;
        private readonly int _oldPlayerX;
        private readonly int _oldPlayerY;

        public SelectGame(CasinoGameController cg, int oldX, int oldY)
        {
            InitializeComponent();
            SetImages();
            _cg = cg;
            _oldPlayerX = oldX;
            _oldPlayerY = oldY;
        }

        private void SetImages()
        {
            foreach (var btn in Grid.Children.OfType<Button>())
            {
                if (btn.Content is StackPanel sp)
                {
                    sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
                }
            }
            ImgBackArrow.Source = _backArrow;
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = _casinoChip2;
            (sp.Children[2] as Image).Source = _casinoChip2;
            btn.Cursor = Cursors.Hand;
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            StackPanel sp = btn.Content as StackPanel;
            (sp.Children[0] as Image).Source = _casinoChip1;
            (sp.Children[2] as Image).Source = _casinoChip1;
            btn.Cursor = Cursors.Arrow;
        }

        private void BtnMap_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 2;
            btn.Cursor = Cursors.Hand;
        }

        private void BtnMap_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 2;
            btn.Cursor = Cursors.Arrow;
        }

        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            ApplicationSettings.HumanPlayer.X = _oldPlayerX;
            ApplicationSettings.HumanPlayer.Y = _oldPlayerY;
            _cg.OnPlayerMoved(_oldPlayerX, _oldPlayerY);
            Close();
            _cg.View.Show();
        }

        private void BtnPoker_Click(object sender, RoutedEventArgs e)
        {
            new PokerController();
            Close();
        }

        private void BtnBlackJack_Click(object sender, RoutedEventArgs e)
        {
            ApplicationSettings.HumanPlayer.CurrentProfile = new BlackjackProfile();
            new BlackjackController();
        }
    }
}
