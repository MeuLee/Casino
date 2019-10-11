using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using System.Collections.Generic;

namespace CasinoUI.Model {
    public class Player : IPokerAction {
        public List<Card> Cards { get; set; } = new List<Card>();
        public int Money { get; set; }

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
