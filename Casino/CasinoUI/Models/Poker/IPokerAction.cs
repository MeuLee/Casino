using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    interface IPokerAction {
        void PokerCheck();
        void PokerFold();
        void PokerRaise(int money);
        void PokerCall();

    }
}
