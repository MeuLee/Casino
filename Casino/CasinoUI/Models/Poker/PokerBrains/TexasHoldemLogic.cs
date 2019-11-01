using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Profiles;
using System.Collections.Generic;

namespace CasinoUI.Models.Poker.PokerBrains
{
    public class TexasHoldemLogic : PokerLogic {

        public List<Card> TableCards { get; set; }
        public TexasHoldemLogic(HumanPlayer Human) : base(Human) {
            TableCards = new List<Card>();
        }

        private void PlayersDrawCards() {
            foreach (var player in ListPlayers)
            {
                if (player is PlayerAI ai)
                {
                    ai.Hand.Add(CardStack.DrawCard());
                    ai.Hand.Add(CardStack.DrawCard());
                } 
                else if (player is PokerProfile pp)
                {
                    pp.Hand.Add(CardStack.DrawCard());
                    pp.Hand.Add(CardStack.DrawCard());
                }
            }

            if (PlayerRoles[3] > 0) // pas mal sur que ca va planter si playerroles.length <= 3
                                    // en fait ca sert a quoi? le foreach en haut devrait pogner toute les playerroles
            {
                foreach (var player in ListPlayers)
                {
                    if (player is PlayerAI ai)
                    {
                        ai.Hand.Add(CardStack.DrawCard());
                        ai.Hand.Add(CardStack.DrawCard());
                    }
                    else if (player is PokerProfile pp)
                    {
                        pp.Hand.Add(CardStack.DrawCard());
                        pp.Hand.Add(CardStack.DrawCard());
                    }
                }
            }

        }

    }
}
