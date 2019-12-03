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

        /// <summary>
        /// Remet le joueur et le dealer a leur etat initial afin de commencer une nouvelle partie
        /// </summary>
        public void ResetPlayers()
        {
            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>(),
                             ai = _players.OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();

            human.PlayerBust = false;
            ai.PlayerBust = false;
            human.PlayerStand = false;
            ai.PlayerStand = false;
            human.Has21 = false;
            ai.Has21 = false;
        }

        /// <summary>
        /// Distribue les 2 premieres cartes au debut de la partie a chaque joueur
        /// </summary>
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

        /// <summary>
        /// Donne la valeur de la main au joueur
        /// </summary>
        /// <param name="player"></param>
        private void SetHandValue(Player player)
        {
            int handValue = 0;
            List<Card> tempHand = player.GetHand();
            tempHand = tempHand.OrderBy(o => o.Value).ToList();
            tempHand.Reverse();
            foreach (Card card in tempHand)
            {
                if (card.Value.Equals(CardRank.Jack) || card.Value.Equals(CardRank.Queen) || card.Value.Equals(CardRank.King))
                {
                    handValue += 10;
                }
                else if (card.Value.Equals(CardRank.Ace) && (handValue + 11 <= 21))
                {
                    handValue += 11;
                }
                else if (card.Value.Equals(CardRank.Ace) && (handValue + 11 > 21))
                {
                    handValue += 1;
                }
                else
                {
                    handValue += (int)card.Value;
                }
            }
            player.GetGameType<IBlackjackAction>().PlayerHandValue = handValue;
            if(handValue == 21)
            {
                player.GetGameType<IBlackjackAction>().Has21 = true;
            }
        }

        /// <summary>
        /// Methode qui determine la maniere dont le dealer va jouer
        /// Dans notre version de jeu, le dealer Hit lorsqu'il a un soft 17
        /// </summary>
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
                CheckBust(ai, GetAI());
                if (ai.Has21)
                {
                    ai.PlayerStand = true;
                }
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

        /// <summary>
        /// Methode qui verifie si le dealer a un soft 17
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Methode Insurance ne fonctionne pas en ce moment
        /// </summary>
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

        /// <summary>
        /// Double le pari et le joueur tire une carte
        /// </summary>
        public void DoubleDown()
        {
            Player tempP = GetHuman();
            CardStack.PlayerDrawCard(tempP);
            tempP.GetGameType<IBlackjackAction>().PlayerStand = true;
            if (Bet *2 > tempP.Money)
            {
                Bet += tempP.Money;
                tempP.Money = 0;
                return;
            }
            CheckBust(tempP.GetGameType<IBlackjackAction>(), GetHuman());
            tempP.Money -= Bet;
            Bet *= 2;
        }

        /// <summary>
        /// Termine le tour du joueur
        /// </summary>
        public void Stand()
        {
            GetHuman().GetGameType<IBlackjackAction>().PlayerStand = true;
        }

        /// <summary>
        /// Le joueur tire une carte
        /// </summary>
        public void Hit()
        {
            CardStack.PlayerDrawCard(GetHuman());
            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>();
            CheckBust(human, GetHuman());
            if (human.Has21)
            {
                human.PlayerStand = true;
            }
        }

        /// <summary>
        /// Verifie si le joueur en parametre a tirer une carte qui le met au dessus de 21
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pType"></param>
        private void CheckBust(IBlackjackAction player, Player pType)
        {
            SetHandValue(pType);

            if(player.PlayerHandValue > 21)
            {
                player.PlayerStand = true;
                player.PlayerBust = true;
            }
        }

        /// <summary>
        /// Verifie si le joueur a un Blackjack. Cette methode est seulement appeler suite a
        /// la distribution des cartes.
        /// </summary>
        public void CheckBlackJack()
        {
            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>(),
                             ai = _players.OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();

            Player humanPlayer = GetHuman();
            Player aiPlayer = GetAI();

            SetHandValue(humanPlayer);
            SetHandValue(aiPlayer);

            if(human.Has21 && ai.Has21)
            {
                humanPlayer.Money += Bet;
                human.PlayerStand = true;
                ai.PlayerStand = true;
            }
            else if(human.Has21)
            {
                humanPlayer.Money += Bet + (int)(Bet * 1.5);
                human.PlayerStand = true;
                ai.PlayerStand = true;
            }
            else if(ai.Has21)
            {
                human.PlayerStand = true;
                ai.PlayerStand = true;
            }
        }

        /// <summary>
        /// Verifie quel joueur a gagne et distribue l'argent du pari
        /// </summary>
        /// <returns>Un String qui est afficher sur le UI suite a la partie</returns>
        public String CheckForWinner()
        {
            IBlackjackAction human = _players.OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>(),
                             ai = _players.OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();

            Player humanPlayer = GetHuman();
            Player aiPlayer = GetAI();

            SetHandValue(humanPlayer);
            SetHandValue(aiPlayer);

            if (human.PlayerBust)
            {
                return "Dealer Wins";
            }
            else if (ai.PlayerBust)
            {
                humanPlayer.Money += Bet * 2;
                return "Player Wins";
            }
            else if(human.PlayerHandValue == ai.PlayerHandValue)
            {
                humanPlayer.Money += Bet;
                return "Draw";
            }
            else if(human.PlayerHandValue > ai.PlayerHandValue)
            {
                humanPlayer.Money += Bet * 2;
                return "Player Wins";
            }
            else
            {
                return "Dealer Wins";
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>Retourne le joueur humain</returns>
        public Player GetHuman()
        {
            return _players.First(p => p is HumanPlayer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>Retourne le joueur dealer</returns>
        public Player GetAI()
        {
            return _players.First(p => p is BlackjackAI);
        }
    }
}
