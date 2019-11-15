﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker;
using CasinoUI.Models.Poker.Evaluator;
using System;

namespace TestCasino
{
    [TestClass]
    public class TesthandEvaluator
    {
        private Card[] jeuDeCarte;
        private HandEvaluator evaluationCard;

        [TestInitialize]
        public void TestInitialize()
        {
            jeuDeCarte = new Card[7];
        }

        [TestMethod]
        public void isRoyalFlush()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Hearts);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Hearts);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Hearts);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Hearts);
            jeuDeCarte[5] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Diamonds);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.RoyalFlush, player);
            Assert.AreEqual(24, evaluationCard.HandStrengths.Total);
        }

        [TestMethod]
        public void isRoyalFlushAleatoire()
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
            Assert.AreEqual(24, evaluationCard.HandStrengths.Total);
        }

        [TestMethod]
        public void isStraightFlushPosFin()
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
            Assert.AreEqual(22, evaluationCard.HandStrengths.Total);
        }

        [TestMethod]
        public void isStraightFlushPosMilieu()
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
            Assert.AreEqual(22, evaluationCard.HandStrengths.Total);
        }

        [TestMethod]
        public void isStraightFlushPosDebut()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Two, Card.CardSuit.Spades);
            jeuDeCarte[1] = new Card(Card.CardRank.King, Card.CardSuit.Diamonds);
            jeuDeCarte[2] = new Card(Card.CardRank.Queen, Card.CardSuit.Diamonds);
            jeuDeCarte[3] = new Card(Card.CardRank.Jack, Card.CardSuit.Diamonds);
            jeuDeCarte[4] = new Card(Card.CardRank.Ten, Card.CardSuit.Diamonds);
            jeuDeCarte[5] = new Card(Card.CardRank.Nine, Card.CardSuit.Diamonds);
            jeuDeCarte[6] = new Card(Card.CardRank.Five, Card.CardSuit.Clubs);

            evaluationCard = new HandEvaluator(jeuDeCarte);

            Hand player = evaluationCard.EvaluateHand();

            Assert.AreEqual(Hand.StraightFlush, player);
            Assert.AreEqual(22, evaluationCard.HandStrengths.Total);
        }

        [TestMethod]
        public void TestTwoPairs()
        {
            jeuDeCarte = InitializeCardArr(Card.CardSuit.Clubs, 9, 9, 8, 3, 8, 5, 4);
            evaluationCard = new HandEvaluator(jeuDeCarte);
            Assert.IsTrue(evaluationCard.HandHasPairs(2, 2));
        }

        [TestMethod]
        public void TestFullHouse()
        {
            jeuDeCarte = InitializeCardArr(Card.CardSuit.Clubs, 14, 14, 13, 13, 13, 10, 9);
            evaluationCard = new HandEvaluator(jeuDeCarte);
            Assert.IsTrue(evaluationCard.FullHouse());
        }

        /// <summary>
        /// Creates a Card array with the specified suit and specified values.
        /// </summary>
        /// <param name="suit">Suit to be assigned to all cards</param>
        /// <param name="values">Array of values to be assigned to all cards.</param>
        /// <returns>The newly created Card array.</returns>
        private Card[] InitializeCardArr(Card.CardSuit suit, params int[] values)
        {
            if (values.Length != 7)
            {
                throw new IndexOutOfRangeException("values.Length must be equal to 7");
            }
            var arr = new Card[7];
            for (int i = 0; i < values.Length; i++)
            {
                arr[i] = new Card((Card.CardRank)values[i], suit, null);
            }
            return arr;
        }
    }
}