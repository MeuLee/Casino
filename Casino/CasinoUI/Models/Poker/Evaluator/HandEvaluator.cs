using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker.Evaluator;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HandPlayer"></param>
        public HandEvaluator(Card[] HandPlayer)
        {
            _playerCards = HandPlayer.OrderByDescending(c => (int)c.Value).ToArray();
            _handStrength = new HandStrength(tailleMainTotal);
            InitNbOccurencesSuit();
            InitFlushList();
            InitNbOccurencesValue();
            InitStraightList();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        private void InitFlushList()
        {
            _flushList = _nbOccurencesSuit.FirstOrDefault(kvp => kvp.Value.Count >= 5).Value;
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        private Card GetCardMatchSuit(List<Card> cards)
        {
            if (_flushList == null) return cards[0];
            CardSuit suit = _flushList[0].Suit;
            Card temp = cards.FirstOrDefault(c => c.Suit == suit);
            return temp ?? cards[0];
        }

        /// <summary>
        /// 
        /// </summary>
        public HandStrength HandStrengths
        {
            get { return _handStrength; }
            set { _handStrength = value; }

        }

        /// <summary>
        /// 
        /// </summary>
        public Card[] Cards
        {
            get { return _playerCards; }
            set { _playerCards = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            else if (ThreeOfKind())
            {
                return Hand.ThreeKind;
            }
            else if (TwoPairs())
            {
                return Hand.TwoPairs;
            }
            else if (OnePair())
            {
                return Hand.OnePair;
            }
            AddCardHandStrength(_playerCards[4], _playerCards[3], _playerCards[2],
                    _playerCards[1], _playerCards[0]);
            return Hand.Nothing;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un RoyalFlush
        /// dans la main.
        /// </summary>
        /// <returns> True si un RoyalFlush present False si pas de RoyalFlush</returns>
        private bool RoyalFlush()
        {
            if (_straightList != null && 
                (int)_straightList[0].Value == 14 &&
                _flushList != null)
            {
                if (Enumerable.SequenceEqual(_straightList.OrderByDescending(t => t),
                    _flushList.OrderByDescending(t => t)))
                {
                    AddCardHandStrength(_straightList[4], _straightList[3], _straightList[2],
                     _straightList[1], _straightList[0]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un StraightFlush
        /// dans la main.
        /// </summary>
        /// <returns>True si un StraightFlush present False si pas de StraightFlush</returns>
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

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un FourOfKind
        /// dans la main.
        /// </summary>
        /// <returns>True si un FourOfKind present False si pas de FourOfKind</returns>
        private bool FourOfKind()
        {
            List<Card> fourKindTemp;
            Card highCardTemp = new Card(CardRank.Two, CardSuit.Clubs);
            foreach (var typeCard in _nbOccurencesValue)
            {
                if (typeCard.Value.Count == 4)
                {
                    fourKindTemp = typeCard.Value.ToList();
                    foreach (var playerCard in _playerCards)
                    {
                        if (playerCard.Value != typeCard.Key &&
                            playerCard.Value >= highCardTemp.Value)
                        {
                            highCardTemp = playerCard;
                        }
                    }
                    AddCardHandStrength(fourKindTemp[0], fourKindTemp[1],
                        fourKindTemp[2], fourKindTemp[3], highCardTemp);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un FullHouse
        /// dans la main.
        /// </summary>
        /// <returns>True si un FullHouse present False si pas de FullHouse</returns>
        private bool FullHouse()
        {
            List<Card> threeKindTemp = null;
            List<Card> twoKindTemp = null;
            foreach (var typeCard in _nbOccurencesValue)
            {
                if (typeCard.Value.Count == 3)
                {
                    threeKindTemp = typeCard.Value.ToList();
                }
                else if (typeCard.Value.Count == 2)
                {
                    twoKindTemp = typeCard.Value.ToList();
                }

                if (threeKindTemp != null && twoKindTemp != null)
                {
                    AddCardHandStrength(twoKindTemp[0], twoKindTemp[1],
                        threeKindTemp[0], threeKindTemp[1], threeKindTemp[2]);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un Flush
        /// dans la main.
        /// </summary>
        /// <returns>True si un Flush present False si pas de Flush</returns>
        private bool Flush()
        {
            if(_flushList != null)
            {
                AddCardHandStrength(_flushList[4], _flushList[3], _flushList[2],
                    _flushList[1], _flushList[0]);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un Straight
        /// dans la main.
        /// </summary>
        /// <returns>True si un Straight present False si pas de Straight</returns>
        private bool Straight()
        {
            if (_straightList != null)
            {
                AddCardHandStrength(_straightList[4], _straightList[3], _straightList[2],
                    _straightList[1], _straightList[0]);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un ThreeOfKind
        /// dans la main.
        /// </summary>
        /// <returns>True si un ThreeOfKind present False si pas de ThreeOfKind</returns>
        private bool ThreeOfKind()
        {
            List<Card> threeKindTemp;
            Card highCardTemp = new Card(CardRank.Two, CardSuit.Clubs);
            Card secondhighCardTemp = new Card(CardRank.Two, CardSuit.Diamonds);
            foreach (var typeCard in _nbOccurencesValue)
            {
                if(typeCard.Value.Count == 3)
                {
                    threeKindTemp = typeCard.Value.ToList();
                    foreach (var playerCard in _playerCards)
                    {
                        if (playerCard.Value != typeCard.Key &&
                            playerCard.Value >= highCardTemp.Value)
                        {
                            highCardTemp = playerCard;
                        }else if(playerCard.Value != typeCard.Key &&
                            playerCard.Value >= secondhighCardTemp.Value)
                        {
                            secondhighCardTemp = playerCard;
                        }
                    }
                    AddCardHandStrength(threeKindTemp[0], threeKindTemp[1],
                        threeKindTemp[2], secondhighCardTemp, highCardTemp);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un TwoPairs
        /// dans la main.
        /// </summary>
        /// <returns>True si un TwoPairs present False si pas de TwoPairs</returns>
        private bool TwoPairs()
        {
            List<Card> twoKindTemp = null;
            List<Card> secondTwoKindTemp = null;
            Card highCardTemp = new Card(CardRank.Two, CardSuit.Clubs);
            foreach (var typeCard in _nbOccurencesValue)
            {
                if (typeCard.Value.Count == 2)
                {
                    if(twoKindTemp == null)
                    {
                        twoKindTemp = typeCard.Value.ToList();
                    }
                    else if(secondTwoKindTemp == null)
                    {
                        secondTwoKindTemp = typeCard.Value.ToList();

                        foreach (var playerCard in _playerCards)
                        {
                            if (playerCard.Value != typeCard.Key &&
                                playerCard.Value != twoKindTemp[0].Value &&
                                playerCard.Value >= highCardTemp.Value)
                            {
                                highCardTemp = playerCard;
                            }
                        }
                    }                    

                    if (secondTwoKindTemp != null && twoKindTemp != null)
                    {
                        AddCardHandStrength(twoKindTemp[0], twoKindTemp[1],
                            secondTwoKindTemp[0], secondTwoKindTemp[1], highCardTemp);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un OnePair
        /// dans la main.
        /// </summary>
        /// <returns>True si un OnePair present False si pas de OnePair</returns>
        private bool OnePair()
        {
            //List<Card> onePairTemp;
            //Card highCardTemp = new Card(CardRank.Two, CardSuit.Clubs);
            //Card secondhighCardTemp = new Card(CardRank.Two, CardSuit.Diamonds);
            //Card thridhighCardTemp = new Card(CardRank.Two, CardSuit.Diamonds);
            //foreach (var typeCard in _nbOccurencesValue)
            //{
            //    if (typeCard.Value.Count == 2)
            //    {
            //        onePairTemp = typeCard.Value.ToList();

            //        foreach (var playerCard in _playerCards)
            //        {
            //            if (playerCard.Value != typeCard.Key &&
            //                playerCard.Value >= highCardTemp.Value)
            //            {
            //                highCardTemp = playerCard;
            //            }
            //            else if (playerCard.Value != typeCard.Key &&
            //               playerCard.Value >= secondhighCardTemp.Value)
            //            {
            //                secondhighCardTemp = playerCard;
            //            }else if(playerCard.Value != typeCard.Key &&
            //               playerCard.Value >= thridhighCardTemp.Value)
            //            {
            //                thridhighCardTemp = playerCard;
            //            }
            //        }
            //        AddCardHandStrength(onePairTemp[0], onePairTemp[1],
            //            thridhighCardTemp, secondhighCardTemp, highCardTemp);
            //        return true;
            //    }
            //}
            //return false;
            var pair = _nbOccurencesValue.FirstOrDefault(kvp => kvp.Value.Count == 2).Value;
            if (pair == null)
            {
                return false;
            }
            CardRank value = pair[0].Value;
            var highCards = _playerCards.Where(c => c.Value != value).OrderByDescending(c => c.Value).Take(3).ToList();
            AddCardHandStrength(pair[0], pair[1], highCards[2], highCards[1], highCards[0]);
            return true;
        }

        /// <summary>
        /// Méthode qui prend en parametre 5 carte pour les ajouter a la
        /// main gagnate. La dernier carte est aussi la plus fort soit la
        /// HighCard
        /// </summary>
        /// <param name="firstCard">Carte la moins forte</param>
        /// <param name="secondCard">Deuxieme carte la moins forte</param>
        /// <param name="thirdCard">Troisieme carte la moins forte</param>
        /// <param name="fourthCard">Quatrieme carte la moins forte</param>
        /// <param name="fifthCard">Cinquieme carte la moins forte 
        /// AKA la plus fort highcard</param>
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
