using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker.Evaluator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.Models.Poker
{
    //Auteur : Damien Dussurget, Mathieu Larivée
    //Fortement inspirer de la video suivante : https://www.youtube.com/watch?v=gkJKqVo30LA    

    public class HandEvaluator : Card
    {
        private static readonly int tailleMainTotal = 5;
        private Card[] _playerCards;
        private HandStrength _handStrength;
        Dictionary<CardSuit, List<Card>> _nbOccurencesSuit;
        List<Card> _flushList = null;
        Dictionary<CardRank, List<Card>> _nbOccurencesValue;
        List<Card> _straightList = null;

        public HandEvaluator(Card[] HandPlayer)
        {
            _playerCards = HandPlayer.OrderByDescending(c => (int)c.Value).ToArray();
            _handStrength = new HandStrength(tailleMainTotal);
            InitNbOccurencesSuit();
            InitFlushList();
            InitNbOccurencesValue();
            InitStraightList();
        }

        private void InitNbOccurencesSuit()
        {
            _nbOccurencesSuit = new Dictionary<CardSuit, List<Card>>();
            foreach (var card in _playerCards)
            {
                if (!_nbOccurencesSuit.ContainsKey(card.Suit))
                {
                    _nbOccurencesSuit.Add(card.Suit, new List<Card>() { card });
                }
                else
                {
                    _nbOccurencesSuit[card.Suit].Add(card);
                }
            }
        }

        private void InitFlushList()
        {
            _flushList = _nbOccurencesSuit.FirstOrDefault(kvp => kvp.Value.Count >= 5).Value;
        }

        private void InitNbOccurencesValue()
        {
            _nbOccurencesValue = new Dictionary<CardRank, List<Card>>();
            foreach (var card in _playerCards)
            {
                if (!_nbOccurencesValue.ContainsKey(card.Value))
                {
                    _nbOccurencesValue.Add(card.Value, new List<Card>() { card });
                }
                else
                {
                    _nbOccurencesValue[card.Value].Add(card);
                }
            }
        }

        private void InitStraightList()
        {
            var tempDic = _nbOccurencesValue.ToList();
            var straights = new List<List<Card>>()
            {
                new List<Card>() { GetCardMatchSuit(tempDic[0].Value) }
            };
            int listIndex = 0;

            for (int i = 1; i < tempDic.Count; i++)
            {
                var kvp = tempDic[i];
                int previous = (int)tempDic[i - 1].Key,
                    current = (int)kvp.Key;
                if (previous != current + 1)
                {
                    listIndex++;
                    straights.Add(new List<Card>());
                }
                straights[listIndex].Add(GetCardMatchSuit(kvp.Value));
            }

            _straightList = straights.OrderByDescending(l => l.Count)
                                     .FirstOrDefault(l => l.Count >= 5);
        }

        private Card GetCardMatchSuit(List<Card> cards)
        {
            if (_flushList == null) return cards[0];
            CardSuit suit = _flushList[0].Suit;
            Card temp = cards.FirstOrDefault(c => c.Suit == suit);
            return temp ?? cards[0];
        }

        public HandStrength HandStrengths
        {
            get { return _handStrength; }
            set { _handStrength = value; }

        }

        public Card[] Cards
        {
            get { return _playerCards; }
            set { _playerCards = value; }
        }

        public Hand EvaluateHand()
        {
            if (RoyalFlush())
            {
                return Hand.RoyalFlush;
            }
            else if (StraightFlush())
            {
                return Hand.StraightFlush;
            }
            else if (FourOfKind())
            {
                return Hand.FourKind;
            }
            //    else if (FullHouse())
            //    {
            //        return Hand.FullHouse;
            //    }
            //    else if (Flush())
            //    {
            //        return Hand.Flush;
            //    }
            //    else if (Straight())
            //    {
            //        return Hand.Straight;
            //    }
            //    else if (ThreeOfKind())
            //    {
            //        return Hand.ThreeKind;
            //    }
            //    else if (TwoPairs())
            //    {
            //        return Hand.TwoPairs;
            //    }
            //    else if (OnePair())
            //    {
            //        return Hand.OnePair;
            //    }
            AddCardHandStrength(_playerCards[4], _playerCards[3], _playerCards[2],
                    _playerCards[1], _playerCards[0]);
            return Hand.Nothing;
        }

        private bool RoyalFlush()
        {
            if (_straightList != null && (int)_straightList[0].Value == 14)
            {
                int countSuit = 0;
                Card.CardSuit suiteRoyal = _straightList[0].Suit;
                foreach (Card carteRoyalFlush in _straightList){
                    if(carteRoyalFlush.Suit == suiteRoyal)
                    {
                        countSuit++;
                    }                   
                }
                if(countSuit == 5)
                {
                    AddCardHandStrength(_straightList[4], _straightList[3], _straightList[2],
                    _straightList[1], _straightList[0]);
                    return true;
                }
            }                        
            return false;
        }

        private bool StraightFlush()
        {
            if (_straightList != null && _flushList != null)
            {
                if (Enumerable.SequenceEqual(_straightList.OrderByDescending(t => t), _flushList.OrderByDescending(t => t)))
                {
                    AddCardHandStrength(_straightList[4], _straightList[3], _straightList[2],
                     _straightList[1], _straightList[0]);
                    return true;
                }                
            }
            return false;
        }

        private bool FourOfKind()
        {
            List<Card> mainGagnanteTemp = new List<Card>(5);
            Card highCardTemp = null; 
            foreach(var typeCard in _nbOccurencesValue)
            {
                if(typeCard.Value.Count == 4)
                {
                    mainGagnanteTemp = typeCard.Value.ToList();
                }

                if(typeCard.Key > highCardTemp.Value)
                {
                    highCardTemp = typeCard.Value[0];
                }
            }

            if(highCardTemp != mainGagnanteTemp[0])
            {
                AddCardHandStrength(mainGagnanteTemp[0], mainGagnanteTemp[0], mainGagnanteTemp[0], mainGagnanteTemp[0], highCardTemp);
                return true;
            }
            else
            {
                foreach(Card cards in _playerCards)
                {
                    if(cards != mainGagnanteTemp[0])
                    {
                        if((int)cards.Value < (int)mainGagnanteTemp[4].Value)
                        {
                            mainGagnanteTemp[4] = cards;
                        }
                    }
                }
                AddCardHandStrength(mainGagnanteTemp[4], mainGagnanteTemp[3], mainGagnanteTemp[2], mainGagnanteTemp[1], mainGagnanteTemp[0]);
                return true;
            }
            return false;
        }

        //        private bool FullHouse()
        //        {
        //            return HandHasPairs(1, 2) && HandHasPairs(1, 3);
        //        }

        //        private bool Flush()
        //        {
        //            foreach (var suiteCards in _playerCards)
        //            {
        //                if (heartsSum >= 5)
        //                {
        //                    CheckCardsFlush(Card.CardSuit.Hearts);
        //                    return true;

        //                }
        //                else if (diamondSum >= 5)
        //                {
        //                    CheckCardsFlush(Card.CardSuit.Diamonds);
        //                    return true;

        //                }
        //                else if (clubSum >= 5)
        //                {
        //                    CheckCardsFlush(Card.CardSuit.Clubs);
        //                    return true;

        //                }
        //                else if (spadesSum >= 5)
        //                {
        //                    CheckCardsFlush(Card.CardSuit.Spades);
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }

        //        private bool Straight()
        //        {
        //            if (_playerCards[0].Value + 1 == _playerCards[1].Value &&
        //                _playerCards[1].Value + 1 == _playerCards[2].Value &&
        //                _playerCards[2].Value + 1 == _playerCards[3].Value &&
        //                _playerCards[3].Value + 1 == _playerCards[4].Value)
        //            {
        //                AddCardHandStrength(_playerCards[0], _playerCards[1], _playerCards[2], _playerCards[3], _playerCards[4]);
        //                return true;

        //            }
        //            else if
        //            (_playerCards[1].Value + 1 == _playerCards[2].Value &&
        //            _playerCards[2].Value + 1 == _playerCards[3].Value &&
        //            _playerCards[3].Value + 1 == _playerCards[4].Value &&
        //            _playerCards[4].Value + 1 == _playerCards[5].Value)
        //            {
        //                AddCardHandStrength(_playerCards[1], _playerCards[2], _playerCards[3], _playerCards[4], _playerCards[5]);
        //                return true;
        //            }
        //            else if (_playerCards[2].Value + 1 == _playerCards[3].Value &&
        //                _playerCards[3].Value + 1 == _playerCards[4].Value &&
        //                _playerCards[4].Value + 1 == _playerCards[5].Value &&
        //                _playerCards[5].Value + 1 == _playerCards[6].Value)
        //            {
        //                AddCardHandStrength(_playerCards[2], _playerCards[3], _playerCards[4], _playerCards[5], _playerCards[6]);
        //                return true;
        //            }

        //            return false;

        //        }

        //        private bool HandHasPairs(int numPairs, int nbSameValue)
        //        {
        //            Dictionary<int, int> occurences = new Dictionary<int, int>();
        //            foreach (var card in _playerCards)
        //            {
        //                if (!occurences.ContainsKey((int)card.Value))
        //                {
        //                    occurences.Add((int)card.Value, 1);
        //                }
        //                else
        //                {
        //                    occurences[(int)card.Value]++;
        //                }
        //            }
        //            if (occurences.Values.Count(v => v >= nbSameValue) >= numPairs)
        //            {
        //                //handStrength.Total += occurences.Last(v => v.Value >= numPairs).Key * nbSameValue;
        //                //AddCardHandStrength(cards[3], cards[3], cards[3], cards[3], cards[cards.Length - 1]);
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        private void CheckCardsFlush(CardSuit suit)
        //        {
        //            Card[] listeCarteSuite = new Card[5];
        //            int index = listeCarteSuite.Length - 1;
        //            for (int i = _playerCards.Length - 1; i >= 0; i--)
        //            {
        //                if (_playerCards[i].Suit == suit)
        //                {
        //                    if (index >= 0)
        //                    {
        //                        listeCarteSuite[index] = _playerCards[i];
        //                        index--;
        //                    }

        //                }
        //            }
        //            AddCardHandStrength(listeCarteSuite[listeCarteSuite.Length - 5], listeCarteSuite[listeCarteSuite.Length - 4],
        //                listeCarteSuite[listeCarteSuite.Length - 3], listeCarteSuite[listeCarteSuite.Length - 2],
        //                listeCarteSuite[listeCarteSuite.Length - 1]);
        //        }

        //        private bool isRoyal()
        //        {
        //            if (_playerCards[_playerCards.Length - 1].Value == Card.CardRank.Ace &&
        //                _playerCards[_playerCards.Length - 2].Value == Card.CardRank.King &&
        //                _playerCards[_playerCards.Length - 3].Value == Card.CardRank.Queen &&
        //                _playerCards[_playerCards.Length - 4].Value == Card.CardRank.Jack &&
        //                _playerCards[_playerCards.Length - 5].Value == Card.CardRank.Ten)
        //            {
        //                return true;
        //            }
        //            return false;
        //        }

        private void AddCardHandStrength(Card firstCard, Card secondCard, Card thirdCard,
            Card fourthCard, Card fifthCard)
        {
            _handStrength.HandPlayer[0] = firstCard;
            _handStrength.HandPlayer[1] = secondCard;
            _handStrength.HandPlayer[2] = thirdCard;
            _handStrength.HandPlayer[3] = fourthCard;
            _handStrength.HandPlayer[4] = fifthCard;
            _handStrength.HighCard = fifthCard;
        }
    }
}
