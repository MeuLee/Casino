using CasinoUI.Model;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker.PokerBrains {
    public class TexasHoldemLogic : PokerLogic {

        public List<Card> TableCards { get; set; }
        public TexasHoldemLogic(PokerPlayer Human) : base(Human) {
            TableCards = new List<Card>();
        }

        private void playersDrawCards() {
            for (int i = playerRoles[3]; i < listPlayers.Count; i++) {
                listPlayers[i].Hand.Add(cardStack.DrawCard());
                listPlayers[i].Hand.Add(cardStack.DrawCard());
            }

            if (playerRoles[3] > 0) {
                for (int i = 0; i < playerRoles[3]; i++) {
                    listPlayers[i].Hand.Add(cardStack.DrawCard());
                    listPlayers[i].Hand.Add(cardStack.DrawCard());
                }
            }
        }

    }
}
