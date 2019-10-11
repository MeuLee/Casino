using System;
using System.Collections.Generic;
using CasinoUI.Model;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCasino {
    [TestClass]
    public class TestPokerLogic {
        private HumanPlayer humanPlayer;
        private PokerLogic PokerLogic;

        public TestPokerLogic() {
            humanPlayer = new HumanPlayer();
            PokerLogic = new PokerLogic(humanPlayer);
        }

        [TestMethod]
        public void TestCardDistribution() {
            Console.WriteLine("Allo");
        }
    }
}
