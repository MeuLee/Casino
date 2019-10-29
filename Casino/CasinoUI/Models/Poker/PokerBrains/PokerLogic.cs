using CasinoUI.Model;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using CasinoUI.Models;
using CasinoUI.Models.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker.PokerBrains {
    public class PokerLogic {
        public List<PokerPlayer> listPlayers { get; set; }  // index 0 is always the human player
        public GameCardStack cardStack { get; set; }

        public int[] playerRoles { get; set; }      // <---  idx[0] = SmallBlind's index in ListPlayers                            
                                                    //       idx[1] = BigBlind's index in ListPlayers
                                                    //       idx[2] = First player to play
        public int pot { get; set; }
        public int currentRaise { get; set; }

        public PokerLogic(PokerPlayer human) {
            initListPlayers(human);
            setInitialRoles();
            cardStack = new GameCardStack();            
            pot = 0;
            currentRaise = 2;
        }

        private void initListPlayers(PokerPlayer human) {
            listPlayers = new List<PokerPlayer>();

            listPlayers.Add(human);

            for (int i = 0; i < 4; i++) {
                listPlayers.Add(new PokerAI());
            }
        }

        private void setInitialRoles() {
            playerRoles = new int[3];

            playerRoles[0] = 0;
            playerRoles[1] = 1;
            playerRoles[2] = 2;

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
            int listLength = listPlayers.Count;

            for (int i = 0; i < playerRoles.Length; i++) {
                playerRoles[i]++;

                if (playerRoles[i] == listLength) {
                    playerRoles[i] = 0;
                }
            }
        }

        private void clearRoles() {
            foreach (Player player in listPlayers) {
                player.Hand.Clear();
            }
        }

        private void playerPlaysTurn(PokerActionCode pokerActionCode, int playerIdx) {
            switch (pokerActionCode) {
                case PokerActionCode.CALL:
                    pot += listPlayers[playerIdx].PokerCall(currentRaise);
                    break;
                case PokerActionCode.CHECK:
                    listPlayers[playerIdx].PokerCheck();
                    break;
                case PokerActionCode.FOLD:
                    listPlayers[playerIdx].PokerFold();
                    break;
                case PokerActionCode.RAISE:
                    pot += listPlayers[playerIdx].PokerRaise(22); // should take in amount from UI
                    break;
            }

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
