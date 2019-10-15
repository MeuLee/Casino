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
        }

        private void InitListPlayers()
        {
            ListPlayers[0] = Human;
            ListPlayers[1] = new BlackjackAI();
        }

        private void DistributeCards()
        {
            foreach (Player player in ListPlayers)
            {
                CardStack.PlayerDrawCard(player);
                CardStack.PlayerDrawCard(player);
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
                case BlackjackActionCode.SPLIT:
                    break;
                case BlackjackActionCode.INSURANCE:
                    break;
                case BlackjackActionCode.DOUBLEDOWN:
                    break;
            }

            IncCurrentPlayerTurn();
        }

        private void IncCurrentPlayerTurn()
        {

        }
    }
}