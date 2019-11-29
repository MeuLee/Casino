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
        private Card[] mainExpected;
        private HandEvaluator evaluationCard;

        [TestInitialize]
        public void TestInitialize()
        {
            jeuDeCarte = new Card[7];
            mainExpected = new Card[5];
        }

        [TestMethod]
        public void TestRoyalFlush()
        {            
            jeuDeCarte[0] = mainExpected[4] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);            

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.RoyalFlush, player);
        }

        [TestMethod]
        public void TestRoyalFlushMoreFlush()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.RoyalFlush, player);
        }

        [TestMethod]
        public void TestRoyalFlushTotalHandStrengths()
        {
            jeuDeCarte[0] = mainExpected[4] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestRoyalFlushHandPlayer()
        {
            jeuDeCarte[0] = mainExpected[4] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestRoyalFlushAleatoire()
        {
            jeuDeCarte[5] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.RoyalFlush, player);
        }

        [TestMethod]
        public void TestRoyalFlushAleatoireHandStrentghs()
        {
            jeuDeCarte[5] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestRoyalFlushAleatoireHandPlayer()
        {
            jeuDeCarte[5] = mainExpected[4] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[0] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = mainExpected[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[6] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[2] = mainExpected[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }


        [TestMethod]
        public void TestRoyalStraight()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.Straight, player);
        }

        [TestMethod]
        public void TestRoyalStraightHandStrenghts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestRoyalStraightHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(14, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestRoyalStraightHandPlayer()
        {
            jeuDeCarte[0] = mainExpected[4] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = mainExpected[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestStraightFlushHigh()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.StraightFlush, player);
        }

        [TestMethod]
        public void TestStraightFlushMoreStraight()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Eight, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.StraightFlush, player);
        }

        [TestMethod]
        public void TestStraightFlushHighHandStrenghts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(55, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestStraightFlushHighHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(13, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestStraightFlushHighPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[4] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[0] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestStraightFlushMiddle()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Seven, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Six, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.StraightFlush, player);
        }

        [TestMethod]
        public void TestStraightFlushMiddleHandStrenghts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Seven, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Six, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(40, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestStraightFlushMiddleHighcard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Seven, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Six, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestStraightFlushMiddlePlayerHand()
        {
            jeuDeCarte[0] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[3] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Seven, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[0] = new Card(Card.CardRank.Six, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestStraightFlushLow()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Four, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Three, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Six, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.StraightFlush, player);
        }

        [TestMethod]
        public void TestStraightFlushLowHandStrengths()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Four, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Three, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Six, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(20, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestStraightFlushLowHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Four, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Three, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Six, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(6, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestStraightFlushLowPlayerHand()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[0] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = mainExpected[3] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[2] = new Card(Card.CardRank.Four, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = mainExpected[1] = new Card(Card.CardRank.Three, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = mainExpected[4] = new Card(Card.CardRank.Six, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestStraightAndFlushFlushBetter()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.Flush, player);
        }

        [TestMethod]
        public void TestStraightAndFlushFlushBetterHandStrenghts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(44, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestStraightAndFlushFlushBetterHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(12, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestStraightAndFlushFlushBetterPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[2] = mainExpected[4] = new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = mainExpected[2] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = mainExpected[1] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFourOfKind()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.FourKind, player);
        }

        [TestMethod]
        public void TestFourOfKindHandStrenghts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(66, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestFourOfKindHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestFourOfKindPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFourOfKindLow()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.FourKind, player);
        }

        [TestMethod]
        public void TestFourOfKindLowHandStrengts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(18, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestFourOfKindLowHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestFourOfKindLowPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFourOfKindAboveThreeKind()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.FourKind, player);
        }

        [TestMethod]
        public void TestFourOfKindAboveThreeKindHandStrenghts()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(57, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestFourOfKindAboveThreeKindHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(13, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestFourOfKindAboveThreeKindPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = mainExpected[4] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFullHouse()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.FullHouse, player);
        }

        [TestMethod]
        public void TestFullHouseHandStrenght()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(37, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestFullHouseHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(11, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestFullHousePlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[3] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = mainExpected[4] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFullHouseTwoKindTakeHigher()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.FullHouse, player);
        }


        [TestMethod]
        public void TestFullHouseTwoKindTakeHigherHandStrength()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(59, evaluationCard.HandStrengths.CalculerTotal());
        }


        [TestMethod]
        public void TestFullHouseTwoKindTakeHigherHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(11, (int)evaluationCard.HandStrengths.HighCard.Value);
        }


        [TestMethod]
        public void TestFullHouseTwoKindTakeHigherPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = mainExpected[4] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFlush()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.Flush, player);
        }

        [TestMethod]
        public void TestFlushHandStrength()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(41, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestFlushHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[2] = new Card(Card.CardRank.Five, Card.CardSuit.Spades);
            jeuDeCarte[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(13, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestFlushPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Five, Card.CardSuit.Spades);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = mainExpected[4] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.King, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestFlushMoreCard()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Jack, Card.CardSuit.Spades);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Ten, Card.CardSuit.Spades);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Five, Card.CardSuit.Spades);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.King, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = mainExpected[4] = new Card(Card.CardRank.Three, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestStraight()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.Straight, player);
        }

        [TestMethod]
        public void TestStraightHandStrength()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(60, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestStraightHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(14, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestStraightPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Jack, Card.CardSuit.Clubs);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestStraightMoreCard()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Clubs);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Eight, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestThreeOfKind()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.ThreeKind, player);
        }

        [TestMethod]
        public void TestThreeOfKindHandStrenght()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(64, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestThreeOfKindHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(12, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestThreeOfKindPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestThreeOfKindLow()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Three, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.ThreeKind, player);
        }

        [TestMethod]
        public void TestThreeOfKindLowHandStrenght()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Three, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(28, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestThreeOfKindLowHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Three, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(12, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestThreeOfKindLowPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Two, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Queen, Card.CardSuit.Spades);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Three, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestTwoPairs()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.TwoPairs, player);
        }

        [TestMethod]
        public void TestTwoPairsHandStrength()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(56, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestTwoPairsHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestTwoPairsPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Nine, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestOnePairs()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.OnePair, player);
        }

        [TestMethod]
        public void TestOnePairsHandStrength()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(55, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestOnePairsHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestOnePairsPlayerHand()
        {
            jeuDeCarte[0] = mainExpected[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Clubs);
            jeuDeCarte[1] = mainExpected[1] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        [TestMethod]
        public void TestNothing()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Six, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.Nothing, player);
        }

        [TestMethod]
        public void TestNothingHandStrength()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Six, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(38, evaluationCard.HandStrengths.CalculerTotal());
        }

        [TestMethod]
        public void TestNothingHighCard()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Six, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(10, (int)evaluationCard.HandStrengths.HighCard.Value);
        }

        [TestMethod]
        public void TestNothingPlayerHand()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Three, Card.CardSuit.Clubs);
            jeuDeCarte[1] = new Card(Card.CardRank.Two, Card.CardSuit.Hearts);
            jeuDeCarte[2] = mainExpected[0] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = mainExpected[1] = new Card(Card.CardRank.Eight, Card.CardSuit.Hearts);
            jeuDeCarte[4] = mainExpected[2] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = mainExpected[3] = new Card(Card.CardRank.Six, Card.CardSuit.Spades);
            jeuDeCarte[6] = mainExpected[4] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            TestVerifierHand(mainExpected, evaluationCard.HandStrengths.HandPlayer);
        }

        /// <summary>
        /// Tester deux main pour savoir s'il elles sont pareil
        /// </summary>
        /// <param name="cardArr">Premier main a comparer</param>
        /// <param name="cardExpected">Deuxieme main a comparer</param>
        private void TestVerifierHand(Card[] cardArr, Card[] cardExpected)
        {
            Assert.IsTrue(Enumerable.SequenceEqual(cardArr.OrderBy(c => c.Suit).ThenBy(c => c.Value), cardExpected.OrderBy(c => c.Suit).ThenBy(c => c.Value)));
        }
    }
}
