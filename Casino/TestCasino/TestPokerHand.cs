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
        private Bitmap imageBidon;

        public TestPokerHand()
        {
            list = new List<Card>();
            pokerCombo = new PokerHand(list);
            imageBidon = Properties.Resources._2C;
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
            PrivateObject obj = new PrivateObject(pokerCombo);

            list.Add(new Card(Card.CardRank.Eight, Card.CardSuit.Clubs, imageBidon));
            list.Add(new Card(Card.CardRank.Eight, Card.CardSuit.Spades, imageBidon));
            list.Add(new Card(Card.CardRank.Eight, Card.CardSuit.Hearts, imageBidon));

            obj.Invoke("CreateListValue");

            Assert.AreEqual(1, pokerCombo.ListValue.Count);

            list.Clear();
            pokerCombo.ListValue.Clear();

            list.Add(new Card(Card.CardRank.Eight, Card.CardSuit.Clubs, imageBidon));
            list.Add(new Card(Card.CardRank.Eight, Card.CardSuit.Spades, imageBidon));
            list.Add(new Card(Card.CardRank.Five, Card.CardSuit.Hearts, imageBidon));

            obj.Invoke("CreateListValue");

            Assert.AreEqual(2, pokerCombo.ListValue.Count);

            list.Clear();
            pokerCombo.ListValue.Clear();

            list.Add(new Card(Card.CardRank.Four, Card.CardSuit.Clubs, imageBidon));
            list.Add(new Card(Card.CardRank.Eight, Card.CardSuit.Spades, imageBidon));
            list.Add(new Card(Card.CardRank.King, Card.CardSuit.Hearts, imageBidon));

            obj.Invoke("CreateListValue");

            Assert.AreEqual(3, pokerCombo.ListValue.Count);

            list.Clear();
            pokerCombo.ListValue.Clear();
        }
    }
}
