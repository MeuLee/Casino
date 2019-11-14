using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Models.Cards;
using CasinoUI.Models.Poker;


namespace TestCasino
{
    [TestClass]
    public class TesthandEvaluator
    {
        private Card[] jeuDeCarte;
        private HandEvaluator evaluationCard; 

        [TestMethod]
        public void isRoyalFlush()
        {
            jeuDeCarte = new Card[7];
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
            Assert.AreEqual(evaluationCard.HandStrengths.Total, 24);
        }
    }
}
