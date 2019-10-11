using System;
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
            pokerTest = new PrivateObject(pokerCombo);

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

        private void AddCardList(Card.CardRank rank, Card.CardSuit suit)
        {
            list.Add(new Card(rank,suit, imageBidon));
        }

        private void CreateBoardCard(int nbrCard, int diffCard)
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
                if (compt < listValue.Count && comptC < listValue.Count) {
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
            pokerTest.Invoke("DescendingValueList");
            CreateBoardCard(3, 0);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(1, pokerCombo.ListValue.Count);
            ClearLists();

            CreateBoardCard(3, 1);
            pokerTest.Invoke("CreateListValue");
            Assert.AreEqual(2, pokerCombo.ListValue.Count);
            ClearLists();

            CreateBoardCard(5, 4);
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

            pokerTest.Invoke("DescendingValueList");

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

            CreateBoardCard(3, 0);
            pokerTest.Invoke("DescendingValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateSameKindCombo");
            AssertSameCombo();
            ClearLists();

            CreateBoardCard(3, 1);
            pokerTest.Invoke("DescendingValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateSameKindCombo");
            AssertSameCombo();
            ClearLists();

            CreateBoardCard(5, 4);
            pokerTest.Invoke("DescendingValueList");
            pokerTest.Invoke("CreateListValue");
            pokerTest.Invoke("CreateSameKindCombo");
            AssertSameCombo();
            ClearLists();

        }

    }
}
