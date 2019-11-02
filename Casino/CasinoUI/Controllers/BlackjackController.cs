using CasinoUI.Models;
using CasinoUI.Models.Blackjack;
using CasinoUI.Utils;
using CasinoUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            _model = new BlackjackLogic(ApplicationSettings.HumanPlayer);
            _view = new GameBlackjack();
            _view.Show();

            AddEventsOnUIControls();
            SetImages();
            SetCardImagesTemp();
        }

        private void AddEventsOnUIControls()
        {
            foreach (var btn in _view.Grid.Children.OfType<Button>())
            {
                btn.MouseEnter += Btn_MouseEnter;
                btn.MouseLeave += Btn_MouseLeave;
            }
            _view.BtnAction1.Click += BtnAction1_Click;
            _view.BtnAction2.Click += BtnAction2_Click;
            _view.BtnAction3.Click += BtnAction3_Click;
            _view.BtnAction4.Click += BtnAction4_Click;
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

        private void BtnAction1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //call method in model
            //either the method returns something to this method or calls another method from this class
            //call method in view (refresh ui)
        }

        private void BtnAction2_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnAction3_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnAction4_Click(object sender, System.Windows.RoutedEventArgs e)
        {

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


        private void SetCardImagesTemp()
        {
            _view.Img1Player.Source = Properties.Resources._10C.ToBitmapImage();
            _view.Img2Player.Source = Properties.Resources._9D.ToBitmapImage();
            _view.Img1Dealer.Source = Properties.Resources._6S.ToBitmapImage();
            _view.Img2Dealer.Source = Properties.Resources._11S.ToBitmapImage();
        }
    }
}
