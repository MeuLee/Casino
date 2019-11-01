﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Model.Poker;
using CasinoUI.Model.Cards;
using System.Drawing;

namespace TestCasino
{
    /// <summary>
    /// Summary description for TestPokerHand
    /// </summary>
    [TestClass]
    public class TestPokerHand
    {
        private PokerHand pokerCombo;
        private List<Card> list;
        private PrivateObject pokerTest;

        private Card.CardRank cardRank;
        private Card.CardSuit cardSuit;
        private Bitmap imageBidon;

        public TestPokerHand()
        {
            list = new List<Card>();
            pokerCombo = new PokerHand(list);
            pokerTest = new PrivateObject(pokerCombo, new PrivateType(typeof(PokerHand)));

            cardRank = Card.CardRank.Two;
            cardSuit = Card.CardSuit.Clubs;
            imageBidon = Properties.Resources._2C;

        }

        private void ClearLists()
        {
            list.Clear();
            pokerCombo.ListValue.Clear();
            pokerCombo.ComboValuePoss.Clear();
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
            List<Card> listValue = pokerCombo.ListValue;
            int compt = 0;
            int comptC = 0;

            for(int i = 0; i < pokerCombo.ComboValuePoss.Count; i++)
            {
                if (compt < listValue.Count && comptC < listValue.Count && listValue.Count > 1) {
                    Assert.AreEqual((int)listValue[comptC].Value, pokerCombo.ComboValuePoss[i].Item1);
                    Assert.AreEqual((int)listValue[compt].Value, pokerCombo.ComboValuePoss[i].Item2);
                    compt++;
                }
                else
                {
                    Assert.AreEqual((int)listValue[comptC].Value, pokerCombo.ComboValuePoss[i].Item1);
                    Assert.AreEqual(-1, pokerCombo.ComboValuePoss[i].Item2);

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
            Assert.AreEqual(false, pokerCombo.IsStraight);
            ClearLists();

            CreateTableCard(3, 1);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, pokerCombo.IsStraight);
            ClearLists();

            CreateTableCard(4, 1);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, pokerCombo.IsStraight);
            ClearLists();

            CreateTableCard(4, 2);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, pokerCombo.IsStraight);
            ClearLists();

            CreateTableCard(5, 4);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, pokerCombo.IsStraight);
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
            Assert.AreEqual(true, pokerCombo.IsStraight);
            ClearLists();

            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Nine, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, pokerCombo.IsStraight);
            ClearLists();

            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            AddCardList(Card.CardRank.Six, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(false, pokerCombo.IsStraight);
            ClearLists();

            AddCardList(Card.CardRank.King, cardSuit);
            AddCardList(Card.CardRank.Jack, cardSuit);
            AddCardList(Card.CardRank.Ten, cardSuit);
            AddCardList(Card.CardRank.Seven, cardSuit);
            AddCardList(Card.CardRank.Two, cardSuit);
            pokerTest.Invoke("DescendValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("isStraightCombo");
            Assert.AreEqual(true, pokerCombo.IsStraight);
            ClearLists();



        }

        private void AsserCreateList()
        {
            int[] tab = {14, 13, 12, 11, 10, 9};
            List<int> list =(List<int>) pokerTest.GetFieldOrProperty("_listStraightCombo");
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
            List<int> list = (List<int>)pokerTest.GetFieldOrProperty("_listStraightCombo");
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
            pokerTest.Invoke("DescendValueList");
            CreateTableCard(3, 0);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(1, pokerCombo.ListValue.Count);
            ClearLists();

            CreateTableCard(3, 1);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(2, pokerCombo.ListValue.Count);
            ClearLists();

            CreateTableCard(5, 4);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(5, pokerCombo.ListValue.Count);
            ClearLists();
        }

        [TestMethod]
        public void TestDescendingValue()
        {
            AddCardList(Card.CardRank.Eight, Card.CardSuit.Clubs);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendValueList");

            for(int i = 0; i < pokerCombo.ListValue.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        Assert.AreEqual(Card.CardRank.King, pokerCombo.ListValue[i]);
                        break;
                    case 1:
                        Assert.AreEqual(Card.CardRank.Jack, pokerCombo.ListValue[i]);
                        break;
                    case 2:
                        Assert.AreEqual(Card.CardRank.Eight, pokerCombo.ListValue[i]);
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

            for (int i = 0; i < pokerCombo.ListCardInGame.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        Assert.AreEqual(Card.CardSuit.Hearts, pokerCombo.ListCardInGame[i].Suit);
                        break;
                    case 1:
                        Assert.AreEqual(Card.CardSuit.Clubs, pokerCombo.ListCardInGame[i].Suit);
                        break;
                    case 2:
                        Assert.AreEqual(Card.CardSuit.Diamonds, pokerCombo.ListCardInGame[i].Suit);
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

            Assert.IsFalse(pokerCombo.IsFlush);
            ClearLists();

            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");

            Assert.IsTrue(pokerCombo.IsFlush);

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

            Assert.AreEqual(Card.CardSuit.Diamonds, pokerCombo.FlushCombo);
            ClearLists();


            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");
            pokerTest.Invoke("ComboFlush");

            Assert.AreEqual(Card.CardSuit.Diamonds, pokerCombo.FlushCombo);
            ClearLists();

            AddCardList(Card.CardRank.Eight, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            AddCardList(Card.CardRank.King, Card.CardSuit.Clubs);
            AddCardList(Card.CardRank.Jack, Card.CardSuit.Hearts);

            pokerTest.Invoke("DescendSuitList");
            pokerTest.Invoke("isFlushCombo");
            pokerTest.Invoke("ComboFlush");

            Assert.AreEqual(Card.CardSuit.Diamonds, pokerCombo.FlushCombo);
            ClearLists();
        }
    }


    }

