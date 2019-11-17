using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using System;
using System.Collections.Generic;

namespace CasinoUI.Models.Poker
{
    public class PokerAI : PlayerAI, IPokerAction
    {

        private List<Card> LisCardtOnBoard;
        private List<Card> ListValue;

        private List<int> ListStraightCombo;
        private List<Tuple<int, int>> ComboValuePoss;
        private Card.CardSuit FlushCombo;

        private bool IsStraight = false;
        private bool IsFlush = false;

        public PokerAI()
        {
            Money = 1000;

            this.LisCardtOnBoard = new List<Card>();
            this.ListStraightCombo = new List<int>();
            this.ListValue = new List<Card>();
            this.ComboValuePoss = new List<Tuple<int, int>>();
            this.FlushCombo = Card.CardSuit.Diamonds;
        }

        public List<Card> CardsOnBoard
        {
            get { return LisCardtOnBoard; }
            set { this.LisCardtOnBoard = value; }
        }

        public int MakeDecision()
        {
            List<Card> hand = GetHand();


            return (int) PokerActionCode.FOLD;
        }

        public void CreateAllCPoss(List<Card> LisCardtOnBoard)
        {
            this.LisCardtOnBoard = LisCardtOnBoard;

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
            if (IsFlush)
            {
                ComboFlush();
            }
        }

        private void CreateStraightCombo()
        {
            isStraightCombo();
            if (IsStraight)
            {
                StraightCombo();
            }
        }

        private void CreateListValue()
        {
            bool firstTime = true;
            Card.CardRank CardBefore = 0;

            foreach (Card card in LisCardtOnBoard)
            {

                if (firstTime)
                {
                    CardBefore = card.Value;
                    ListValue.Add(card);
                    firstTime = false;
                }
                else if (CardBefore != card.Value)
                {
                    ListValue.Add(card);
                    CardBefore = card.Value;
                }

            }

        }


        private void ComboFlush()
        {
            double nbr = LisCardtOnBoard.Count / 2.0;
            int Middle = (int)Math.Ceiling(nbr);
            FlushCombo = LisCardtOnBoard[Middle].Suit;
        }

        private void StraightCombo()
        {
            CreateList();
            AdjustCards();

        }
        private void AdjustCards()
        {

            foreach (Card card in ListValue)
            {
                if (ListStraightCombo.Contains((int)card.Value))
                {
                    ListStraightCombo.Remove((int)card.Value);
                }
            }

            DetermineStraight();

        }

        private void DetermineStraight()
        {
            if (ListValue.Count == 4)
            {
                FourCardStraightPoss();
            }
            else if (ListValue.Count == 3)
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
                    if (ThreeCardEnd() && ListStraightCombo[0] == 14)
                    {
                        ListStraightCombo.RemoveAt(0);
                    }
                    break;
                case 3:
                    if (ThreeCardEnd())
                    {
                        int compt = 1;

                        for (int i = 0; i < 2; i++)
                        {
                            int itemStraight = (int)ListValue[ListValue.Count - 1].Value - compt;

                            if (itemStraight < 15 && itemStraight > 1 && !ListStraightCombo.Contains(itemStraight))
                            {
                                ListStraightCombo.Add(itemStraight);
                            }
                            compt++;
                        }
                    }
                    break;
            }
        }

        private void ThreeCardsStraighPoss()
        {
            if (ListStraightCombo.Count > 3)
            {

                switch (SpaceBetweenCard())
                {
                    case 2:
                        ListStraightCombo.RemoveAt(0);
                        break;
                    case 3:
                        ListStraightCombo.RemoveRange(0, 2);
                        break;
                }
            }
            else if (ListStraightCombo.Count > 2)
            {
                switch (SpaceBetweenCard())
                {
                    case 3:
                        ListStraightCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private void FourCardStraightPoss()
        {
            if (ListStraightCombo.Count > 2)
            {
                switch (SpaceBetweenCard())
                {
                    case 2:
                        if (ListStraightCombo[0] != 14)
                        {
                            ListStraightCombo.RemoveAt(0);
                        }
                        break;
                    case 3:
                        ListStraightCombo.RemoveAt(0);
                        break;
                }
            }
        }

        private bool ThreeCardEnd()
        {
            bool isTrue = false;
            int comptSuite = 0;
            if (ListValue.Count == 5)
            {
                for (int i = 0; i < ListValue.Count - 1; i++)
                {
                    if (ListValue[i].Value == ListValue[i + 1].Value + 1)
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

            while (nbr == 1)
            {
                if (compt + 1 < ListValue.Count)
                {
                    nbr = ListValue[compt].Value - ListValue[compt + 1].Value;
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
            ListStraightCombo = new List<int>();
            int itemStraight = (int)LisCardtOnBoard[0].Value + 2;

            for (int i = 0; i < LisCardtOnBoard.Count + 4; i++)
            {
                if (itemStraight < 15 && itemStraight > 1)
                {
                    ListStraightCombo.Add(itemStraight);
                }
                itemStraight--;
            }
        }
        private void DescendSuitList()
        {
            LisCardtOnBoard.Sort((a, b) => b.Suit.CompareTo(a.Suit));
        }

        private void isFlushCombo()
        {
            bool firstTime = true;
            int cardBefore = 0;
            int compt = 0;

            foreach (Card card in LisCardtOnBoard)
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
                IsFlush = true;
            }
            else
            {
                IsFlush = false;
            }

        }

        private void isStraightCombo()
        {

            bool firstTime = true;
            bool blockCombo = false;

            int cardBefore = 0;
            int comptStraight = 0;

            foreach (Card card in LisCardtOnBoard)
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

                    if (LisCardtOnBoard.Count < 4)
                    {
                        blockCombo = true;
                    }
                }
                else if (cardBefore - 3 == actuelCard && !blockCombo)
                {
                    cardBefore -= 3;
                    comptStraight++;

                    if (LisCardtOnBoard.Count < 4)
                    {
                        blockCombo = true;
                    }
                }


            }

            if (comptStraight >= 2)
            {
                IsStraight = true;
            }
            else
            {
                IsStraight = false;
            }
        }

        private void CreateSameKindCombo()
        {
            int taille = ListValue.Count;
            int compt = 0;
            int comptC = 0;

            for (int i = 0; i < ListValue.Count; i++)
            {
                while (compt <= taille)
                {
                    if (compt < taille && ListValue.Count > 1)
                    {
                        ComboValuePoss.Add(Tuple.Create((int)ListValue[i].Value, (int)ListValue[compt].Value));
                    }
                    else if (ListValue.Count > 1)
                    {
                        ComboValuePoss.Add(Tuple.Create((int)ListValue[i].Value, -1));
                    }
                    else
                    {
                        ComboValuePoss.Add(Tuple.Create((int)ListValue[i].Value, -1));
                        compt = taille + 1;
                    }
                    compt++;
                }
                comptC++;
                compt = comptC;
            }
        }
        private void DescendValueList()
        {
            LisCardtOnBoard.Sort((a, b) => b.Value.CompareTo(a.Value));
        }

        public void PokerAllIn()
        {
            throw new System.NotImplementedException();
        }

        public int PokerCall(int currentRaise)
        {
            throw new System.NotImplementedException();
        }

        public void PokerCheck()
        {
            throw new System.NotImplementedException();
        }

        public void PokerFold()
        {
            throw new System.NotImplementedException();
        }

        public int PokerRaise(int moneyRaised)
        {
            throw new System.NotImplementedException();
        }
    }
}
