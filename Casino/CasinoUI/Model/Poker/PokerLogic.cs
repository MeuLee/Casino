using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    public class PokerLogic {
        private HumanPlayer Human;
        private List<Player> ListPlayers;
        private int FirstPlayer;
        private int SmallBlind;
        private int BigBlind;       

        public PokerLogic(HumanPlayer Human) {
            this.Human = Human;
            InitListPlayers();
            SetInitialRoles();
        }

        private void GameFlow() {
            // 1. Distribute cards to players
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
            SmallBlind = 0;
            BigBlind = 1;
            FirstPlayer = 2;
        }

        private void ProceedNextTurn() {
            RotateRoles();
            ClearHands();
            // restore deck and shuffle
        }

        private void RotateRoles() {
            int listLength = ListPlayers.Count;

            // Can refactor into array
            if (SmallBlind == listLength) {
                SmallBlind = 0;
            }
            else {
                SmallBlind++;
            }

            if (BigBlind == listLength) {
                BigBlind = 0;
            }
            else {
                BigBlind++;
            }

            if (FirstPlayer == listLength) {
                FirstPlayer = 0;
            }
            else {
                FirstPlayer++;
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
