using CasinoUI.Models.Cards;
using CasinoUI.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CasinoUI.Views
{
    /// <summary>
    /// Interaction logic for BlackJack.xaml
    /// </summary>
    public partial class GameBlackjack : Window
    {
        List<Image> _imgHuman;
        List<Image> _imgAi;

        public GameBlackjack()
        {
            InitializeComponent();
            _imgHuman = new List<Image>();
            _imgAi = new List<Image>();
        }

        public void CreateNewImageSpace(Models.PlayerModel.Player humanPlayer, Models.PlayerModel.Player aiPlayer)
        {
            List<Card> _humanCards = humanPlayer.GetHand();
            AddCardImgUI(_humanCards, _imgHuman, 239, 65);

            List<Card> _aiCards = aiPlayer.GetHand();
            AddCardImgUI(_aiCards, _imgAi, 24, 280);   
        }

        private void AddCardImgUI(List<Card> cards, List<Image> images, int thiccValue1, int thiccValue2)
        {
            
            for (int i = images.Count; i < cards.Count; i++)
            {
                Image image = new Image() { Height = 76, Width = 50, Margin = new Thickness(193 + i * 20, thiccValue1, 223 - i * 20, thiccValue2) };
                image.SetValue(Grid.ColumnSpanProperty, 2);
                image.SetValue(Grid.ColumnProperty, 2);
                image.Source = cards[i].Image.ToBitmapImage();
                Grid.Children.Add(image);
            }
        }

        public void Stand()
        {

        }
    }
}
