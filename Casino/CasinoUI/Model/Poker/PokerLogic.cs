using CasinoUI.Model.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    public class PokerLogic {
        private HumanPlayer Human;
        private List<Player> ListPlayers;
        private GameCardStack CardStack;

        private int[] PlayerRoles; // <---  idx[0] = SmallBlind's index in ListPlayers
                                   //       idx[1] = BigBlind's index in ListPlayers
                                   //       idx[2] = CurrentPlayersTurn index in ListPlayers

        public PokerLogic(HumanPlayer Human) {
            this.Human = Human;
            InitListPlayers();
            SetInitialRoles();
            CardStack = new GameCardStack();
        }

        private void GameFlow() {
            // 1. Distribute cards to players
            // TODO: Give cards to first player first;
            foreach (Player player in ListPlayers) {
                CardStack.DrawCard(player);
                CardStack.DrawCard(player);
            }

            // 2. small blind deposits
            // 3. Big blind deposits
            // 4. firstPlayer decides to call, fold or raise
            // 5. other players do the same...
            // 6. back to small blind: He decides if he wants to call, raise or fold
            // 7. back to big blind: He decides if he wants to raise or fold if someone raised
            // 8. Place first 3 cards on table
            // 9. rotate through players for them to play...
            // 10. go to second round... (NOTE: check if everybody but one folded or not)
            // 11. third... (NOTE: check if everybody but one folded or not)
            // 12. Declare winner and gibe him the pot
            // 13. proceed to next turn
        }

        private void InitListPlayers() {
            ListPlayers[0] = Human;

            for (int i = 1; i < 5; i++) {
                ListPlayers[i] = new PokerAI();
            }
        }

        private void SetInitialRoles() {
            PlayerRoles = new int[3];

            PlayerRoles[0] = 0;
            PlayerRoles[1] = 1;
            PlayerRoles[2] = 2;


        }

        private void ProceedNextTurn() {
            RotateRoles();
            ClearHands();
            // restore deck and shuffle
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
    }
}

/// TODO: 1. make initial blinds randomly chosen
///       2. Restore deck and shuffle
