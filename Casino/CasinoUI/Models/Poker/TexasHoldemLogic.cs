using CasinoUI.Model;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker {
    public class TexasHoldemLogic : PokerLogic {

        public List<Card> TableCards { get; set; }
        public TexasHoldemLogic(HumanPlayer Human) : base(Human) {
            TableCards = new List<Card>();
        }


    }
}
