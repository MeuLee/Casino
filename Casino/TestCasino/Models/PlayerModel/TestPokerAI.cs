using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Models.Poker;
using CasinoUI.Models.Cards;
using System.Drawing;
using System;

namespace TestCasino
{
    /// <summary>
    /// Summary description for TestPokerHand
    /// </summary>
    [TestClass]
    public class TestPokerAI
    {
        private PokerAI pokerCombo;
        private List<Card> list;
        private PrivateObject pokerTest;

        private Card.CardRank cardRank;
        private Card.CardSuit cardSuit;
        private Bitmap imageBidon;

        public TestPokerAI()
        {
            list = new List<Card>();
            pokerCombo = new PokerAI(TypePlayerPoker.NORMAL);
            pokerCombo.CardsOnBoard = list;
            pokerTest = new PrivateObject(pokerCombo, new PrivateType(typeof(PokerAI)));

            cardRank = Card.CardRank.Two;
            cardSuit = Card.CardSuit.Clubs;
            imageBidon = Properties.Resources._2C;

        }

        private void ClearLists()
        {
            list.Clear(); 
            List<Card> listVal = (List<Card>) pokerTest.GetFieldOrProperty("ListValue");
            listVal.Clear();
            List<Tuple<int, int>> listTuple = (List<Tuple<int, int>>)pokerTest.GetFieldOrProperty("ComboValuePoss");
            listTuple.Clear();
        }

        private void AddCardList(Card.CardRank rank, Card.CardSuit suit = Card.CardSuit.Clubs)
        {
            list.Add(new Card(rank,suit, imageBidon));
        }

        private void CreateTableCard(int nbrCard, int diffCard)
        {
            int compt = 0;

            do
            {
                AddCardList(cardRank + diffCard, cardSuit);

                if (diffCard > 0) {
                    diffCard--;
                }

                compt++;
            } while (compt < nbrCard);

        }

        private void AssertSameCombo()
        {
            List<Card> listValue =(List<Card>)pokerTest.GetFieldOrProperty("ListValue");
            List<Tuple<int, int>> listTuple = (List<Tuple<int, int>>)pokerTest.GetFieldOrProperty("ComboValuePoss");
            int compt = 0;
            int comptC = 0;

            for(int i = 0; i < listTuple.Count; i++)
            {
                if (compt < listValue.Count && comptC < listValue.Count && listValue.Count > 1) {
                    Assert.AreEqual((int)listValue[comptC].Value, listTuple[i].Item1);
                    Assert.AreEqual((int)listValue[compt].Value, listTuple[i].Item2);
                    compt++;
                }
                else
                {
                    Assert.AreEqual((int)listValue[comptC].Value, listTuple[i].Item1);
                    Assert.AreEqual(-1, listTuple[i].Item2);

                    comptC++;
                    compt = comptC;
                }
            }
        }

        private void StraightDirect()
        {
            CreateTableCard(3, 0);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            CreateTableCard(3, 1);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            CreateTableCard(4, 1);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            CreateTableCard(4, 2);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            CreateTableCard(5, 4);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();
        }

        private void StraightIndirect()
        {
            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Jack, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Nine, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            AddCardList(Card.CardRank.Six, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();

            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Jack, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            AddCardList(Card.CardRank.Seven, cardSuit);
            AddCardList(Card.CardRank.Two, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, (bool)pokerTest.GetFieldOrProperty("IsStraight"));
            ClearLists();



        }

        private void AsserCreateList()
        {
            int[] tab = {14, 13, 12, 11, 10, 9};
            List<int> list = (List<int>)pokerTest.GetFieldOrProperty("ListStraightCombo");
            for(int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(tab[i],list[i]);
            }
        }

        private void TestRemoveCardGeneric(int[] tab, params Card.CardRank[] ranks)
        {
            foreach (var rank in ranks)
            {
                AddCardList(rank, cardSuit);
            }
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateList");
            pokerTest.Invoke("AdjustCards");
            AssertRemoveCard(tab);
            ClearLists();
        }
        private void AssertRemoveCard(int[] tab)
        {
            List<int> list = (List<int>)pokerTest.GetFieldOrProperty("ListStraightCombo");
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(tab[i], list[i]);
            }
        }
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateListValueTestfilter()
        {
            List<Card> ListVal = (List<Card>)pokerTest.GetFieldOrProperty("ListValue");
            pokerTest.Invoke("DescendValueList");
            CreateTableCard(3, 0);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(1, ListVal.Count);
            ClearLists();

            CreateTableCard(3, 1);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(2, ListVal.Count);
            ClearLists();

            CreateTableCard(5, 4);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(5, ListVal.Count);
            ClearLists();
        }

        [TestMethod]
        public void TestDescendingValue()
        {
            List<Card> ListVal = (List<Card>)pokerTest.GetFieldOrProperty("ListValue");

            AddCardList(Card.CardRank.Eight, Card.CardSuit.Clubs);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendValueList");

            for(int i = 0; i < ListVal.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        Assert.AreEqual(Card.CardRank.King, ListVal[i]);
                        break;
                    case 1:
                        Assert.AreEqual(Card.CardRank.Jack, ListVal[i]);
                        break;
                    case 2:
                        Assert.AreEqual(Card.CardRank.Eight, ListVal[i]);
                        break;
                }
            }

            ClearLists();
        }

        [TestMethod]
        public void TestSameKindCombo()
        {

            CreateTableCard(3, 0);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateSameKindCombo");
            AssertSameCombo();
            ClearLists();

            CreateTableCard(3, 1);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateSameKindCombo");
            AssertSameCombo();
            ClearLists();

            CreateTableCard(5, 4);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateSameKindCombo");
            AssertSameCombo();
            ClearLists();

        }

        [TestMethod]
        public void TestisStraight()
        {
            StraightDirect();
            StraightIndirect();
        }

        [TestMethod]
        public void TestCreateListStraightCombo()
        {
            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Jack, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            pokerTest.Invoke("CreateList");
            AsserCreateList();
            ClearLists();
        }

        [TestMethod]
        public void TestThreeCardRemoveCard()
        {
            TestRemoveCardGeneric(new int[] { 14, 12, 9 },
                                  Card.CardRank.King,
                                  Card.CardRank.Jack,
                                  Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] { 7, 5, 2 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Four,
                                  Card.CardRank.Three);

            TestRemoveCardGeneric(new int[] { 13, 10 },
                                  Card.CardRank.Ace,
                                  Card.CardRank.Queen,
                                  Card.CardRank.Jack);

            TestRemoveCardGeneric(new int[] { 12, 11 },
                                  Card.CardRank.King,
                                  Card.CardRank.Nine,
                                  Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] { 5, 4 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Three,
                                  Card.CardRank.Two);

            TestRemoveCardGeneric(new int[] { 13, 12 },
                                  Card.CardRank.Ace,
                                  Card.CardRank.Jack,
                                  Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] { 14, 10, 9 },
                                  Card.CardRank.King,
                                  Card.CardRank.Queen,
                                  Card.CardRank.Jack);

            TestRemoveCardGeneric(new int[] { 11, 10 },
                                  Card.CardRank.Ace,
                                  Card.CardRank.King,
                                  Card.CardRank.Queen);

            TestRemoveCardGeneric(new int[] { 8, 7, 3, 2 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Five,
                                  Card.CardRank.Four);

            TestRemoveCardGeneric(new int[] {7, 4, 2 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Five,
                                  Card.CardRank.Three);

        }

        [TestMethod]
        public void TestFourCardRemoveCard()
        {
            TestRemoveCardGeneric(new int[] { 7, 5 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Four,
                                  Card.CardRank.Three,
                                  Card.CardRank.Two);

            TestRemoveCardGeneric(new int[] { 7, 4 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Five,
                                  Card.CardRank.Three,
                                  Card.CardRank.Two);

            TestRemoveCardGeneric(new int[] {8, 7, 2 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Five,
                                  Card.CardRank.Three,
                                  Card.CardRank.Four);

            TestRemoveCardGeneric(new int[] { 14, 9, 8 },
                                  Card.CardRank.King,
                                  Card.CardRank.Queen,
                                  Card.CardRank.Jack,
                                  Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] { 14, 11, 8 },
                                  Card.CardRank.King,
                                  Card.CardRank.Queen,
                                  Card.CardRank.Nine,
                                  Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] {11, 10 },
                                  Card.CardRank.King,
                                  Card.CardRank.Queen,
                                  Card.CardRank.Nine,
                                  Card.CardRank.Eight);

            TestRemoveCardGeneric(new int[] { 10, 9 },
                                 Card.CardRank.King,
                                 Card.CardRank.Queen,
                                 Card.CardRank.Ace,
                                 Card.CardRank.Jack);

            TestRemoveCardGeneric(new int[] { 12, 9 },
                                 Card.CardRank.King,
                                 Card.CardRank.Ten,
                                 Card.CardRank.Ace,
                                 Card.CardRank.Jack);

            TestRemoveCardGeneric(new int[] { 12, 11 },
                                 Card.CardRank.King,
                                 Card.CardRank.Ten,
                                 Card.CardRank.Ace,
                                 Card.CardRank.Nine);
        }

        [TestMethod]
        public void TestFiveCardRemoveCard()
        {
            TestRemoveCardGeneric(new int[] { 8, 7 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Four,
                                  Card.CardRank.Three,
                                  Card.CardRank.Two,
                                  Card.CardRank.Five);

            TestRemoveCardGeneric(new int[] { 10, 9, 5, 2 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Four,
                                  Card.CardRank.Three,
                                  Card.CardRank.Seven,
                                  Card.CardRank.Eight);

            TestRemoveCardGeneric(new int[] { 10, 9, 5, 4 },
                                  Card.CardRank.Six,
                                  Card.CardRank.Three,
                                  Card.CardRank.Two,
                                  Card.CardRank.Seven,
                                  Card.CardRank.Eight);

            TestRemoveCardGeneric(new int[] { 14, 8, 7 },
                                  Card.CardRank.King,
                                  Card.CardRank.Queen,
                                  Card.CardRank.Jack,
                                  Card.CardRank.Ten,
                                  Card.CardRank.Nine);

            TestRemoveCardGeneric(new int[] { 14, 12, 7 },
                                 Card.CardRank.King,
                                 Card.CardRank.Eight,
                                 Card.CardRank.Jack,
                                 Card.CardRank.Ten,
                                 Card.CardRank.Nine);

            TestRemoveCardGeneric(new int[] { 14, 11, 10, 6, 5 },
                                 Card.CardRank.King,
                                 Card.CardRank.Queen,
                                 Card.CardRank.Nine,
                                 Card.CardRank.Eight,
                                 Card.CardRank.Seven);

            TestRemoveCardGeneric(new int[] { 9, 8 },
                                 Card.CardRank.Ace,
                                 Card.CardRank.King,
                                 Card.CardRank.Queen,
                                 Card.CardRank.Jack,
                                 Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] { 12, 8, 7 },
                                 Card.CardRank.Ace,
                                 Card.CardRank.King,
                                 Card.CardRank.Nine,
                                 Card.CardRank.Jack,
                                 Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] { 12, 11, 7, 6 },
                                 Card.CardRank.Ace,
                                 Card.CardRank.King,
                                 Card.CardRank.Nine,
                                 Card.CardRank.Eight,
                                 Card.CardRank.Ten);

            TestRemoveCardGeneric(new int[] {13, 10, 6, 5},
                                Card.CardRank.Queen,
                                Card.CardRank.Eight,
                                Card.CardRank.Jack,
                                Card.CardRank.Seven,
                                Card.CardRank.Nine);
        }

        [TestMethod]
        public void TestDescendingSuitList()
        {
            AddCardList(Card.CardRank.Eight, Card.CardSuit.Clubs);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendSuitList");

            for (int i = 0; i < pokerCombo.CardsOnBoard.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        Assert.AreEqual(Card.CardSuit.Hearts, pokerCombo.CardsOnBoard[i].Suit);
                        break;
                    case 1:
                        Assert.AreEqual(Card.CardSuit.Clubs, pokerCombo.CardsOnBoard[i].Suit);
                        break;
                    case 2:
                        Assert.AreEqual(Card.CardSuit.Diamonds, pokerCombo.CardsOnBoard[i].Suit);
                        break;
                }
            }

            ClearLists();
        }

        [TestMethod]
        public void TestIsFlush()
        {
            AddCardList(Card.CardRank.Eight, Card.CardSuit.Clubs);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");

            Assert.IsFalse((bool)pokerTest.GetFieldOrProperty("IsFlush"));
            ClearLists();

            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");

            Assert.IsTrue((bool)pokerTest.GetFieldOrProperty("IsFlush"));

        }

        [TestMethod]
        public void TestFlushCombo()
        {
            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");
            pokerTest.Invoke("ComboFlush");

            Assert.AreEqual(Card.CardSuit.Diamonds, (Card.CardSuit)pokerTest.GetFieldOrProperty("FlushCombo"));
            ClearLists();


            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");
            pokerTest.Invoke("ComboFlush");

            Assert.AreEqual(Card.CardSuit.Diamonds, (Card.CardSuit)pokerTest.GetFieldOrProperty("FlushCombo"));
            ClearLists();

            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Clubs);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");
            pokerTest.Invoke("ComboFlush");

            Assert.AreEqual(Card.CardSuit.Diamonds, (Card.CardSuit)pokerTest.GetFieldOrProperty("FlushCombo"));
            ClearLists();
        }

        private void CreateGameEnvironnment(List<Card> HandContent)
        {
            AddCardList(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);

            pokerCombo.CreateAllPoss(list);

            pokerCombo.Hand = HandContent;
        }

        private void AssertSingleTestChanceWin(String method, double expectNbr)
        {
            int nbr = (int)pokerTest.Invoke("AllProbability");

            object[] args = new object[1] { nbr };
            pokerTest.Invoke(method, args);
            List<double> vals = (List<double>)pokerTest.GetFieldOrProperty("WinProb");

            Assert.AreEqual(expectNbr, vals[0]);
        }

        private void AssertMultTestChanceWinf()
        {

        }

        [TestMethod]
        public void TestAllProbability()
        {
            AddCardList(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);

            pokerCombo.CreateAllPoss(list);

            int nbr = (int) pokerTest.Invoke("AllProbability");

            Assert.AreEqual(23, nbr);

        }

        [TestMethod]
        public void TestChanceWinSameValue()
        {

            List<Card> HandContent = new List<Card>()
            {
                new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds, imageBidon),
                new Card(Card.CardRank.King, Card.CardSuit.Diamonds, imageBidon)
            };
            CreateGameEnvironnment(HandContent);

            AssertSingleTestChanceWin("ChanceWinSamevalue", 8.0 / 23.0);

        }
        [TestMethod]
        public void TestChanceWinStraight()
        {
            List<Card> HandContent = new List<Card>()
            {
                new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds, imageBidon),
                new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds, imageBidon)
            };
            CreateGameEnvironnment(HandContent);

            AssertSingleTestChanceWin("ChanceWinStraight", 2.0 / 23.0);
        }
        [TestMethod]
        public void TestChanceWinFlush()
        {

            List<Card> HandContent = new List<Card>()
            {
                new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds, imageBidon),
                new Card(Card.CardRank.King, Card.CardSuit.Diamonds, imageBidon)
            };
            CreateGameEnvironnment(HandContent);

            AssertSingleTestChanceWin("ChanceWinFlush", 2.0 / 23.0);

        }

        private void DoAllPoss()
        {
            int nbr = (int)pokerTest.Invoke("AllProbability");
            object[] TotalProb = new object[1] { nbr };

            pokerTest.Invoke("ChanceWinSamevalue", TotalProb);
            pokerTest.Invoke("ChanceWinStraight", TotalProb);
            pokerTest.Invoke("ChanceWinFlush", TotalProb);
        }
        [TestMethod]
        public void TestChanceWinRaise()
        {

            List<Card> HandContent = new List<Card>()
            {
                new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds, imageBidon),
                new Card(Card.CardRank.King, Card.CardSuit.Diamonds, imageBidon)
            };
            CreateGameEnvironnment(HandContent);
            DoAllPoss();

            pokerCombo.Money = 800;
            object[] amountRaise = new object[1] { 124 };
            double nbrExpect =(double) pokerTest.Invoke("ChanceWinRaise", amountRaise);

            Assert.AreEqual(0.845, nbrExpect);

        }

        [TestMethod]
        public void TestCalculChance()
        {
            List<Card> HandContent = new List<Card>()
            {
                new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds, imageBidon),
                new Card(Card.CardRank.King, Card.CardSuit.Diamonds, imageBidon)
            };
            CreateGameEnvironnment(HandContent);
            DoAllPoss();

            object[] args_Chance = new object[1] { 1 };
            double nbrExpect = (double)pokerTest.Invoke("CalculChance", args_Chance);

            Assert.AreEqual(10.0/23.0, nbrExpect);
        }
        private PokerActionCode GameSituationCreater(int MoneyRaise, GameState currentState)
        {
            pokerCombo.Money = 1000;
            object[] args = new object[2] { currentState, MoneyRaise };
            PokerActionCode expectedAct = (PokerActionCode)pokerTest.Invoke("MakeDecision", args);

            return expectedAct;
        }
        [TestMethod]
        public void TestReturnPokerAction()
        {
            List<Card> HandContent = new List<Card>()
            {
                new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds, imageBidon),
                new Card(Card.CardRank.King, Card.CardSuit.Diamonds, imageBidon)
            };
            CreateGameEnvironnment(HandContent);

            Assert.AreEqual(PokerActionCode.RAISE, GameSituationCreater(0, GameState.normal));

            Assert.AreEqual(PokerActionCode.FOLD, GameSituationCreater(1000, GameState.raised));

            Assert.AreEqual(PokerActionCode.CALL, GameSituationCreater(500, GameState.raised));
        }
    }
    }

