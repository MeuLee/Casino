using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
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
        int GridChildrenBeforeNewCards;
        Grid tempGrid;
        int x = 0;

        public GameBlackjack()
        {
            InitializeComponent();
            _imgHuman = new List<Image>();
            _imgAi = new List<Image>();
            GridChildrenBeforeNewCards = Grid.Children.Count;

            tempGrid = this.Grid;
        }

        /// <summary>
        /// Methode qui determine les cartes du joueurs et du dealer qui doivent etre creer
        /// </summary>
        /// <param name="humanPlayer"></param>
        /// <param name="aiPlayer"></param>
        public void CreateNewImageSpace(Player humanPlayer, Player aiPlayer)
        {
            List<Card> _humanCards = humanPlayer.GetHand();
            AddCardImgUI(_humanCards, _imgHuman, "human");

            List<Card> _aiCards = aiPlayer.GetHand();
            
            AddCardImgUI(_aiCards, _imgAi, "ai");
            if (_aiCards.Count == 2)
            {
                _imgAi[0].Source = Properties.Resources.Carte_Dos.ToBitmapImage();
            }
        }

        /// <summary>
        /// Methode qui ajoute dynamiquement les nouvelles cartes au UI
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="images"></param>
        /// <param name="playerString"></param>
        private void AddCardImgUI(List<Card> cards, List<Image> images, String playerString)
        {            
            for (int i = images.Count; i < cards.Count; i++)
            {
                Image image = new Image() { Height = 76, Width = 50 };
                image.SetValue(Grid.ColumnSpanProperty, 2);
                image.SetValue(Grid.ColumnProperty, 2);
                image.Source = cards[i].Image.ToBitmapImage();
                if (playerString.Equals("ai"))
                {
                    AICards.Children.Add(image);
                }
                else
                {
                    HumanCards.Children.Add(image);
                }
                images.Add(image);
            }
        }

        /// <summary>
        /// Tourne la premiere carte de la main du dealer afin que le joueur puisse la voir
        /// </summary>
        /// <param name="aiPlayer"></param>
        public void RevealAIFirstCard(Player aiPlayer)
        {
            _imgAi[0].Source = aiPlayer.GetHand()[0].Image.ToBitmapImage();
        }

        /// <summary>
        /// Retire les images de cartes du ui
        /// </summary>
        public void ClearImgs()
        {                            
            _imgAi.Clear();
            _imgHuman.Clear();
            AICards.Children.Clear();
            HumanCards.Children.Clear();
            Grid.UpdateLayout();
        }
    }
}
