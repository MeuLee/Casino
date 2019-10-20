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

        public bool IsStraight { 
            get { return isStraight; }
            set { this.isStraight = value; }
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
            isFlush = isFlushCombo();
            if (isFlush)
            {
                ComboFlush();
            }
        }

        private void CreateStraightCombo()
        {
            isStraightCombo();
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
            AdjustCards();
            InsertComboStraight();

        }

        private void InsertComboStraight()
        {
            for (int i = 0; i < ListTempCombo.Count - 2; i++)
            {
                comboSraightPoss.Add(Tuple.Create(ListTempCombo[i], ListTempCombo[i + 1]));
            }
        }
        private void AdjustCards()
        {

            foreach (Card card in listValue)
            {
                if (ListTempCombo.Contains((int)card.Value))
                {
                    ListTempCombo.Remove((int)card.Value);
                }
            }

            DetermineStraight();

        }

        private void DetermineStraight()
        {
            if (listValue.Count == 4)
            {
                FourCardStraightPoss();
            }
            else if (listValue.Count == 3)
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
            switch (CheckerTypeStraight())
            {
                case 2:
                    if (ThreeCardEnd() && ListTempCombo[0] == 14)
                    {
                        ListTempCombo.RemoveAt(0);
                    }
                    break;
                case 3:
                    if (ThreeCardEnd())
                    {
                        int compt = 1;

                        for (int i = 0; i < 2; i++)
                        {
                            int itemStraight = (int)listValue[listValue.Count - 1].Value - compt;

                            if (itemStraight < 15 && itemStraight > 1 && !ListTempCombo.Contains(itemStraight))
                            {
                                ListTempCombo.Add(itemStraight);
                            }
                            compt++;
                        }
                    }
                    break;
            }
        }

        private void ThreeCardsStraighPoss()
        {
            if (ListTempCombo.Count > 3)
            {

                switch (CheckerTypeStraight())
                {
                    case 2:
                        ListTempCombo.RemoveAt(0);
                        break;
                    case 3:
                        ListTempCombo.RemoveRange(0, 2);
                        break;
                }
            }
            else if (ListTempCombo.Count > 2)
            {
                switch (CheckerTypeStraight())
                {
                    case 3:
                        ListTempCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private void FourCardStraightPoss()
        {
            if (ListTempCombo.Count > 2)
            {
                switch (CheckerTypeStraight())
                {
                    case 2:
                        if (ListTempCombo[0] != 14)
                        {
                            ListTempCombo.RemoveAt(0);
                        }
                        break;
                    case 3:
                        ListTempCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private bool ThreeCardEnd()
        {
            bool isTrue = false;
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

            if (comptSuite == 2)
            {
                isTrue = true;
            }

            return isTrue;
        }

        private int CheckerTypeStraight()
        {
            int compt = 0;
            int nbr = 1;

            while (nbr == 1) {
                if (compt + 1 < listValue.Count)
                {
                    nbr = listValue[compt].Value - listValue[compt + 1].Value;
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
            ListTempCombo = new List<int>();
            int itemStraight = (int)listCardInGame[0].Value + 2;

            for (int i = 0; i < listCardInGame.Count + 4; i++)
            {
                if (itemStraight < 15 && itemStraight > 1) {
                    ListTempCombo.Add(itemStraight);
                }
                itemStraight--;
            }
        }
        private void DescendSuitList()
        {
            listCardInGame.Sort((a, b) => b.Suit.CompareTo(a.Suit));
        }

        private bool isFlushCombo()
        {
            bool isTrue = false;
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
                isTrue = true;
            }

            return isTrue;
        }

        private void isStraightCombo()
        {

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
            else
            {
                IsStraight = false;
            }
        }

        private void CreateSameKindCombo()
        {
            int taille = listValue.Count;
            int compt = 0;
            int comptC = 0;

            for (int i = 0; i < listValue.Count; i++)
            {
                while (compt <= taille)
                {
                    if (compt < taille && listValue.Count > 1)
                    {
                        comboValuePoss.Add(Tuple.Create((int)listValue[i].Value, (int)listValue[compt].Value));
                    }
                    else if(listValue.Count > 1)
                    {
                        comboValuePoss.Add(Tuple.Create((int)listValue[i].Value, -1));
                    }
                    else
                    {
                        comboValuePoss.Add(Tuple.Create((int)listValue[i].Value, -1));
                        compt = taille + 1;
                    }
                    compt++;
                }
                comptC++;
                compt = comptC;
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

        private void DescendValueList()
        {
            listCardInGame.Sort((a, b) => b.Value.CompareTo(a.Value));
        }

    }
}
