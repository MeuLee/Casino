using CasinoUI.Models.Settings;
using CasinoUI.Models.Blackjack;
using CasinoUI.Utils;
using CasinoUI.Views;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using CasinoUI.Models.PlayerModel;
using System.Windows;
using System;
using System.Windows.Media;
using CasinoUI.Models.Cards;

namespace CasinoUI.Controllers
{
    public class BlackjackController
    {
        private readonly int _minimumBet = 10 * ApplicationSettings.HumanPlayer.StressLevel;
        private int _currentBet;
        private int CurrentBet
        {
            get { return _currentBet; }
            set
            {
                _currentBet = value;
                _view.TxbBet.Text = _currentBet.ToString();
            }
        }

        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();
        private readonly HumanPlayer _humanPlayer;
        private readonly BlackjackLogic _model;
        private readonly GameBlackjack _view;
        private static Brush _mouseOverBrush = Brushes.White;
        private static Brush _mouseLeaveBrush = Brushes.RosyBrown;

        public BlackjackController()
        {
            _humanPlayer = ApplicationSettings.HumanPlayer;
            _model = new BlackjackLogic(_humanPlayer, this);
            _view = new GameBlackjack();
            _view.Show();

            AddEventsOnUIControls();
            SetImages();
            CurrentBet = _minimumBet;

            _view.BtnHit.Visibility = Visibility.Hidden;
            _view.BtnInsurance.Visibility = Visibility.Hidden;
            _view.BtnStand.Visibility = Visibility.Hidden;
            _view.BtnDoubleDown.Visibility = Visibility.Hidden;
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
            _view.BtnReduceBet.Click += BtnReduceBet_Click;
            _view.BtnIncreaseBet.Click += BtnIncreaseBet_Click;
            _view.BtnReduceBet.MouseEnter += BtnBet_MouseOver;
            _view.BtnIncreaseBet.MouseEnter += BtnBet_MouseOver;
            _view.BtnReduceBet.MouseLeave += BtnBet_MouseLeave;
            _view.BtnIncreaseBet.MouseLeave += BtnBet_MouseLeave;
            _view.BtnBet.Click += BtnBet_Click;
        }

        private void BtnBet_Click(object sender, RoutedEventArgs e)
        {
            _view.BtnHit.Visibility = Visibility.Visible;
            _view.BtnInsurance.Visibility = Visibility.Visible;
            _view.BtnStand.Visibility = Visibility.Visible;
            _view.BtnDoubleDown.Visibility = Visibility.Visible;
            _view.BtnIncreaseBet.Visibility = Visibility.Hidden;
            _view.BtnReduceBet.Visibility = Visibility.Hidden;
            _view.BtnBet.Visibility = Visibility.Hidden;

            _model.CardStack = new GameCardStack();
            _model.DistributeCards();
            _view.CreateNewImageSpace(_model._players.First(p => p is HumanPlayer), _model._players.First(p => p is BlackjackAI));

            _model.Bet = CurrentBet;
        }

        private void BtnBet_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.BorderBrush = _mouseLeaveBrush;
            btn.Cursor = Cursors.Arrow;
        }

        private void BtnBet_MouseOver(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.BorderBrush = _mouseOverBrush;
            btn.Cursor = Cursors.Hand;
        }

        private void BtnIncreaseBet_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentBet + _minimumBet > _humanPlayer.Money)
            {
                CurrentBet = _humanPlayer.Money;
                return;
            }
            CurrentBet += _minimumBet;
        }

        private void BtnReduceBet_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentBet - _minimumBet < _minimumBet)
            {
                CurrentBet = _minimumBet;
                return;
            }
            CurrentBet -= _minimumBet;
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize += 4;
            if (btn.Content is StackPanel sp)
            {
                sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip2);
            }
            btn.Cursor = Cursors.Hand;
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize -= 4;
            if (btn.Content is StackPanel sp)
            {
                sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
            }
            btn.Cursor = Cursors.Arrow;
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            _model.Hit();
            if (_humanPlayer.GetGameType<IBlackjackAction>().PlayerStand)
            {
                HideWhenStand();
            }
            _view.CreateNewImageSpace(_model._players.First(p => p is HumanPlayer), _model._players.First(p => p is BlackjackAI));
            _model.AIPlays();                   
        }

        private void Insurance_Click(object sender, RoutedEventArgs e)
        {
            _model.Insurance();
        }

        private void DoubleDown_Click(object sender, RoutedEventArgs e)
        {
            _model.DoubleDown();
            _model.AIPlays();

            HideWhenStand();
            CurrentBet = _model.Bet;
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            _model.Stand();
            _model.AIPlays();
            HideWhenStand();
        }

        private void HideWhenStand()
        {
            _view.BtnHit.Visibility = Visibility.Hidden;
            _view.BtnInsurance.Visibility = Visibility.Hidden;
            _view.BtnStand.Visibility = Visibility.Hidden;
            _view.BtnDoubleDown.Visibility = Visibility.Hidden;
            _view.BtnBet.Visibility = Visibility.Visible;
            _view.BtnIncreaseBet.Visibility = Visibility.Visible;
            _view.BtnReduceBet.Visibility = Visibility.Visible;
        }

        private void SetImages()
        {
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
