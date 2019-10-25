using CasinoUI.Model;
using CasinoUI.Model.Poker;
using CasinoUI.Models.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models {
    public class PokerPlayer : Player, IPokerAction {
        public int State { get; set; }
        public int PokerCall(int currentRaise) {
            int callMoney;
            if (Money > currentRaise) {
                callMoney = currentRaise;
                Money -= callMoney;
            }
            else {
                callMoney = Money;
                Money = 0;
            }

            return callMoney;
        }

        public void PokerCheck() {
            State = (int) PokerPlayerState.CHECKED;
        }

        public void PokerFold() {
            State = (int) PokerPlayerState.FOLDED;
        }

        public int PokerRaise(int moneyRaised) {
            // UI should limit the amount to be raised as to not go over the amount of the player's money.
            Money -= moneyRaised;
            return moneyRaised;
        }
    }
}
