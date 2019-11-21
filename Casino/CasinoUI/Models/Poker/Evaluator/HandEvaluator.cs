using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker.Evaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker
{
    //Auteur : Damien Dussurget
    //Fortement inspirer de la video suivante : https://www.youtube.com/watch?v=gkJKqVo30LA    

    public class HandEvaluator : Card
    {
        static private int tailleMainTotal = 5;
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] cards;
        private HandStrength handStrength;

        public HandEvaluator(Card[] HandPlayer)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = HandPlayer;
            handStrength = new HandStrength(tailleMainTotal);
        }

        public HandStrength HandStrengths
        {
            get { return handStrength; }
            set { handStrength = value; }

        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
                cards[5] = value[5];
                cards[6] = value[6];
            }
        }

        public Hand EvaluateHand()
        {
            SortCards();
            getNumberOfSuit();
            if (RoyalFlush())
            {
                return Hand.RoyalFlush;
            }
            else if (StraightFlush())
            {
                return Hand.StraightFlush;
            }
            else if (HandHasPairs(1, 4))
            {
                return Hand.FourKind;
            }
            else if (FullHouse())
            {
                return Hand.FullHouse;
            }
            else if (Flush())
            {
                return Hand.Flush;
            }
            else if (Straight())
            {
                return Hand.Straight;
            }
            else if (HandHasPairs(1, 3))
            {
                return Hand.ThreeKind;
            }
            else if (HandHasPairs(2, 2))
            {
                return Hand.TwoPairs;
            }
            else if (HandHasPairs(1, 2))
            {
                return Hand.OnePair;
            }

            handStrength.HighCard = cards[cards.Length - 1];
            return Hand.Nothing;
        }
        private void getNumberOfSuit()
        {
            foreach (var element in cards)
            {
                switch (element.Suit)
                {
                    case Card.CardSuit.Hearts:
                        heartsSum++;
                        break;
                    case Card.CardSuit.Diamonds:
                        diamondSum++;
                        break;
                    case Card.CardSuit.Clubs:
                        clubSum++;
                        break;
                    case Card.CardSuit.Spades:
                        spadesSum++;
                        break;
                }
            }
        }

        private bool RoyalFlush()
        {
            if (isRoyal())
            {
                if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
                {
                    AddCardHandStrength(cards[cards.Length - 5], cards[cards.Length - 4], cards[cards.Length - 3],
                        cards[cards.Length - 2], cards[cards.Length - 1]);
                    return true;
                }
            }
            return false;
        }

        private bool StraightFlush()
        {
            if (cards[0].Value + 1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                    cards[3].Value + 1 == cards[4].Value)
            {                
                if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
                {
                    AddCardHandStrength(cards[0], cards[1], cards[2], cards[3], cards[4]);
                    return true;
                }
            }
            else if
            (cards[1].Value + 1 == cards[2].Value &&
            cards[2].Value + 1 == cards[3].Value &&
            cards[3].Value + 1 == cards[4].Value &&
            cards[4].Value + 1 == cards[5].Value)
            {
                if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
                {
                    AddCardHandStrength(cards[1], cards[2], cards[3], cards[4], cards[5]);
                    return true;
                }
            }
            else if (cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value &&
                cards[4].Value + 1 == cards[5].Value &&
                cards[5].Value + 1 == cards[6].Value)
            {
                if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
                {
                    AddCardHandStrength(cards[2], cards[3], cards[4], cards[5], cards[6]);
                    return true;
                }
            }
            return false;
        }

        private bool FourOfKind()
        {
            // premiere 4 cards, ajouter la valeur des 4 cards et la derniere est la plus haute
            if (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[0].Value == cards[3].Value)
            {
                AddCardHandStrength(cards[0], cards[0], cards[0], cards[0], cards[0]);
                return true;
            }
            else if (cards[3].Value == cards[4].Value && cards[3].Value == cards[5].Value && cards[3].Value == cards[6].Value)
            {
                AddCardHandStrength(cards[3], cards[3], cards[3], cards[3], cards[3]);
                return true;
            }

            return false;
        }

        private bool FullHouse()
        {
            return HandHasPairs(1, 2) && HandHasPairs(1, 3);
        }

        private bool Flush()
        {
            foreach (var suiteCards in cards)
            {
                if (heartsSum >= 5)
                {
                    CheckCardsFlush(Card.CardSuit.Hearts);
                    return true;

                }
                else if (diamondSum >= 5)
                {
                    CheckCardsFlush(Card.CardSuit.Diamonds);
                    return true;

                }
                else if (clubSum >= 5)
                {
                    CheckCardsFlush(Card.CardSuit.Clubs);
                    return true;

                }
                else if (spadesSum >= 5)
                {
                    CheckCardsFlush(Card.CardSuit.Spades);
                    return true;
                }
            }
            return false;
        }

        private bool Straight()
        {
            if (cards[0].Value + 1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value)
            {
                AddCardHandStrength(cards[0], cards[1], cards[2], cards[3], cards[4]);
                return true;

            }
            else if
            (cards[1].Value + 1 == cards[2].Value &&
            cards[2].Value + 1 == cards[3].Value &&
            cards[3].Value + 1 == cards[4].Value &&
            cards[4].Value + 1 == cards[5].Value)
            {
                AddCardHandStrength(cards[1], cards[2], cards[3], cards[4], cards[5]);
                return true;
            }
            else if (cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value &&
                cards[4].Value + 1 == cards[5].Value &&
                cards[5].Value + 1 == cards[6].Value)
            {
                AddCardHandStrength(cards[2], cards[3], cards[4], cards[5], cards[6]);
                return true;
            }

            return false;

        }

        private bool HandHasPairs(int numPairs, int nbSameValue)
        {
            Dictionary<int, int> occurences = new Dictionary<int, int>();
            foreach (var card in cards)
            {
                if (!occurences.ContainsKey((int)card.Value))
                {
                    occurences.Add((int)card.Value, 1);
                }
                else
                {
                    occurences[(int)card.Value]++;
                }
            }
            if (occurences.Values.Count(v => v >= nbSameValue) >= numPairs)
            {
                //handStrength.Total += occurences.Last(v => v.Value >= numPairs).Key * nbSameValue;
                //AddCardHandStrength(cards[3], cards[3], cards[3], cards[3], cards[cards.Length - 1]);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SortCards()
        {
            cards = cards.OrderBy(c => (int)c.Value).ToArray();
        }

        private void CheckCardsFlush(CardSuit suit)
        {
            Card[] listeCarteSuite = new Card[5];
            int index = listeCarteSuite.Length - 1;
            for (int i = cards.Length - 1; i >= 0; i--)
            {
                if (cards[i].Suit == suit)
                {
                    if (index >= 0)
                    {
                        listeCarteSuite[index] = cards[i];
                        index--;
                    }

                }
            }
            AddCardHandStrength(listeCarteSuite[listeCarteSuite.Length - 5], listeCarteSuite[listeCarteSuite.Length - 4],
                listeCarteSuite[listeCarteSuite.Length - 3], listeCarteSuite[listeCarteSuite.Length - 2],
                listeCarteSuite[listeCarteSuite.Length - 1]);
        }

        private bool isRoyal()
        {
            if (cards[cards.Length - 1].Value == Card.CardRank.Ace &&
                cards[cards.Length - 2].Value == Card.CardRank.King &&
                cards[cards.Length - 3].Value == Card.CardRank.Queen &&
                cards[cards.Length - 4].Value == Card.CardRank.Jack &&
                cards[cards.Length - 5].Value == Card.CardRank.Ten)
            {
                return true;
            }
            return false;
        }

        private void AddCardHandStrength(Card firstCard, Card secondCard, Card thirdCard,
            Card fourthCard, Card fifthCard)
        {
            handStrength.HandPlayer[0] = firstCard;
            handStrength.HandPlayer[1] = secondCard;
            handStrength.HandPlayer[2] = thirdCard;
            handStrength.HandPlayer[3] = fourthCard;
            handStrength.HandPlayer[4] = fifthCard;
            handStrength.HighCard = fifthCard;
        }
    }
}
