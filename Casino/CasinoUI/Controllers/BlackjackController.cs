using CasinoUI.Models.Settings;
using CasinoUI.Models.Blackjack;
using CasinoUI.Utils;
using CasinoUI.Views;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Collections.Generic;
using CasinoUI.Models.PlayerModel;

namespace CasinoUI.Controllers
{
    public class BlackjackController
    {
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();
        private static BitmapImage _blackjackTable = Properties.Resources.blackjackTable.ToBitmapImage();
        private static BitmapImage _greenTable = Properties.Resources.table.ToBitmapImage();

        private readonly BlackjackLogic _model;
        private readonly GameBlackjack _view;

        public BlackjackController()
        {
            _model = new BlackjackLogic(ApplicationSettings.HumanPlayer, this);
            _view = new GameBlackjack();
            _view.Show();

            AddEventsOnUIControls();
            SetImages();
        }

        private void AddEventsOnUIControls()
        {
            foreach (var btn in _view.Grid.Children.OfType<Button>())
            {
                btn.MouseEnter += Btn_MouseEnter;
                btn.MouseLeave += Btn_MouseLeave;
            }
            _view.BtnHit.Click += Hit_Click;
            _view.BtnInsurance.Click += Insurance_Click;
            _view.BtnDoubleDown.Click += DoubleDown_Click;
            _view.BtnStand.Click += Stand_Click;
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

        private void Hit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _model.Hit();
            _view.CreateNewImageSpace(_model._players.First(p => p is HumanPlayer), _model._players.First(p => p is BlackjackAI));
            _model.AIPlays();
        }

        private void Insurance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _model.Insurance();
        }

        private void DoubleDown_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _model.DoubleDown();
            _model.AIPlays();
        }

        private void Stand_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _model.Stand();
            _model.AIPlays();
        }

        private void SetImages()
        {
            _view.ImgBackground.ImageSource = _greenTable;
            _view.ImgBlackJackTable.Source = _blackjackTable;
            foreach (var btn in _view.Grid.Children.OfType<Button>())
            {
                if (btn.Content is StackPanel sp)
                {
                    sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
                }
            }
        }

        public void UpdateViewNewCardAI()
        {
            _view.CreateNewImageSpace(_model.GetHuman(), _model.GetAI());
        }
    }
}
