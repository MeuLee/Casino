    using CasinoUI.Model.Cards;
using CasinoUI.Models.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    public class PokerLogic {
        private HumanPlayer Human;
        public List<Player> ListPlayers { get; set; }
        public GameCardStack CardStack { get; set; }

        public int[] PlayerRoles { get; set; }      // <---  idx[0] = SmallBlind's index in ListPlayers
                                                    //       idx[1] = BigBlind's index in ListPlayers
                                                    //       idx[2] = CurrentPlayersTurn index in ListPlayers

        public int Pot { get; set; }
        public int CurrentRaise { get; set; }
        public List<Card> TableCards { get; set; }

        public PokerLogic(HumanPlayer Human) {
            this.Human = Human;
            InitListPlayers();
            SetInitialRoles();
            CardStack = new GameCardStack();
            TableCards = new List<Card>();
            Pot = 0;
        }

        private void GameFlow() {
            // 1. Distribute cards to players
            DistributeCards();

            // 2. small blind deposits
            // 3. Big blind deposits
            BlindsInitialBet();

            // 4. firstPlayer decides to call, fold or raise
            // 5. other players do the same...
            // 6. back to small blind: He decides if he wants to call, raise or fold
            // 7. back to big blind: He decides if he wants to raise or fold if someone raised
            CurrentPlayerPlay(PokerActionCode.CALL);

            // 8. Place first 3 cards on table
            PlaceInit3TableCards();

            // 9. rotate through players for them to play...
            // 10. go to second round... (NOTE: check if everybody but one folded or not)
            // 11. third... (NOTE: check if everybody but one folded or not)
            // 12. Declare winner and gibe him the pot
            // 13. proceed to next turn
        }

        private void InitListPlayers() {
            ListPlayers[0] = Human;

            for (int i = 1; i < ListPlayers.Count; i++) {
                ListPlayers[i] = new PokerAI();
                ListPlayers[i].Money = 1000;
            }
        }

        private void SetInitialRoles() {
            PlayerRoles = new int[3];

            PlayerRoles[0] = 0;
            PlayerRoles[1] = 1;
            PlayerRoles[2] = 2;

            // draw high card for dealer
        }

        private void ProceedNextTurn() {
            RotateRoles();
            ClearHands();
            Pot = 0;
            // restore deck and shuffle
            // check if everyone has enough money
        }

        private void RotateRoles() {
            int listLength = ListPlayers.Count;

            for (int i = 0; i < PlayerRoles.Length; i++) {
                PlayerRoles[i]++;

                if (PlayerRoles[i] == listLength) {
                    PlayerRoles[i] = 0;
                }
            }
        }

        private void ClearHands() {
            foreach (Player player in ListPlayers) {
                player.Cards.Clear();
            }
        }

        private void DistributeCards() {
            // TODO: Give cards to first player first;
            foreach (Player player in ListPlayers) {
                CardStack.PlayerDrawCard(player);
                CardStack.PlayerDrawCard(player);
            }
        }

        private void BlindsInitialBet() {
            Pot += 4;
            ListPlayers[PlayerRoles[0]].Money -= 1;
            ListPlayers[PlayerRoles[1]].Money -= 3;
        }

        private void CurrentPlayerPlay(PokerActionCode Action) {
            // TODO: add logic

            IncCurrentPlayerTurn();
        }

        private void PlaceInit3TableCards() {
            for (int i = 0; i < 3; i++) {
                TableCards.Add(CardStack.DrawCard());
            }
        }

        private void IncCurrentPlayerTurn() {
            PlayerRoles[2]++;

            if (PlayerRoles[2] == ListPlayers.Count) {
                PlayerRoles[2] = 0;
            }
        }
    }
}

/// TODO: 1. make initial blinds randomly chosen
///       2. Restore deck and shuffle
