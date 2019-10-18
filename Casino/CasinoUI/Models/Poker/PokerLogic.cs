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
        public List<Player> ListAI { get; set; }
        public GameCardStack CardStack { get; set; }

        public int[] PlayerRoles { get; set; }      // <---  idx[0] = SmallBlind's index in ListPlayers                            
                                                    //       idx[1] = BigBlind's index in ListPlayers
        public int Pot { get; set; }
        public int CurrentRaise { get; set; }

        public PokerLogic(HumanPlayer Human) {
            this.Human = Human;
            InitListPlayers();
            SetInitialRoles();
            CardStack = new GameCardStack();            
            Pot = 0;
        }

        private void InitListPlayers() {
            ListAI = new List<Player>();

            for (int i = 0; i < ListAI.Count; i++) {
                ListAI[i] = new PokerAI();
                ListAI[i].Money = 1000;
            }
        }

        private void SetInitialRoles() {
            PlayerRoles = new int[2];

            PlayerRoles[0] = 0;
            PlayerRoles[1] = 1;

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
            int listLength = ListAI.Count;

            for (int i = 0; i < PlayerRoles.Length; i++) {
                PlayerRoles[i]++;

                if (PlayerRoles[i] == listLength) {
                    PlayerRoles[i] = 0;
                }
            }
        }

        private void ClearHands() {
            foreach (Player player in ListAI) {
                player.Cards.Clear();
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
