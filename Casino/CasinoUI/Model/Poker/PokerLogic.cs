using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Model.Poker {
    public class PokerLogic {
        private HumanPlayer Human;

        private List<Player> ListPlayers;

        public PokerLogic(HumanPlayer Human) {
            this.Human = Human;
        }

        private void InitListPlayers() {
            ListPlayers[0] = Human;

            for (int i = 1; i < 5; i++) {
                ListPlayers[i] = new PokerAI();
            }
        }
    }
}
