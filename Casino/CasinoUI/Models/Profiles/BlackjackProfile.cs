using CasinoUI.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Profiles {
    class BlackjackProfile : Profile, IBlackjackAction {
        public int BlackjackDoubleDown(int currentBet) {
            throw new NotImplementedException();
        }

        public void BlackjackHit() {
            throw new NotImplementedException();
        }

        public void BlackjackInsurance() {
            throw new NotImplementedException();
        }

        public void BlackjackStand() {
            throw new NotImplementedException();
        }
    }
}
