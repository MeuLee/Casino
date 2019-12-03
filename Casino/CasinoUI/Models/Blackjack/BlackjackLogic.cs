using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Profiles;
using System.Collections.Generic;

namespace CasinoUI.Models.Blackjack
{
    public class BlackjackLogic
    {
        private HumanPlayer Human;
        public List<Player> ListPlayers { get; set; }
        public GameCardStack CardStack { get; set; }

        public int Bet { get; set; }

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
            Bet = 0;
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
                    CheckHandValue(PlayerHandValue, player);

                }
                else
                {
                    CheckHandValue(DealerHandValue, player);
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
            ListPlayers = new List<Player>
            {
                Human,
                new BlackjackAI()
            };
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
                    CheckHandValue(PlayerHandValue, player);

                } else
                {
                    CheckHandValue(DealerHandValue, player);
                }
            }
            RoundEnd = false;
        }

        private void CheckHandValue(int handValue, Player player)
        {
            handValue = 0;
            foreach (Card card in player.Hand)
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
            Bet = 0;
        }

        private void ClearHands()
        {
            foreach (Player player in ListPlayers)
            {
                player.Hand.Clear();
            }
            PlayerHandValue = 0;
            DealerHandValue = 0;
        }

        private void CurrentPlayerPlay(BlackjackActionCode Action, HumanPlayer CurrentPlayer)
        {
            switch (Action)
            {
                case BlackjackActionCode.HIT:
                    CardStack.PlayerDrawCard(CurrentPlayer);
                    if (CurrentPlayer is HumanPlayer)
                    {
                        CheckHandValue(PlayerHandValue, CurrentPlayer);
                    }
                    else
                    {
                        CheckHandValue(DealerHandValue, CurrentPlayer);
                    }
                    break;
                case BlackjackActionCode.STAND:
                    if(CurrentPlayer is HumanPlayer)
                    {
                        PlayerStand = true;
                    } else
                    {
                        DealerStand = true;
                    }
                    break;
                case BlackjackActionCode.INSURANCE:
                    //only available if dealer face up card is ACE
                    //first turn only
                    //put side bet = to 1/2 initial bet.
                    //If dealer has 21 on first 2 cards, dealer pays 2:1 the side bet but you lose the init bet
                    //If dealer has 21 on first 2 cards and so does player, dealer pays 2:1 the side bet and you take back init bet
                    //If dealer does not have 21 on first 2 cards, player loses side bet and continue playing until end
                    //If in previous case Player wins, player gets init bet, but loses side bet
                    //If in previous case Player loses, player loses both bets
                    break;
                case BlackjackActionCode.DOUBLEDOWN:
                    CardStack.PlayerDrawCard(CurrentPlayer);
                    CurrentPlayer.GetGameType<IBlackjackAction>().BlackjackDoubleDown(Bet);
                    //Double init bet
                    break;
            }

            IncCurrentPlayerTurn();
        }

        private void IncCurrentPlayerTurn()
        {

        }
    }
}
