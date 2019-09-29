using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    interface IPokerAction {
        void Check();
        void Fold();
        void Raise();
        void Call();

    }
}
