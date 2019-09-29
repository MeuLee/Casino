using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using System.Collections.Generic;

namespace CasinoUI.Model {
    public class Player : IPokerAction {
        public List<Card> Cards { get; set; } = new List<Card>();
        public int Money { get; set; }

        public void Call() {
            throw new System.NotImplementedException();
        }

        public void Check() {
            throw new System.NotImplementedException();
        }

        public void Fold() {
            throw new System.NotImplementedException();
        }

        public void Raise() {
            throw new System.NotImplementedException();
        }
    }
}
