using CasinoUI.Model.Blackjack;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using System.Collections.Generic;

namespace CasinoUI.Model
{
    public class Player
    {
        public List<Card> Hand { get; set; } = new List<Card>();
        public int Money { get; set; }
    }
}
