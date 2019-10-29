using CasinoUI.Model.Blackjack;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using System.Collections.Generic;

namespace CasinoUI.Model {
    public class Player : IBlackjackAction {
        public List<Card> Hand { get; set; } = new List<Card>();
        public int Money { get; set; }

        public void BlackjackDoubleDown()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackHit()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackInsurance()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackSplit()
        {
            throw new System.NotImplementedException();
        }

        public void BlackjackStand() 
        {
            throw new System.NotImplementedException();
        }
    }
}
