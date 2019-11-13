using CasinoUI.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker
{
    //Auteur : Damien Dussurget
    // Fortement inspirer de la video suivante : https://www.youtube.com/watch?v=gkJKqVo30LA
    public enum Hand
    {
        Nothing = 1,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush,
        RoyalFlush
    }

    public struct HandStrength
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }

    class HandEvaluator : Card
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] cards;
        private HandStrength handStrength;

        public HandEvaluator(Card[] sortedHand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = new Card[7];
            handStrength = new HandStrength();
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
            sortCards();
            getNumberOfSuit();
            if (RoyalFlush())
            {
                return Hand.RoyalFlush;
            }
            else if(StraightFlush())
            {
                return Hand.RoyalFlush;
            }
            else if(FourOfKind())
            {
                return Hand.StraightFlush;
            }
            else if(FullHouse())
            {
                return Hand.FourKind;
            }
            else if(Flush())
            {
                return Hand.Flush;
            }
            else if(Straight())
            {
                return Hand.Straight;
            }
            else if(ThreeKind())
            {
                return Hand.ThreeKind;
            }
            else if(TwoPairs())
            {
                return Hand.TwoPairs;
            }
            else if(OnePair())
            {
                return Hand.OnePair;
            }

            handStrength.HighCard = (int)cards[6].Value;
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
                if (heartsSum == 5)
                {
                    handStrength.Total = (int)Hand.RoyalFlush + (int)cards[6].Value;
                    return true;
                }
                else if (diamondSum == 5)
                {
                    handStrength.Total = (int)Hand.RoyalFlush + (int)cards[6].Value;
                    return true;
                }
                else if (clubSum == 5)
                {
                    handStrength.Total = (int)Hand.RoyalFlush + (int)cards[6].Value;
                    return true;
                }
                else if (spadesSum == 5)
                {
                    handStrength.Total = (int)Hand.RoyalFlush + (int)cards[6].Value;
                    return true;
                }
            }
            return false;

        }

        private bool StraightFlush()
        {
            bool checkCard = false;
            foreach (var suiteCards in cards)
            {
                if (heartsSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Hearts);

                }
                else if (diamondSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Diamonds);

                }
                else if (clubSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Clubs);

                }
                else if (spadesSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Spades);

                }
            }
            return checkCard;

        }

        private bool FourOfKind()
        {
            // premiere 4 cards, ajouter la valeur des 4 cards et la derniere est la plus haute
            if (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[0].Value == cards[3].Value)
            {
                handStrength.Total = (int)Hand.FourKind + (int)cards[0].Value;
                return true;
            }
            else if(cards[3].Value == cards[4].Value && cards[3].Value == cards[5].Value && cards[3].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.FourKind + (int)cards[3].Value;
                return true;
            }

            return false;            
        }

        private bool FullHouse()
        {
            if (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[0].Value;
                return true;
            }
            else if
              (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[4].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[0].Value;
                return true;
            }
            else if
              (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[3].Value == cards[4].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[0].Value;
                return true;
            }
            else if
              (cards[1].Value == cards[2].Value && cards[1].Value == cards[3].Value && cards[4].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[1].Value;
                return true;
            }
            else if
              (cards[1].Value == cards[2].Value && cards[1].Value == cards[3].Value && cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[1].Value;
                return true;
            }
            else if
              (cards[2].Value == cards[3].Value && cards[2].Value == cards[4].Value && cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[2].Value;
                return true;
            }
            else if
              (cards[2].Value == cards[3].Value && cards[2].Value == cards[4].Value && cards[0].Value == cards[1].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[2].Value;
                return true;
            }
            else if
              (cards[3].Value == cards[4].Value && cards[3].Value == cards[5].Value && cards[0].Value == cards[1].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[3].Value;
                return true;
            }
            else if
              (cards[4].Value == cards[5].Value && cards[4].Value == cards[6].Value && cards[0].Value == cards[1].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[4].Value;
                return true;
            }
            else if
              (cards[4].Value == cards[5].Value && cards[4].Value == cards[6].Value && cards[1].Value == cards[2].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[4].Value;
                return true;
            }
            else if
              (cards[4].Value == cards[5].Value && cards[4].Value == cards[6].Value && cards[2].Value == cards[3].Value)
            {
                handStrength.Total = (int)Hand.FullHouse + (int)cards[4].Value;
                return true;
            }
            return false;
        }

        private bool Flush()
        {
            bool checkCard = false;
            foreach(var suiteCards in cards)
            {
                if(heartsSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Hearts);

                }else if (diamondSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Diamonds);

                }
                else if (clubSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Clubs);

                }
                else if (spadesSum == 5)
                {
                    checkCard = checkCardsFlush(suiteCards, Card.CardSuit.Spades);

                }
            }
            return checkCard;

        }

        private bool Straight()
        {
            if(cards[0].Value + 1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value)
            {
                handStrength.Total = (int)Hand.Straight + (int)cards[4].Value;
                return true;

            }   else if
                (cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value &&
                cards[4].Value + 1 == cards[5].Value)
            {
                handStrength.Total = (int)Hand.Straight + (int)cards[5].Value;
                return true;
            }
            else if (cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value &&
                cards[4].Value + 1 == cards[5].Value &&
                cards[5].Value + 1 == cards[6].Value)
            {
                handStrength.Total = (int)Hand.Straight + (int)cards[6].Value;
                return true;
            }

            return false;

        }

        private bool ThreeKind()
        {
            if (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value)
            {
                handStrength.Total = (int)Hand.ThreeKind + (int)cards[0].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value && cards[1].Value == cards[3].Value)
            {
                handStrength.Total = (int)Hand.ThreeKind + (int)cards[1].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value && cards[2].Value == cards[4].Value)
            {
                handStrength.Total = (int)Hand.ThreeKind + (int)cards[2].Value;
                return true;
            }
            else if (cards[3].Value == cards[4].Value && cards[3].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.ThreeKind + (int)cards[3].Value;
                return true;
            }
            else if(cards[4].Value == cards[5].Value && cards[4].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.ThreeKind + (int)cards[4].Value;
                return true;
            }

            return false;

        }

        private bool TwoPairs()
        {   if (cards[0].Value == cards[1].Value &&
                cards[2].Value == cards[3].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[3].Value;
                return true;
            }
            else if (cards[0].Value == cards[1].Value &&
                cards[3].Value == cards[4].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[4].Value;
                return true;
            }
            else if (cards[0].Value == cards[1].Value &&
                cards[4].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[5].Value;
                return true;
            }
            else if (cards[0].Value == cards[1].Value &&
                cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[6].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value &&
                cards[3].Value == cards[4].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[4].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value &&
                cards[4].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[5].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value &&
                cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[6].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value &&
                cards[4].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[5].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value &&
                cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[6].Value;
                return true;
            }
            else if (cards[3].Value == cards[4].Value &&
                cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.TwoPairs + (int)cards[6].Value;
                return true;
            }

            return false;
        }

        private bool OnePair()
        {
            if (cards[0].Value == cards[1].Value)
            {
                handStrength.Total = (int)Hand.OnePair + (int)cards[0].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value)
            {
                handStrength.Total = (int)Hand.OnePair + (int)cards[1].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value)
            {
                handStrength.Total = (int)Hand.OnePair + (int)cards[2].Value;
                return true;
            }
            else if (cards[3].Value == cards[4].Value)
            {
                handStrength.Total = (int)Hand.OnePair + (int)cards[3].Value;
                return true;
            }
            else if (cards[4].Value == cards[5].Value)
            {
                handStrength.Total = (int)Hand.OnePair + (int)cards[4].Value;
                return true;
            }
            else if (cards[5].Value == cards[6].Value)
            {
                handStrength.Total = (int)Hand.OnePair + (int)cards[5].Value;
                return true;
            }

            return false;

        }

        private void sortCards()
        {
            var queryPlayer = from hand in Cards
                              orderby hand.Value
                              select hand;            
        }

        private bool checkCardsFlush(Card suiteCards, CardSuit suit)
        {
            if (suiteCards.Suit == suit)
            {
                if (handStrength.Total < (int)suiteCards.Value)
                    handStrength.Total = (int)suiteCards.Value + (int)Hand.Straight;
                return true;
            }
            return false;
        }

        private bool checkCardsStraightFlush(Card suiteCards, CardSuit suit)
        {
            if (suiteCards.Suit == suit)
            {
                if (cards[0].Value + 1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value)
                {
                    if (handStrength.Total < (int)suiteCards.Value)
                        handStrength.Total = (int)suiteCards.Value + (int)Hand.StraightFlush;
                    return true;
                }
                else if
                (cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value &&
                cards[4].Value + 1 == cards[5].Value)
                {
                    if (handStrength.Total < (int)suiteCards.Value)
                        handStrength.Total = (int)suiteCards.Value + (int)Hand.StraightFlush;
                    return true;
                }
                else if (cards[2].Value + 1 == cards[3].Value &&
                    cards[3].Value + 1 == cards[4].Value &&
                    cards[4].Value + 1 == cards[5].Value &&
                    cards[5].Value + 1 == cards[6].Value)
                {
                    if (handStrength.Total < (int)suiteCards.Value)
                        handStrength.Total = (int)suiteCards.Value + (int)Hand.StraightFlush;
                    return true;
                }
                
            }
            return false;
        }

        private bool isRoyal()
        {
            if(cards[6].Value == Card.CardRank.Ace &&
                cards[5].Value == Card.CardRank.King &&
                cards[4].Value == Card.CardRank.Queen &&
                cards[3].Value == Card.CardRank.Jack &&
                cards[2].Value == Card.CardRank.Ten)
            {
                return true;
            }
            return false;
        }
    }
}
