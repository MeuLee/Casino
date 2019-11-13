using System.Drawing;

namespace CasinoUI.Models.Cards
{
    public class Card
    {
        public enum CardRank
        {
            Two = 2,
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
            King,
            Ace
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

        public Card()
        {

        }
    }
}
