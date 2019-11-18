using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Profiles;
using System.Collections.Generic;
using System.Linq;

namespace CasinoUI.Models.Blackjack
{
    public class BlackjackLogic
    {
        public List<Player> _players { get; }
        public GameCardStack CardStack { get; set; }

        public int Bet { get; set; }

        public bool PlayerStand { get; set; }
        public bool DealerStand { get; set; }

        public bool RoundEnd { get; set; }

        public BlackjackLogic(HumanPlayer human)
        {
            CardStack = new GameCardStack();
            Bet = 0;
            _players = new List<Player>() { human, new BlackjackAI() };
        }

        private void GameFlow()
        {
            DistributeCards();

            foreach (var player in _players)
            {
                SetHandValue(player);
            }

            CheckForBlackjack();

            if (RoundEnd)
            {
                ProceedNextTurn();
            }
        }

        private void DistributeCards()
        {
            PlayerStand = false;
            DealerStand = false;
            foreach (Player player in _players)
            {
                CardStack.PlayerDrawCard(player);
                CardStack.PlayerDrawCard(player);
                SetHandValue(player);
            }
            RoundEnd = false;
        }

        private void SetHandValue(Player player)
        {
            int handValue = 0;
            foreach (Card card in player.GetHand())
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
            player.GetGameType<IBlackjackAction>().PlayerHandValue = handValue;
        }

        private void CheckForBlackjack()
        {
            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>(),
                             ai = _players.OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();
            
            if (human.PlayerHandValue == 21 && ai.PlayerHandValue != 21)
            {
                //Player wins bet 3:2
                RoundEnd = true;
            }
            else if (human.PlayerHandValue == 21 && ai.PlayerHandValue == 21)
            {
                //There is a tie. Return bet to player.
                RoundEnd = true;
            }
        }

        private void ProceedNextTurn()
        {
            ClearHands();
            Bet = 0;
        }

        private void ClearHands()
        {
            foreach (Player player in _players)
            {
                player.GetHand().Clear();
                player.GetGameType<IBlackjackAction>().PlayerHandValue = 0;
            }
        }

        public void Hit()
        {
            Player tempP = new Player();
            foreach(Player p in _players)
            {
                if(p is HumanPlayer)
                {
                    tempP = p;
                }
            }
            CardStack.PlayerDrawCard(tempP);
        }

        private void CurrentPlayerPlay(BlackjackActionCode Action, HumanPlayer CurrentPlayer)
        {
            switch (Action)
            {
                case BlackjackActionCode.HIT:
                    CardStack.PlayerDrawCard(CurrentPlayer);
                    if (CurrentPlayer is HumanPlayer)
                    {
                        //CheckHandValue(PlayerHandValue, CurrentPlayer); TODO @Motoki
                    }
                    else
                    {
                        //CheckHandValue(DealerHandValue, CurrentPlayer); TODO @Motoki
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
