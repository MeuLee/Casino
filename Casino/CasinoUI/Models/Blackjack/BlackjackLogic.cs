using CasinoUI.Model.Cards;
using System.Collections.Generic;
using System;
using CasinoUI.Models.Blackjack;

namespace CasinoUI.Model.Blackjack
{
    public class BlackjackLogic
    {
        private HumanPlayer Human;
        public List<Player> ListPlayers { get; set; }
        public GameCardStack CardStack { get; set; }

        public int Pot { get; set; }

        public int PlayerHandValue { get; set; }
        public int DealerHandValue { get; set; }

        public bool PlayerStand { get; set; }
        public bool DealerStand { get; set; }

        public bool RoundEnd { get; set; }

        public BlackjackLogic(HumanPlayer Human)
        {
            this.Human = Human;
            InitListPlayers();
            CardStack = new GameCardStack();
            Pot = 0;
            PlayerHandValue = 0;
            DealerHandValue = 0;            
        }

        private void GameFlow()
        {
            DistributeCards();

            foreach (Player player in ListPlayers)
            {
                if (player is HumanPlayer)
                {
                    checkHandValue(PlayerHandValue, player);

                }
                else
                {
                    checkHandValue(DealerHandValue, player);
                }
            }

            CheckForBlackjack();
            if (RoundEnd)
            {
                ProceedNextTurn();
            }
        }

        private void CheckForBlackjack()
        {
            if (PlayerHandValue == 21 && DealerHandValue != 21)
            {
                //Player wins bet 3:2
                RoundEnd = true;
            } else if (PlayerHandValue == 21 && DealerHandValue == 21)
            {
                //There is a tie. Return bet to player.
                RoundEnd = true;
            }
        }

        private void InitListPlayers()
        {
            ListPlayers[0] = Human;
            ListPlayers[1] = new BlackjackAI();
        }

        private void DistributeCards()
        {
            PlayerStand = false;
            DealerStand = false;
            foreach (Player player in ListPlayers)
            {
                CardStack.PlayerDrawCard(player);
                CardStack.PlayerDrawCard(player);
                if(player is HumanPlayer)
                {
                    checkHandValue(PlayerHandValue, player);
                    
                } else
                {
                    checkHandValue(DealerHandValue, player);
                }
            }
            RoundEnd = false;
        }

        private void checkHandValue(int handValue, Player player)
        {
            handValue = 0;
            foreach (Card card in player.Cards)
            {
                if (card.Equals(Card.CardRank.Jack) || card.Equals(Card.CardRank.Queen) || card.Equals(Card.CardRank.King))
                {
                    handValue += 10;
                }
                else if (card.Equals(Card.CardRank.Ace) && (handValue + 11 <= 21))
                {
                    handValue += 11;
                }
                else if (card.Equals(Card.CardRank.Ace) && (handValue + 11 > 21))
                {
                    handValue += 1;
                }
                else
                {
                    handValue += (int)card.Value;
                }
            }
        }

        private void ProceedNextTurn()
        {
            ClearHands();
            Pot = 0;
        }

        private void ClearHands()
        {
            foreach (Player player in ListPlayers)
            {
                player.Cards.Clear();
            }
            PlayerHandValue = 0;
            DealerHandValue = 0;
        }

        private void CurrentPlayerPlay(BlackjackActionCode Action, Player CurrentPlayer)
        {
            // TODO: add logic
            switch (Action)
            {
                case BlackjackActionCode.HIT:
                    CardStack.PlayerDrawCard(CurrentPlayer);                    
                    break;
                case BlackjackActionCode.STAND:
                    break;
                case BlackjackActionCode.INSURANCE:
                    break;
                case BlackjackActionCode.DOUBLEDOWN:
                    CardStack.PlayerDrawCard(CurrentPlayer);
                    break;
            }

            IncCurrentPlayerTurn();
        }

        private void IncCurrentPlayerTurn()
        {

        }
    }
}