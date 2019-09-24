using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    public class PokerLogic {
        private List<PokerAI> ListAI;
        private HumanPlayer Human;

        public PokerLogic(HumanPlayer Human) {
            this.Human = Human;
        }

        private void InitAI() {
            for (int i = 0; i < 4; i++) {
                ListAI[i] = new PokerAI();
            }
        }


    }
}
