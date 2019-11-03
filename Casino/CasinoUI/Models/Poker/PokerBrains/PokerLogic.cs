using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using System.Collections.Generic;

namespace CasinoUI.Models.Poker.PokerBrains
{
    public class PokerLogic {
        public List<Player> ListPlayers { get; set; }  // index 0 is always the human player
        public GameCardStack CardStack { get; set; }

        public int[] PlayerRoles { get; set; }      // <---  idx[0] = SmallBlind's index in ListPlayers
                                                    //       idx[1] = BigBlind's index in ListPlayers
                                                    //       idx[2] = First player to play
        public int Pot { get; set; }
        public int CurrentRaise { get; set; }

        public PokerLogic(HumanPlayer human) {
            InitListPlayers(human);
            SetInitialRoles();
            CardStack = new GameCardStack();
            Pot = 0;
            CurrentRaise = 2;
        }

        private void InitListPlayers(HumanPlayer human)
        {
            ListPlayers = new List<Player> { human };

            for (int i = 0; i < 4; i++) {
                ListPlayers.Add(new PokerAI());
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
            ClearRoles();
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

        private void ClearRoles() {
            foreach (Player player in ListPlayers) {
                player.GetHand().Clear();
            }
        }

        private void PlayerPlaysTurn(PokerActionCode pokerActionCode, int playerIdx) {
            IPokerAction player = ListPlayers[playerIdx].GetGameType<IPokerAction>();
            switch (pokerActionCode) {
                case PokerActionCode.CALL:
                    Pot += player.PokerCall(CurrentRaise);
                    break;
                case PokerActionCode.CHECK:
                    player.PokerCheck();
                    break;
                case PokerActionCode.FOLD:
                    player.PokerFold();
                    break;
                case PokerActionCode.RAISE:
                    Pot += player.PokerRaise(22); // should take in amount from UI
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
