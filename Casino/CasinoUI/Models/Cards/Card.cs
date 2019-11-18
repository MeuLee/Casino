using System.Drawing;

namespace CasinoUI.Models.Cards
{
    public class Card
    {
        public enum CardRank
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }
        public enum CardSuit
        {
            Diamonds,
            Clubs,
            Hearts,
            Spades
        }

        public CardRank Value { get; private set; }
        public CardSuit Suit { get; private set; }

        public Bitmap Image { get; private set; }

        public Card(CardRank value, CardSuit suit, Bitmap image)
        {
            Value = value;
            Suit = suit;
            Image = image;
        }
    }
}
