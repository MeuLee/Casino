using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker;
using CasinoUI.Models.Poker.Evaluator;

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


    }
}
