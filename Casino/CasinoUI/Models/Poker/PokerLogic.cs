using CasinoUI.Model.Cards;
using CasinoUI.Models.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    public class PokerLogic {
        private HumanPlayer human;
        public List<Player> listAI { get; set; }
        public GameCardStack cardStack { get; set; }

        public int[] playerRoles { get; set; }      // <---  idx[0] = SmallBlind's index in ListPlayers                            
                                                    //       idx[1] = BigBlind's index in ListPlayers
        public int pot { get; set; }
        public int currentRaise { get; set; }

        public PokerLogic(HumanPlayer Human) {
            this.human = Human;
            initListPlayers();
            setInitialRoles();
            cardStack = new GameCardStack();            
            pot = 0;
        }

        private void initListPlayers() {
            listAI = new List<Player>();

            for (int i = 0; i < listAI.Count; i++) {
                listAI[i] = new PokerAI();
                listAI[i].Money = 1000;
            }
        }

        private void setInitialRoles() {
            playerRoles = new int[2];

            playerRoles[0] = 0;
            playerRoles[1] = 1;

            // draw high card for dealer
        }

        private void proceedNextTurn() {
            rotateRoles();
            clearRoles();
            pot = 0;
            // restore deck and shuffle
            // check if everyone has enough money
        }

        private void rotateRoles() {
            int listLength = listAI.Count;

            for (int i = 0; i < playerRoles.Length; i++) {
                playerRoles[i]++;

                if (playerRoles[i] == listLength) {
                    playerRoles[i] = 0;
                }
            }
        }

        private void clearRoles() {
            foreach (Player player in listAI) {
                player.Cards.Clear();
            }
        }

        private void playTurn(PokerActionCode pokerActionCode) {
            switch (pokerActionCode) {
                case PokerActionCode.CALL:

                    break;
                case PokerActionCode.CHECK:
                    break;
                case PokerActionCode.FOLD:
                    break;
                case PokerActionCode.RAISE:
                    break;
            }



            // AIs play their turn.
        }

    }
}

/// TODO: 1. make initial blinds randomly chosen
///       2. Restore deck and shuffle
///       

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
