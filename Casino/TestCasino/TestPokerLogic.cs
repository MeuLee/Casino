using System;
using System.Collections.Generic;
using System.Drawing;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Poker.PokerBrains;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCasino
{
    [TestClass]
    public class TestPokerLogic {
        private HumanPlayer humanPlayer;
        
        

        private PokerLogic pokerLogic;

        public TestPokerLogic()
        {
            humanPlayer = new HumanPlayer(0, 0);

            pokerLogic = new PokerLogic(humanPlayer);

        }

        [TestMethod]
        public void TestCardDistribution() {
            List<Player> listPlayers;
            bool everyoneHas2Cards = true;

            listPlayers = pokerLogic.ListPlayers;

            foreach (Player player in listPlayers) {
                if (player.Hand.Count != 2) {
                    everyoneHas2Cards = false;
                    break;
                }
            }

            Assert.IsTrue(everyoneHas2Cards);
        }

        [TestMethod]
        public void TestProceedNextGame() {
            int beforeSmallBlindIdx = pokerLogic.PlayerRoles[0];
            int beforeBigBlindIdx = pokerLogic.PlayerRoles[1];
            int beforeFirstPlayerIdx = pokerLogic.PlayerRoles[2];

            pokerLogic.ProceedNextGame();

            Assert.AreEqual(beforeSmallBlindIdx + 1, pokerLogic.PlayerRoles[0]);
            Assert.AreEqual(beforeBigBlindIdx + 1, pokerLogic.PlayerRoles[1]);
            Assert.AreEqual(beforeFirstPlayerIdx + 1, pokerLogic.PlayerRoles[2]);

        }
    }
}
