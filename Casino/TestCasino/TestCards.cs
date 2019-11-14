using System.Linq;
using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCasino
{
    [TestClass]
    public class TestCards
    {
        private readonly CardStack _stack = new GameCardStack();
        [TestMethod]
        public void Generated52Cards()
        {
            Assert.AreEqual(52, _stack.Cards.Count);
        }

        [TestMethod]
        public void NoImageNull()
        {
            int nbImageNull = _stack.Cards.Count(c => c.Image == null);

            Assert.AreEqual(0, nbImageNull);
        }

        [TestMethod]
        public void NoDuplicateCard()
        {
            //Arrange
            int nbUniqueCards;

            //Act
            nbUniqueCards = _stack.Cards.Distinct().Count();

            //Assert
            Assert.AreEqual(52, nbUniqueCards);
        }

        [TestMethod]
        public void DrawProperly()
        {
            //Arrange
            GameCardStack stack = new GameCardStack();
            HumanPlayer player = new HumanPlayer(0, 0);
            int playerCardsBefore = player.GetHand().Count, gameCardsBefore = stack.Cards.Count,
                playerCardsAfter, gameCardsAfter;

            //Act
            stack.PlayerDrawCard(player);
            playerCardsAfter = player.GetHand().Count;
            gameCardsAfter = stack.Cards.Count;

            //Assert
            Assert.AreEqual(playerCardsBefore + 1, playerCardsAfter);
            Assert.AreEqual(gameCardsBefore - 1, gameCardsAfter);
        }
    }
}
