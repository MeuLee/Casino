using CasinoUI.Models.Cards;

namespace CasinoUI.Models.Poker.Evaluator
{
    public class HandStrength
    {
        public HandStrength()
        {
            HighCard = null;
            Total = 0;
        }

        public Card[] HandPlayer { get; set; }
        private int Total { get; set; }
        public Card HighCard { get; set; }

        public int CalculerTotal()
        {
            return TrouverTotal();
        }

        private int TrouverTotal()
        {
            foreach (Card cardMain in HandPlayer)
            {
                Total += (int)cardMain.Value;
            }
            return Total;
        }
    }
}
