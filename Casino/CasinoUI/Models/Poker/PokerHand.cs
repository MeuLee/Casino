using CasinoUI.Model.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoUI.Model.Poker
{
    public class PokerHand
    {
        private List<Card> _listCardInGame;
        private List<Card> _listValue;
        private List<int> _listTempCombo;

        private List<Tuple<int, int>> _comboValuePoss;
        private Card.CardSuit _flushCombo;

        private bool _isStraight = false;
        private bool _isFlush = false;

        public PokerHand(List<Card> listCardInGame)
        {
            this._listCardInGame = listCardInGame;
            _listTempCombo = new List<int>();
            _listValue = new List<Card>();
            _comboValuePoss = new List<Tuple<int, int>>();
            _flushCombo = Card.CardSuit.Diamonds;
        }

        public List<Card> ListCardInGame {
            get { return _listCardInGame; }
            set { this._listCardInGame = value; }
        }
        public List<Card> ListValue {
            get { return _listValue; }
        }

        public Card.CardSuit FlushCombo
        {
            get { return _flushCombo; }
            set { this._flushCombo = value; }
        }

        public List<Tuple<int, int>> ComboValuePoss {
            get { return _comboValuePoss; }
            set { this._comboValuePoss = value; }
        }

        public bool IsStraight { 
            get { return _isStraight; }
            set { this._isStraight = value; }
        }

        public bool IsFlush
        {
            get { return _isFlush; }
            set { this._isFlush = value; }
        }
        private void CheckCombo()
        {
            //Si seulement straight ou flush calcul hignestCombo
            //
        }
        public void CreateAllCombo()
        {
            DescendValueList();
            CreateListValue();
            CreateSameKindCombo();
            CreateStraightCombo();

            DescendSuitList();
            CreateFlushCombo();
        }

        private void CreateFlushCombo()
        {
            isFlushCombo();
            if (_isFlush)
            {
                ComboFlush();
            }
        }

        private void CreateStraightCombo()
        {
            isStraightCombo();
            if (_isStraight)
            {
                StraightCombo();
            }
        }


        private void ComboFlush()
        {
            double nbr = _listCardInGame.Count / 2.0;
            int middle = (int)Math.Ceiling(nbr);
            if (middle % 2 == 0)
            {
                _flushCombo = _listCardInGame[middle].Suit;
            }
            else
            {
                _flushCombo = _listCardInGame[middle-1].Suit;
            }
        }

        private void StraightCombo()
        {
            CreateList();
            AdjustCards();
        }

        private void AdjustCards()
        {
            foreach (Card card in _listValue)
            {
                if (_listTempCombo.Contains((int)card.Value))
                {
                    _listTempCombo.Remove((int)card.Value);
                }
            }

            DetermineStraight();
        }

        private void DetermineStraight()
        {
            if (_listValue.Count == 4)
            {
                FourCardStraightPoss();
            }
            else if (_listValue.Count == 3)
            {
                ThreeCardsStraighPoss();
            }
            else
            {
                FiveCardsStraightPoss();
            }
        }

        private void FiveCardsStraightPoss()
        {
            switch (SpaceBetweenCard())
            {
                case 2:
                    if (ThreeCardEnd() && _listTempCombo[0] == 14)
                    {
                        _listTempCombo.RemoveAt(0);
                    }
                    break;
                case 3:
                    if (ThreeCardEnd())
                    {
                        int compt = 1;

                        for (int i = 0; i < 2; i++)
                        {
                            int itemStraight = (int)_listValue[_listValue.Count - 1].Value - compt;

                            if (itemStraight < 15 && itemStraight > 1 && !_listTempCombo.Contains(itemStraight))
                            {
                                _listTempCombo.Add(itemStraight);
                            }
                            compt++;
                        }
                    }
                    break;
            }
        }

        private void ThreeCardsStraighPoss()
        {
            if (_listTempCombo.Count > 3)
            {

                switch (SpaceBetweenCard())
                {
                    case 2:
                        _listTempCombo.RemoveAt(0);
                        break;
                    case 3:
                        _listTempCombo.RemoveRange(0, 2);
                        break;
                }
            }
            else if (_listTempCombo.Count > 2)
            {
                switch (SpaceBetweenCard())
                {
                    case 3:
                        _listTempCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private void FourCardStraightPoss()
        {
            if (_listTempCombo.Count > 2)
            {
                switch (SpaceBetweenCard())
                {
                    case 2:
                        if (_listTempCombo[0] != 14)
                        {
                            _listTempCombo.RemoveAt(0);
                        }
                        break;
                    case 3:
                        _listTempCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private bool ThreeCardEnd()
        {
            int comptSuite = 0;
            if (ListValue.Count == 5) {
                for (int i = 0; i < ListValue.Count - 1; i++)
                {
                    if(ListValue[i].Value == ListValue[i + 1].Value + 1)
                    {
                        comptSuite++;
                    }
                    else
                    {
                        comptSuite = 0;
                    }

                }
            }

            return comptSuite == 2;
        }

        private int SpaceBetweenCard()
        {
            int compt = 0;
            int nbr = 1;

            while (nbr == 1)
            {
                if (compt + 1 < _listValue.Count)
                {
                    nbr = _listValue[compt].Value - _listValue[compt + 1].Value;
                    compt++;
                }
                else
                {
                    nbr = -1;
                }
            }

            return nbr;
        }

        private void CreateList()
        {
            _listTempCombo = new List<int>();
            int itemStraight = (int)_listCardInGame[0].Value + 2;

            for (int i = 0; i < _listCardInGame.Count + 4; i++)
            {
                if (itemStraight < 15 && itemStraight > 1)
                {
                    _listTempCombo.Add(itemStraight);
                }
                itemStraight--;
            }
        }
        private void DescendSuitList()
        {
            _listCardInGame.OrderByDescending(a => a.Suit);
        }

        private void isFlushCombo()
        {
            bool firstTime = true;
            int cardBefore = 0;
            int compt = 0;

            foreach (Card card in _listCardInGame)
            {
                if (firstTime)
                {
                    cardBefore = (int)card.Suit;
                    firstTime = false;
                }
                else if (cardBefore == (int)card.Suit)
                {
                    compt++;
                }
                else
                {
                    cardBefore = (int)card.Suit;
                }
            }

            _isFlush = compt >= 2;

        }

        private void isStraightCombo()
        {

            bool firstTime = true;
            bool blockCombo = false;

            int cardBefore = 0;
            int comptStraight = 0;

            foreach (Card card in _listCardInGame)
            {

                int actuelCard = (int)card.Value;

                if (firstTime)
                {
                    cardBefore = (int)card.Value;
                    firstTime = false;
                }
                else if (cardBefore - 1 == actuelCard)
                {
                    cardBefore--;
                    comptStraight++;
                }
                else if (cardBefore - 2 == actuelCard && !blockCombo)
                {
                    cardBefore -= 2;
                    comptStraight++;

                    if (_listCardInGame.Count < 4)
                    {
                        blockCombo = true;
                    }
                }
                else if (cardBefore - 3 == actuelCard && !blockCombo)
                {
                    cardBefore -= 3;
                    comptStraight++;

                    if (_listCardInGame.Count < 4)
                    {
                        blockCombo = true;
                    }
                }


            }

            if (comptStraight >= 2)
            {
                _isStraight = true;
            }
            else
            {
                IsStraight = false;
            }
        }

        private void CreateSameKindCombo()
        {
            int taille = _listValue.Count;
            int compt = 0;
            int comptC = 0;

            for (int i = 0; i < _listValue.Count; i++)
            {
                while (compt <= taille)
                {
                    if (compt < taille && _listValue.Count > 1)
                    {
                        _comboValuePoss.Add(Tuple.Create((int)_listValue[i].Value, (int)_listValue[compt].Value));
                    }
                    else if(_listValue.Count > 1)
                    {
                        _comboValuePoss.Add(Tuple.Create((int)_listValue[i].Value, -1));
                    }
                    else
                    {
                        _comboValuePoss.Add(Tuple.Create((int)_listValue[i].Value, -1));
                        compt = taille + 1;
                    }
                    compt++;
                }
                comptC++;
                compt = comptC;
            }
        }

        private void CreateListValue()
        {
            _listValue.Add(_listCardInGame[0]);
            Card.CardRank CardBefore = _listValue[0].Value;
            foreach (Card card in _listCardInGame)
            {

                if (CardBefore != card.Value)
                {
                    _listValue.Add(card);
                    CardBefore = card.Value;
                }

            }
        }

        private void DescendValueList()
        {
            _listCardInGame.OrderByDescending(a => a.Value);
        }

    }
}
