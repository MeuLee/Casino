using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using System;
using System.Collections.Generic;

namespace CasinoUI.Models.Poker
{
    public class PokerAI : PlayerAI, IPokerAction
    {
        private TypePlayerPoker CurrentType { get; set; }

        private List<Card> LisCardOnBoard;
        private List<Card> ListValue;

        private List<int> ListStraightCombo;
        private List<Tuple<int, int>> ComboValuePoss;
        private Card.CardSuit FlushCombo;

        private bool IsStraight = false;
        private bool IsFlush = false;

        private List<double> WinProb;

        public PokerAI(TypePlayerPoker CurrentType)
        {
            Money = 1000;

            this.LisCardOnBoard = new List<Card>();
            this.ListStraightCombo = new List<int>();
            this.ListValue = new List<Card>();
            this.ComboValuePoss = new List<Tuple<int, int>>();
            this.FlushCombo = Card.CardSuit.Diamonds;
            this.WinProb = new List<double>();

            this.CurrentType = CurrentType;
        }

        public List<Card> CardsOnBoard
        {
            get { return LisCardOnBoard; }
            set { this.LisCardOnBoard = value; }
        }

        public PokerActionCode MakeDecision(GameState gameState, int amount_raise = 0)
        {
            WinProb.Clear();
            int TotalProb = AllProbability();

            ChanceWinSamevalue(TotalProb);
            ChanceWinStraight(TotalProb);
            ChanceWinFlush(TotalProb);

            return ReturnPokerAction(amount_raise, gameState);
        }

        private PokerActionCode ReturnPokerAction(int amount_raise, GameState gameState)
        {
            PokerActionCode NextMove;

            if (gameState != GameState.inital)
            {
                double chance = gameState == GameState.raised ? ChanceWinRaise(amount_raise) : 1;
                double prob_win = CalculChance(chance);

                NextMove = prob_win >= 0.4 ?
                    amount_raise >= Money ?
                    PokerActionCode.ALLIN :
                    PokerActionCode.RAISE :
                    prob_win >= 0.1 ?
                    gameState == GameState.normal ?
                    PokerActionCode.CHECK:
                    PokerActionCode.CALL
                    : PokerActionCode.FOLD;
            }

            else
            {
                NextMove = CurrentType == TypePlayerPoker.BIG_BLIND ?
                    PokerActionCode.CHECK : PokerActionCode.CALL;
            }

            return NextMove;
        }

        private double CalculChance(double chance)
        {
            double somme = 0;
            foreach (double nbr in WinProb) somme += nbr;
            somme *= chance;

            return somme;
        }

        private double ChanceWinRaise(int amount_raise)
        {
            return 1.0 - (double)amount_raise/Money;
        }

        private void ChanceWinFlush(int TotalProb)
        {
            double chance = 0;
            if (IsFlush)
            {
                foreach (Card card in Hand)
                {
                    chance = FlushCombo == card.Suit ? chance + 1 : chance;
                }

                chance = chance == 1 ? 0 : chance;

                WinProb.Add(chance/ TotalProb );
            }
        }

        private void ChanceWinStraight(int TotalProb)
        {
            double chance = 0;
            if (IsStraight)
            {
                foreach (Card card in Hand)
                {
                    chance = ListStraightCombo.Contains((int)card.Value) ? chance + 1 : chance;
                }

                WinProb.Add(chance /TotalProb);
            }
        }

        private void ChanceWinSamevalue(int TotalProb)
        {
           double chance = 0;

           foreach (Tuple<int, int> combo in ComboValuePoss){

                foreach (Card card in Hand)
                {
                    chance = combo.Item1 == (int)card.Value 
                        || combo.Item2 == (int)card.Value ?chance + 1: chance;

                }
            }

            WinProb.Add(chance / TotalProb);
        }

        private int AllProbability()
        {

            int debut = ListValue.Count + 1;

            int Total =  0;

            while(debut > 1) Total += (debut--);

            Total = 2 * Total;
            Total = IsStraight ? Total + ListStraightCombo.Count : Total;
            Total = IsFlush ? Total + 2 : Total;

            return Total;

        }

        public void CreateAllPoss(List<Card> LisCardtOnBoard)
        {
            this.LisCardOnBoard = LisCardtOnBoard;

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

            foreach (Card card in LisCardOnBoard)
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
            double nbr = LisCardOnBoard.Count / 2.0;
            int Middle = (int)Math.Ceiling(nbr);
            FlushCombo = LisCardOnBoard[Middle].Suit;
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
            int itemStraight = (int)LisCardOnBoard[0].Value + 2;

            for (int i = 0; i < LisCardOnBoard.Count + 4; i++)
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
            LisCardOnBoard.Sort((a, b) => b.Suit.CompareTo(a.Suit));
        }

        private void isFlushCombo()
        {
            bool firstTime = true;
            int cardBefore = 0;
            int compt = 0;

            foreach (Card card in LisCardOnBoard)
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

            foreach (Card card in LisCardOnBoard)
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

                    if (LisCardOnBoard.Count < 4)
                    {
                        blockCombo = true;
                    }
                }
                else if (cardBefore - 3 == actuelCard && !blockCombo)
                {
                    cardBefore -= 3;
                    comptStraight++;

                    if (LisCardOnBoard.Count < 4)
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
            LisCardOnBoard.Sort((a, b) => b.Value.CompareTo(a.Value));
        }

        public int PokerAllIn()
        {
            MoneyCall = Money;
            return MoneyCall;
        }

        public int PokerCall(int currentRaise)
        {
            MoneyCall = currentRaise;
            return MoneyCall;
        }

        public void PokerCheck()
        {
        }

        public void PokerFold()
        {
            CurrentType = TypePlayerPoker.NULL;
        }

        public int PokerRaise(int moneyRaised)
        {
            MoneyCall += moneyRaised;
            return MoneyCall;
        }
    }
}
