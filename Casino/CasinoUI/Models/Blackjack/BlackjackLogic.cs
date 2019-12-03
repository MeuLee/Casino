using CasinoUI.Controllers;
using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using static CasinoUI.Models.Cards.Card;

namespace CasinoUI.Models.Blackjack
{
    public class BlackjackLogic
    {
        public List<Player> _players { get; }
        public GameCardStack CardStack { get; set; }

        public int Bet { get; set; }

        BlackjackController BJController;

        public BlackjackLogic(HumanPlayer human, BlackjackController blackjackController)
        {
            human.CurrentProfile = new BlackjackProfile();
            CardStack = new GameCardStack();
            BJController = blackjackController;
            Bet = 0;
            _players = new List<Player>() { human, new BlackjackAI() };
        }

        public void DistributeCards()
        {            
            foreach (Player player in _players)
            {
                player.GetHand().Clear();
                CardStack.PlayerDrawCard(player);
                CardStack.PlayerDrawCard(player);
                SetHandValue(player);
            }
        }

        private void SetHandValue(Player player)
        {
            int handValue = 0;
            foreach (Card card in player.GetHand())
            {
                if (card.Equals(CardRank.Jack) || card.Equals(CardRank.Queen) || card.Equals(CardRank.King))
                {
                    handValue += 10;
                }
                else if (card.Equals(CardRank.Ace) && (handValue + 11 <= 21))
                {
                    handValue += 11;
                }
                else if (card.Equals(CardRank.Ace) && (handValue + 11 > 21))
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

        public void AIPlays()
        {
            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>(),
                             ai = _players.OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();

            if (ai.PlayerStand)
            {
                return;
            }

            SetHandValue(GetAI());

            if (ai.PlayerHandValue <= 16 || CheckSoft17(ai))
            {
                CardStack.PlayerDrawCard(GetAI());
                BJController.UpdateViewNewCardAI();
                CheckBust(ai);
            }
            else
            {
                ai.PlayerStand = true;
            } 

            if(human.PlayerStand)
            {
                AIPlays();
            }
        }

        private bool CheckSoft17(IBlackjackAction ai)
        {
            int sum = GetAI().GetHand().Select(c => (int)c.Value).Where(c => (CardRank)c != CardRank.Ace).Sum();

            if (!GetAI().GetHand().Select(c => c.Value).Contains(CardRank.Ace))
            {
                return false;
            }
            else if (sum < 7)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public void Insurance()
        {
            //only available if dealer face up card is ACE
            //first turn only
            //put side bet = to 1/2 initial bet.
            //If dealer has 21 on first 2 cards, dealer pays 2:1 the side bet but you lose the init bet
            //If dealer has 21 on first 2 cards and so does player, dealer pays 2:1 the side bet and you take back init bet
            //If dealer does not have 21 on first 2 cards, player loses side bet and continue playing until end
            //If in previous case Player wins, player gets init bet, but loses side bet
            //If in previous case Player loses, player loses both bets
        }

        public void DoubleDown()
        {
            Player tempP = GetHuman();
            CardStack.PlayerDrawCard(tempP);
            tempP.GetGameType<IBlackjackAction>().PlayerStand = true;
            if (Bet *2 > tempP.Money)
            {
                Bet = tempP.Money;
                return;
            }
            Bet *= 2;
        }      

        public void Stand()
        {
            GetHuman().GetGameType<IBlackjackAction>().PlayerStand = true;
        }

        public void Hit()
        {
            CardStack.PlayerDrawCard(GetHuman());
            if(GetHuman().GetGameType<IBlackjackAction>().PlayerHandValue >= 22)
            {
                GetHuman().GetGameType<IBlackjackAction>().PlayerStand = true;
            }

            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>();

            CheckBust(human);            
        }

        private void CheckBust(IBlackjackAction player)
        {
            SetHandValue(GetHuman());

            if(player.PlayerHandValue > 21)
            {
                player.PlayerStand = true;
                player.PlayerBust = true;
            }
        }

        public Player GetHuman()
        {
            return _players.First(p => p is HumanPlayer);
        }

        public Player GetAI()
        {
            return _players.First(p => p is BlackjackAI);
        }
    }
}
