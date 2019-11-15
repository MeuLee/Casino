using CasinoUI.Model.Cards;
using System;
using System.Collections.Generic;

namespace CasinoUI.Model.Poker
{
    public class PokerHand
    {
        private List<Card> listCardInGame;
        private List<Card> listValue;

        private List<int> listStraightCombo;
        private List<Tuple<int, int>> comboValuePoss;
        private Card.CardSuit flushCombo;

        private List<Tuple<Player, int>> listGagnants;

        private bool isStraight = false;
        private bool isFlush = false;

        public PokerHand()
        {
            this.listCardInGame = new List<Card>();
            listStraightCombo = new List<int>();
            listValue = new List<Card>();
            comboValuePoss = new List<Tuple<int, int>>();
            flushCombo = Card.CardSuit.Diamonds;
        }

        public List<int> ListSraightCombo
        {
            get { return listStraightCombo; }
            set { this.listStraightCombo = value; }
        }

        public List<Card> ListCardInGame {
            get { return listCardInGame; }
            set { this.listCardInGame = value; }
        }
        public List<Card> ListValue {
            get { return listValue; }
        }

        public Card.CardSuit FlushCombo
        {
            get { return flushCombo; }
            set { this.flushCombo = value; }
        }

        public List<Tuple<int, int>> ComboValuePoss {
            get { return comboValuePoss; }
            set { this.comboValuePoss = value; }
        }

        public bool IsStraight { 
            get { return isStraight; }
            set { this.isStraight = value; }
        }

        public bool IsFlush
        {
            get { return isFlush; }
            set { this.isFlush = value; }
        }
        private void CheckCombo()
        {
            if (isFlush)
            {
                CheckFlush();
            }

            if (isStraight)
            {
                CheckStraight();
            }

            CheckSameKind();
        }

        private void CheckStraight()
        {
            throw new NotImplementedException();
        }

        private void CheckFlush()
        {
            throw new NotImplementedException();
        }

        private void CheckSameKind()
        {
            throw new NotImplementedException();
        }

        public void CreateAllCombo(List<Card> listCardInGame)
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

        }
        private void AdjustCards()
        {

            foreach (Card card in listValue)
            {
                if (listStraightCombo.Contains((int)card.Value))
                {
                    listStraightCombo.Remove((int)card.Value);
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
            switch (SpaceBetweenCard())
            {
                case 2:
                    if (ThreeCardEnd() && listStraightCombo[0] == 14)
                    {
                        listStraightCombo.RemoveAt(0);
                    }
                    break;
                case 3:
                    if (ThreeCardEnd())
                    {
                        int compt = 1;

                        for (int i = 0; i < 2; i++)
                        {
                            int itemStraight = (int)listValue[listValue.Count - 1].Value - compt;

                            if (itemStraight < 15 && itemStraight > 1 && !listStraightCombo.Contains(itemStraight))
                            {
                                listStraightCombo.Add(itemStraight);
                            }
                            compt++;
                        }
                    }
                    break;
            }
        }

        private void ThreeCardsStraighPoss()
        {
            if (listStraightCombo.Count > 3)
            {

                switch (SpaceBetweenCard())
                {
                    case 2:
                        listStraightCombo.RemoveAt(0);
                        break;
                    case 3:
                        listStraightCombo.RemoveRange(0, 2);
                        break;
                }
            }
            else if (listStraightCombo.Count > 2)
            {
                switch (SpaceBetweenCard())
                {
                    case 3:
                        listStraightCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private void FourCardStraightPoss()
        {
            if (listStraightCombo.Count > 2)
            {
                switch (SpaceBetweenCard())
                {
                    case 2:
                        if (listStraightCombo[0] != 14)
                        {
                            listStraightCombo.RemoveAt(0);
                        }
                        break;
                    case 3:
                        listStraightCombo.RemoveAt(0);
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

        private int SpaceBetweenCard()
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
            listStraightCombo = new List<int>();
            int itemStraight = (int)listCardInGame[0].Value + 2;

            for (int i = 0; i < listCardInGame.Count + 4; i++)
            {
                if (itemStraight < 15 && itemStraight > 1) {
                    listStraightCombo.Add(itemStraight);
                }
                itemStraight--;
            }
        }
        private void DescendSuitList()
        {
            listCardInGame.Sort((a, b) => b.Suit.CompareTo(a.Suit));
        }

        private void isFlushCombo()
        {
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
            else
            {
                isFlush = false;
            }

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
