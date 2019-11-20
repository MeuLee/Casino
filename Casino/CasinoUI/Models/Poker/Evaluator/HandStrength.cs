using CasinoUI.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker.Evaluator
{
    public struct HandStrength
    {
        public HandStrength(int tailleMain)
        {
            MainJoueur = new Card[tailleMain];
            HighCard = null;
            Total = 0;
        }

        public Card[] MainJoueur { get; set; }
        private int Total { get; set; }
        public Card HighCard { get; set; }

        public int CalculerTotal()
        {
            return TrouverTotal();
        }

        private int TrouverTotal()
        {
            foreach (Card cardMain in MainJoueur)
            {
                Total += (int)cardMain.Value;
            }
            return Total;
        }
    }
}
