using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Blackjack {
    interface IBlackjackAction {
        void BlackjackHit();
        void BlackjackStand();
        void BlackjackSplit();
        void BlackjackInsurance();
        void BlackjackDoubleDown();

    }
}
