using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker;
using CasinoUI.Models.Poker.Evaluator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestCasino
{
    [TestClass]
    public class TesthandEvaluator
    {
        private Card[] jeuDeCarte;
        private HandEvaluator evaluationCard;
        private PrivateObject _evaluator;

        [TestInitialize]
        public void TestInitialize()
        {
            jeuDeCarte = new Card[7];
        }

        #region commented code
        //[TestMethod]
        //public void TestRoyalFlush()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
        //    jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

        //    evaluationCard = new HandEvaluator(jeuDeCarte);

        //    Hand player = evaluationCard.EvaluateHand();

        //    Assert.AreEqual(Hand.RoyalFlush, player);
        //    Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        //}

        //[TestMethod]
        //public void TestRoyalFlushAleatoire()
        //{
        //    jeuDeCarte[5] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
        //    jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

        //    evaluationCard = new HandEvaluator(jeuDeCarte);

        //    Hand player = evaluationCard.EvaluateHand();

        //    Assert.AreEqual(Hand.RoyalFlush, player);
        //    Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        //}

        //[TestMethod]
        //public void TestRoyalFlushNotFlush()
        //{

        //    jeuDeCarte[5] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
        //    jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
        //    evaluationCard = new HandEvaluator(jeuDeCarte);
        //    Hand player = evaluationCard.EvaluateHand();
        //    Assert.AreEqual(Hand.RoyalFlush, player);
        //    Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        //}

        //[TestMethod]
        //public void TestStraightFlushPosFin()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

        //    evaluationCard = new HandEvaluator(jeuDeCarte);

        //    Hand player = evaluationCard.EvaluateHand();

        //    Assert.AreEqual(Hand.StraightFlush, player);
        //    Assert.AreEqual(55, evaluationCard.HandStrengths.CalculerTotal());
        //    Assert.AreEqual(13, (int)evaluationCard.HandStrengths.HighCard.Value);
        //}

        //[TestMethod]
        //public void TestStraightFlushPosMilieu()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Seven, Card.CardSuit.Hearts);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Six, Card.CardSuit.Hearts);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);

        //    evaluationCard = new HandEvaluator(jeuDeCarte);

        //    Hand player = evaluationCard.EvaluateHand();

        //    Assert.AreEqual(Hand.StraightFlush, player);
        //    Assert.AreEqual(40, evaluationCard.HandStrengths.CalculerTotal());
        //    Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        //}

        //[TestMethod]
        //public void TestStraightFlushPosDebut()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

        //    evaluationCard = new HandEvaluator(jeuDeCarte);

        //    Hand player = evaluationCard.EvaluateHand();

        //    Assert.AreEqual(Hand.StraightFlush, player);
        //    Assert.AreEqual(55, evaluationCard.HandStrengths.CalculerTotal());
        //    Assert.AreEqual(13, (int)evaluationCard.HandStrengths.HighCard.Value);
        //}

        //[TestMethod]
        //public void TestFourOfKind()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

        //    evaluationCard = new HandEvaluator(jeuDeCarte);

        //    Hand player = evaluationCard.EvaluateHand();

        //    Assert.AreEqual(Hand.FourKind, player);
        //    //Assert.AreEqual(22, evaluationCard.HandStrengths.CalculerTotal());
        //}

        //[TestMethod]
        //public void TestFullHouse()
        //{
        //    jeuDeCarte = InitializeCardArr(Card.CardSuit.Clubs, 14, 14, 13, 13, 13, 10, 9);
        //    evaluationCard = new HandEvaluator(jeuDeCarte);
        //    Hand player = evaluationCard.EvaluateHand();
        //    Assert.AreEqual(Hand.FullHouse, player);
        //}

        //[TestMethod]
        //public void TestFlush()
        //{
        //    jeuDeCarte = InitializeCardArr(Card.CardSuit.Clubs, 9, 9, 8, 3, 8, 5, 4);
        //    evaluationCard = new HandEvaluator(jeuDeCarte);
        //    Hand player = evaluationCard.EvaluateHand();
        //    Assert.AreEqual(Hand.Flush, player);
        //    Assert.AreEqual(39, evaluationCard.HandStrengths.CalculerTotal());
        //    Assert.AreEqual(9, (int)evaluationCard.HandStrengths.HighCard.Value);
        //}

        //[TestMethod]
        //public void TestFlushAleatoire()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Hearts);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
        //    evaluationCard = new HandEvaluator(jeuDeCarte);
        //    Hand player = evaluationCard.EvaluateHand();
        //    Assert.AreEqual(Hand.Flush, player);
        //    Assert.AreEqual(42, evaluationCard.HandStrengths.CalculerTotal());
        //    Assert.AreEqual(14, (int)evaluationCard.HandStrengths.HighCard.Value);


        //}

        //[TestMethod]
        //public void TestStraight()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
        //    jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
        //    evaluationCard = new HandEvaluator(jeuDeCarte);
        //    Hand player = evaluationCard.EvaluateHand();
        //    Assert.AreEqual(Hand.Straight, player);
        //    Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        //    Assert.AreEqual(14, (int)evaluationCard.HandStrengths.HighCard.Value);
        //}

        //[TestMethod]
        //public void TestTwoPairs()
        //{
        //    jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
        //    jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
        //    jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
        //    jeuDeCarte[3] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
        //    jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
        //    jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
        //    jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
        //    evaluationCard = new HandEvaluator(jeuDeCarte);
        //    Hand player = evaluationCard.EvaluateHand();
        //    Assert.AreEqual(Hand.TwoPairs, player);
        //}
        #endregion

        [TestMethod]
        public void TestStraightList()
        {
            TestStraightList(InitCardArr(Card.CardSuit.Clubs, 3, 4, 5, 6, 7, 9, 10),
                             new int[] { 7, 6, 5, 4, 3 });

            TestStraightList(InitCardArr(Card.CardSuit.Clubs, 3, 4, 6, 7, 9, 10),
                             null);
            
            TestStraightList(InitCardArr(Card.CardSuit.Clubs, 3, 3, 4, 5, 6, 7, 9, 10, 11, 12),
                             new int[] { 7, 6, 5, 4, 3 });

            TestStraightList(InitCardArr(Card.CardSuit.Clubs, 3, 4, 5, 6, 7, 9, 10, 11, 12, 13, 14),
                             new int[] { 14, 13, 12, 11, 10, 9 });
        }

        private void TestStraightList(Card[] cardArr, int[] expectedVals)
        {
            var privateObj = new PrivateObject(new HandEvaluator(cardArr));
            int[] actualVals = (privateObj.GetFieldOrProperty("_straightList") as List<Card>)?.Select(c => (int)c.Value)
                                                                                              .ToArray();
            CollectionAssert.AreEqual(expectedVals, actualVals);
        }


        /// <summary>
        /// Creates a Card array with the specified suit and specified values.
        /// </summary>
        /// <param name="suit">Suit to be assigned to all cards</param>
        /// <param name="values">Array of values to be assigned to all cards.</param>
        /// <returns>The newly created Card array.</returns>
        private Card[] InitCardArr(Card.CardSuit suit, params int[] values)
        {
            var arr = new Card[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                arr[i] = new Card((Card.CardRank)values[i], suit, null);
            }
            return arr;
        }
    }
}
