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
        private delegate bool ComboMethod();
        private Dictionary<Hand, ComboMethod> _comboMethods;
        private Card[] _playerCards;
        private HandStrength _handStrength;
        private Dictionary<CardSuit, List<Card>> _nbOccurencesSuit;
        private List<Card> _flushList = null;
        private Dictionary<CardRank, List<Card>> _nbOccurencesValue;
        private List<Card> _straightList = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HandPlayer"></param>
        public HandEvaluator(Card[] HandPlayer)
        {
            _playerCards = HandPlayer.OrderByDescending(c => (int)c.Value).ToArray();
            _handStrength = new HandStrength();
            InitNbOccurencesSuit();
            InitFlushList();
            InitNbOccurencesValue();
            InitStraightList();
            InitComboMethods();
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

        private void InitFlushList()
        {
            _flushList = _nbOccurencesSuit.FirstOrDefault(kvp => kvp.Value.Count >= 5).Value;
            _flushList?.RemoveRange(5, _flushList.Count - 5);
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
            _straightList?.RemoveRange(5, _straightList.Count - 5);
        }

        private void InitComboMethods()
        {
            _comboMethods = new Dictionary<Hand, ComboMethod>()
            {
                { Hand.RoyalFlush, RoyalFlush },
                { Hand.StraightFlush, StraightFlush },
                { Hand.FourKind, FourOfKind },
                { Hand.FullHouse, FullHouse },
                { Hand.Flush, Flush },
                { Hand.Straight, Straight },
                { Hand.ThreeKind, ThreeOfKind },
                { Hand.TwoPairs, TwoPairs },
                { Hand.OnePair, OnePair }
            };
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
        /// <returns></returns>
        public Hand EvaluateHand()
        {
            foreach (var combo in _comboMethods)
            {
                if (combo.Value())
                {
                    return combo.Key;
                }
            }
            AddCardHandStrength(_playerCards.ToList());
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
                if (Enumerable.SequenceEqual(_straightList?.OrderByDescending(t => t),
                                             _flushList?.OrderByDescending(t => t)))
                {
                    AddCardHandStrength(_straightList);
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
                    AddCardHandStrength(_straightList);
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
            return HandHasSameCards(4);
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un FullHouse
        /// dans la main.
        /// </summary>
        /// <returns>True si un FullHouse present False si pas de FullHouse</returns>
        private bool FullHouse()
        {
            List<Card> threeKindTemp = _nbOccurencesValue.FirstOrDefault(kvp => kvp.Value.Count == 3).Value;
            List<Card> twoKindTemp = _nbOccurencesValue.FirstOrDefault(kvp => kvp.Value.Count == 2).Value;
            if (threeKindTemp == null || twoKindTemp == null)
            {
                return false;
            }
            AddCardHandStrength(twoKindTemp, threeKindTemp);
            return true;
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
                AddCardHandStrength(_flushList);
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
                AddCardHandStrength(_straightList);
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
            return HandHasSameCards(3);
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un TwoPairs
        /// dans la main.
        /// </summary>
        /// <returns>True si un TwoPairs present False si pas de TwoPairs</returns>
        private bool TwoPairs()
        {
            var twoPairs = _nbOccurencesValue.Values.Where(l => l.Count == 2).Take(2).ToList();

            if (twoPairs == null || twoPairs.Count < 2)
            {
                return false;
            }
            var comboList = twoPairs[0].Concat(twoPairs[1]).ToList();
            var highCards = _playerCards.Except(comboList).ToList();
            AddCardHandStrength(comboList, highCards);
            return true;
        }

        /// <summary>
        /// Méthode qui va regarder les cartes pour trouver s'il y a un OnePair
        /// dans la main.
        /// </summary>
        /// <returns>True si un OnePair present False si pas de OnePair</returns>
        private bool OnePair()
        {
            return HandHasSameCards(2);
        }

        private bool HandHasSameCards(int nbSameValue)
        {
            var pair = _nbOccurencesValue.FirstOrDefault(kvp => kvp.Value.Count == nbSameValue).Value;
            if (pair == null)
            {
                return false;
            }
            CardRank value = pair[0].Value;
            var highCards = _playerCards.Except(pair).ToList();
            AddCardHandStrength(pair, highCards);
            return true;
        }

        /// <summary>
        /// Méthode qui prend en parametre 5 carte pour les ajouter a la
        /// main gagnate. La dernier carte est aussi la plus fort soit la
        /// HighCard
        /// </summary>
        /// <param name="comboCards">
        /// AKA la plus fort highcard</param>
        private void AddCardHandStrength(List<Card> comboCards, List<Card> highCards = null)
        {
            comboCards = comboCards.OrderByDescending(c => c.Value).ToList();
            if (highCards == null)
            {
                _handStrength.HandPlayer = comboCards.Take(5).ToArray();
                _handStrength.HighCard = comboCards[0];
            }
            else
            {
                _handStrength.HandPlayer = comboCards.Concat(highCards.Take(5 - comboCards.Count)).ToArray();
                _handStrength.HighCard = highCards.OrderByDescending(c => c.Value).ToList()[0];
            }            
        }
    }
}