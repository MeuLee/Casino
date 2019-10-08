using CasinoUI.Model.Cards;
using System;
using System.Collections.Generic;

namespace CasinoUI.Model.Poker
{
    public class PokerHand
    {
        private List<Card> listCardInGame;
        private List<Card> listValue;
        private List<int> ListTempCombo;

        private List<Tuple<int, int>> comboValuePoss;
        private List<Tuple<int, int>> comboSraightPoss;
        private Card.CardSuit flushCombo;

        private bool isStraight = false;
        private bool isFlush = false;

        public PokerHand(List<Card> listCardInGame)
        {
            this.listCardInGame = listCardInGame;
            ListTempCombo = new List<int>();
            listValue = new List<Card>();
            comboValuePoss = new List<Tuple<int, int>>();
            comboSraightPoss = new List<Tuple<int, int>>();
            flushCombo = Card.CardSuit.Diamonds;
        }

        public List<Card> ListCardInGame {
            get { return listCardInGame; }
            set { this.listCardInGame = value; }
        }
        public List<Card> ListValue {
            get { return listValue; }
        }

        public List<Tuple<int, int>> ComboValuePoss {
            get { return comboValuePoss; }
            set { this.comboValuePoss = value; }
        }
        private void CheckCombo()
        {
            //Si seulement straight ou flush calcul hignestCombo
            //
        }
        public void CreateAllCombo()
        {
            DescendingValueList();
            CreateListValue();
            CreateSameKindCombo();
            CreateStraightCombo();

            DescendingSuitList();
            CreateFlushCombo();
        }

        private void CreateFlushCombo()
        {
            isFlush = isFlushCombo();
            if (isFlush)
            {
                ComboFlush();
            }
        }

        private void CreateStraightCombo()
        {
            isStraight = isStraightCombo();
            if (isStraight)
            {
                StraightCombo();
            }
        }

        private void CreateListValue()
        {
            OnlyValue();

        }


        private void ComboFlush()
        {
            double nbr = listCardInGame.Count / 2.0;
            int Middle = (int)Math.Ceiling(nbr);
            flushCombo = listCardInGame[Middle].Suit;
        }

        private void StraightCombo()
        {
            CreateList();
            RemoveSameCard();
            InsertComboStraight();

        }

        private void InsertComboStraight()
        {
            for (int i = 0; i < ListTempCombo.Count - 2; i++)
            {
                comboSraightPoss.Add(Tuple.Create(ListTempCombo[i], ListTempCombo[i + 1]));
            }
        }
        private void RemoveSameCard()
        {
            int compt = 0;
            foreach (Card card in listValue)
            {
                if (ListTempCombo.Contains((int)card.Value))
                {
                    ListTempCombo.Remove((int)card.Value);
                    compt++;
                }
                else if (compt < 3)
                {
                    compt = 0;
                }
            }

            if (compt >= 3)
            {
                ListTempCombo.RemoveRange(0, 2);
            }
        }

        private void CreateList()
        {
            ListTempCombo = new List<int>(listCardInGame.Count + 4);
            int itemStraight = (int)listCardInGame[0].Value + 2;

            for (int i = 0; i < ListTempCombo.Count; i++)
            {
                ListTempCombo.Add(itemStraight);
                itemStraight--;
            }
        }
        private void DescendingSuitList()
        {
            listCardInGame.Sort((a, b) => b.Suit.CompareTo(a.Suit));
        }

        private bool isFlushCombo()
        {
            bool isFlush = false;
            bool firstTime = true;
            int cardBefore = 0;
            int compt = 0;

            foreach (Card card in listCardInGame)
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

            if (compt >= 2)
            {
                isFlush = true;
            }

            return isFlush;
        }

        private bool isStraightCombo()
        {
            bool isStraight = false;
            bool firstTime = true;
            bool blockCombo = false;

            int cardBefore = 0;
            int comptStraight = 0;

            foreach (Card card in listCardInGame)
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

                    if (listCardInGame.Count < 4)
                    {
                        blockCombo = true;
                    }
                }
                else if (cardBefore - 3 == actuelCard && !blockCombo)
                {
                    cardBefore -= 3;
                    comptStraight++;

                    if (listCardInGame.Count < 4)
                    {
                        blockCombo = true;
                    }
                }


            }

            if (comptStraight >= 2)
            {
                isStraight = true;
            }

            return isStraight;
        }

        private void CreateSameKindCombo()
        {
            int taille = listValue.Count;
            int compt = 0;
            for (int i = 0; i < listValue.Count; i++)
            {
                while (compt <= taille)
                {
                    if (compt < taille)
                    {
                        comboValuePoss.Add(Tuple.Create((int)listValue[i].Value, (int)listValue[compt].Value));
                    }
                    else
                    {
                        comboValuePoss.Add(Tuple.Create((int)listValue[i].Value, -1));
                    }
                    compt++;
                }
                compt = 0;
                taille--;
            }
        }

        private void OnlyValue()
        {
            bool firstTime = true;
            Card.CardRank CardBefore = 0;

            foreach (Card card in listCardInGame)
            {

                if (firstTime)
                {
                    CardBefore = card.Value;
                    listValue.Add(card);
                    firstTime = false;
                }
                else if (CardBefore != card.Value)
                {
                    listValue.Add(card);
                    CardBefore = card.Value;
                }

            }
        }

        private void DescendingValueList()
        {
            listCardInGame.Sort((a, b) => b.Value.CompareTo(a.Value));
        }

    }
}
