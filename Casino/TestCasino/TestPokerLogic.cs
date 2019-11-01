using System;
using System.Collections.Generic;
using CasinoUI.Model;
using CasinoUI.Model.Cards;
using CasinoUI.Model.Poker;
using CasinoUI.Models;
using CasinoUI.Models.Poker.PokerBrains;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCasino {
    [TestClass]
    public class TestPokerLogic {
        private HumanPlayer humanPlayer;
        private PokerLogic PokerLogic;

        public TestPokerLogic() {
            humanPlayer = new HumanPlayer();
            Player p = new PokerPlayer();
            //if (p is PokerPlayer)
            //{
            //    PokerPlayer pokerPlayer = p as PokerPlayer;
            //    PokerLogic = new PokerLogic(pokerPlayer);
            //}
        }

        [TestMethod]
        public void TestCardDistribution() {
            Console.WriteLine("Allo");
        }
    }
}
