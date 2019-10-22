using CasinoUI.Model;
using CasinoUI.Model.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models {
    class PokerPlayer : Player, IPokerAction {
        public void PokerCall() {
            throw new System.NotImplementedException();
        }

        public void PokerCheck() {
            throw new System.NotImplementedException();
        }

        public void PokerFold() {
            throw new System.NotImplementedException();
        }

        public void PokerRaise(int money) {
            throw new System.NotImplementedException();
        }
    }
}
