using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasinoUI.Models.Cards;


namespace TestCasino
{
    [TestClass]
    public class TesthandEvaluator
    {
        private Card[] jeuDeCarte;

        [TestMethod]
        public void isRoyalFlush()
        {
            jeuDeCarte[0] = new Card(Card.CardRank.Ace, Card.CardSuit.Hearts);
            
            Assert.Equals((int)jeuDeCarte[0].Value, (int)Card.CardRank.King);
        }
    }
}
