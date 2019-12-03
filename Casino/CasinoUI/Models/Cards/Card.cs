using System;
using System.Collections.Generic;
using System.Drawing;

namespace CasinoUI.Models.Cards
{
    public class Card : IComparable
    {

        protected double cardValue;
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

        public Card(CardRank value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public Card() { }



        public override string ToString()
        {
            return $"{(int)Value}{Suit.ToString()}";
        }

        public override bool Equals(object obj)
        {
            return obj is Card card &&
                   Value == card.Value &&
                   Suit == card.Suit &&
                   EqualityComparer<Bitmap>.Default.Equals(Image, card.Image);
        }

        public override int GetHashCode()
        {
            var hashCode = 1170886212;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Suit.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Bitmap>.Default.GetHashCode(Image);
            return hashCode;
        }

        public int CompareTo(object other)
        {
            if (other == null) return 1;
            Card otherCard = other as Card;
            if (otherCard != null)
                return this.cardValue.CompareTo(otherCard.cardValue);
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
}
