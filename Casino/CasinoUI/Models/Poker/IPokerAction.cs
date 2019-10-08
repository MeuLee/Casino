using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    interface IPokerAction {
        void PokerCheck(int money);
        void PokerFold();
        void PokerRaise();
        void PokerCall();

    }
}
