using CasinoUI.Models.Blackjack;
using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using CasinoUI.Controllers;

namespace TestCasino
{
    [TestClass]
    public class TestBlackJack
    {
        PrivateObject _BlackjackLogic = new PrivateObject(typeof(BlackjackLogic),
                                            new[] { typeof(HumanPlayer), typeof(BlackjackController) },
                                            new object[] { new HumanPlayer(0, 0), null });
        IBlackjackAction ai;
        IBlackjackAction human;
        Player aiPlayer;
        Player humanPlayer;
        int Bet;
        [TestInitialize]
        public void Setup()
        {
            ai = ((List<Player>)_BlackjackLogic.GetFieldOrProperty("_players")).OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();
            human = ((List<Player>)_BlackjackLogic.GetFieldOrProperty("_players")).OfType<HumanPlayer>().First().GetGameType<IBlackjackAction>();
            aiPlayer = (Player)_BlackjackLogic.Invoke("GetAI");
            humanPlayer = (Player)_BlackjackLogic.Invoke("GetHuman");            
        }

        private void AddCardsToPlayer(Player player, params int[] values)
        {
            foreach (var cardValue in values)
            {
                player.Hand.Add(new Card((Card.CardRank)cardValue, Card.CardSuit.Clubs, null));
            }
        }

        [TestMethod]
        public void TestResetPlayers()
        {
            ai.PlayerStand = true;
            human.PlayerStand = true;
            ai.PlayerBust = true;
            human.PlayerBust = true;
            _BlackjackLogic.Invoke("ResetPlayers");
            Assert.IsFalse(ai.PlayerStand);
            Assert.IsFalse(human.PlayerStand);
            Assert.IsFalse(ai.PlayerBust);
            Assert.IsFalse(human.PlayerBust);
        }

        [TestMethod]
        public void TestCheckSoft17True()
        {
            AddCardsToPlayer(aiPlayer, 14, 6);
            bool res = (bool)_BlackjackLogic.Invoke("CheckSoft17", ai);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestCheckSoft17IsHard17()
        {
            AddCardsToPlayer(aiPlayer, 8, 8, 14);
            bool res = (bool)_BlackjackLogic.Invoke("CheckSoft17", ai);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestCheckSoft17NoAce()
        {
            AddCardsToPlayer(aiPlayer, 3, 2);
            bool res = (bool)_BlackjackLogic.Invoke("CheckSoft17", ai);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestAIPlaysStandOnHard17()
        {
            AddCardsToPlayer(aiPlayer, 1, 8, 8);
            _BlackjackLogic.Invoke("AIPlays");
            Assert.IsTrue(ai.PlayerStand);
        }

        /**
         * Test fonctionne que lorsque la ligne BJController.UpdateViewNewCardAI(); est en commentaire
         * puisque le test ne peut pas update le ui qui n'existe pas.
         **/
        //[TestMethod]
        //public void TestAIPlaysAIHitsOnSoft17()
        //{
        //    AddCardsToPlayer(aiPlayer, 1, 6);
        //    _BlackjackLogic.Invoke("AIPlays");
        //    Assert.AreEqual(3, aiPlayer.Hand.Count);
        //}

        /**
         * Idem a TestAIPlaysAIHitsOnSoft17(), BJController.UpdateViewNewCardAI(); doit etre en commentaire
         **/
        //[TestMethod]
        //public void TestAIPlaysAIHitsUnder16()
        //{
        //    AddCardsToPlayer(aiPlayer, 8, 6);
        //    _BlackjackLogic.Invoke("AIPlays");
        //    Assert.IsTrue(aiPlayer.Hand.Count > 2);
        //}

        [TestMethod]
        public void TestCheckBustAITrue ()
        {
            AddCardsToPlayer(aiPlayer, 8, 8, 8);
            _BlackjackLogic.Invoke("CheckBust", ai, aiPlayer);
            Assert.IsTrue(ai.PlayerBust);
            Assert.IsTrue(ai.PlayerStand);
        }

        [TestMethod]
        public void TestCheckBustAIFalse()
        {
            AddCardsToPlayer(aiPlayer, 8, 8, 4);
            _BlackjackLogic.Invoke("CheckBust", ai, aiPlayer);
            Assert.IsFalse(ai.PlayerBust);
            Assert.IsFalse(ai.PlayerStand);
        }

        [TestMethod]
        public void TestCheckBustHumanTrue()
        {
            AddCardsToPlayer(humanPlayer, 8, 8, 8);
            _BlackjackLogic.Invoke("CheckBust", human, humanPlayer);
            Assert.IsTrue(human.PlayerBust);
            Assert.IsTrue(human.PlayerStand);
        }

        [TestMethod]
        public void TestCheckBustHumanFalse()
        {
            AddCardsToPlayer(humanPlayer, 8, 8, 4);
            _BlackjackLogic.Invoke("CheckBust", human, humanPlayer);
            Assert.IsFalse(human.PlayerBust);
            Assert.IsFalse(human.PlayerStand);
        }

        [TestMethod]
        public void TestDistributeCards()
        {
            Assert.AreEqual(0, aiPlayer.Hand.Count);
            Assert.AreEqual(0, humanPlayer.Hand.Count);
            _BlackjackLogic.Invoke("DistributeCards");
            Assert.AreEqual(2, aiPlayer.Hand.Count);
            Assert.AreEqual(2, humanPlayer.Hand.Count);
        }

        [TestMethod]
        public void TestSetHandValueFigureCountsAs10()
        {
            AddCardsToPlayer(humanPlayer, 12);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(10, human.PlayerHandValue);
        }

        [TestMethod]
        public void TestSetHandValueAceCountsAs11()
        {
            AddCardsToPlayer(humanPlayer, 14);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(11, human.PlayerHandValue);
        }

        [TestMethod]
        public void TestSetHandValueOneAceIs11andOtherIs1()
        {
            AddCardsToPlayer(humanPlayer, 14, 14);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(12, human.PlayerHandValue);
        }

        [TestMethod]
        public void TestSetHandValueAceIs1()
        {
            AddCardsToPlayer(humanPlayer, 9, 14, 9);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(19, human.PlayerHandValue);
        }
        
        [TestMethod]
        public void TestSetHandValueTwoAcesBothAre1()
        {
            AddCardsToPlayer(humanPlayer, 14, 5, 6, 14);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(13, human.PlayerHandValue);
        }

        [TestMethod]
        public void TestSetHandValueFourAcesOnly1Is11()
        {
            AddCardsToPlayer(humanPlayer, 14, 14, 14, 14);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(14, human.PlayerHandValue);
        }

        [TestMethod]
        public void TestSetHandValuePlayerHas21()
        {
            AddCardsToPlayer(humanPlayer, 7, 7, 7);
            _BlackjackLogic.Invoke("SetHandValue", humanPlayer);
            Assert.AreEqual(21, human.PlayerHandValue);
            Assert.IsTrue(human.Has21);
        }

        [TestMethod]
        public void TestDoubleDownBetDoubles()
        {
            _BlackjackLogic.SetFieldOrProperty("Bet", 20);
            _BlackjackLogic.Invoke("DoubleDown");
            Assert.AreEqual(40, _BlackjackLogic.GetFieldOrProperty("Bet"));
        }

        /**
         *  Ici le joueur a 1000$
         **/
        [TestMethod]
        public void TestDoubleDownBetCapsAtPlayerMoney()
        {
            int StartingMoney = humanPlayer.Money;
            _BlackjackLogic.SetFieldOrProperty("Bet", 650);
            humanPlayer.Money -= (int)_BlackjackLogic.GetFieldOrProperty("Bet");
            _BlackjackLogic.Invoke("DoubleDown");
            Assert.AreEqual(0, humanPlayer.Money);
            Assert.AreEqual(StartingMoney, _BlackjackLogic.GetFieldOrProperty("Bet"));
        }

        /**
         *  Ici le joueur a 1000$
         **/
        [TestMethod]
        public void TestDoubleDownPlayerMoneyGoesDownBetDoubles()
        {
            int StartingMoney = humanPlayer.Money;
            _BlackjackLogic.SetFieldOrProperty("Bet", 200);
            humanPlayer.Money -= (int)_BlackjackLogic.GetFieldOrProperty("Bet");
            _BlackjackLogic.Invoke("DoubleDown");
            Assert.AreEqual(600, humanPlayer.Money);
            Assert.AreEqual(400, _BlackjackLogic.GetFieldOrProperty("Bet"));
        }

        [TestMethod]
        public void TestDoubleDownPlayerStands()
        {
            Assert.IsFalse(human.PlayerStand);
            _BlackjackLogic.Invoke("DoubleDown");
            Assert.IsTrue(human.PlayerStand);
        }

        [TestMethod]
        public void TestDoubleDownPlayerHandCountUpBy1()
        {
            Assert.AreEqual(0, humanPlayer.Hand.Count);
            _BlackjackLogic.Invoke("DoubleDown");
            Assert.AreEqual(1, humanPlayer.Hand.Count);
        }

        /**
         * Test va echouer que si DoubleDown donne un as au joueur. 
         * Test est juste pour s'assurer que ca arrete dans le cas ou il depasse 21 lors d'un double down
         **/
        [TestMethod]
        public void TestDoubleDownChecksBustTrue()
        {
            AddCardsToPlayer(humanPlayer, 9, 10, 2);
            _BlackjackLogic.Invoke("DoubleDown");
            Assert.IsTrue(human.PlayerBust);
        }

        [TestMethod]
        public void TestStand()
        {
            _BlackjackLogic.Invoke("Stand");
            Assert.IsTrue(human.PlayerStand);
        }

        [TestMethod]
        public void TestHitGives1Card()
        {
            Assert.AreEqual(0, humanPlayer.Hand.Count);
            _BlackjackLogic.Invoke("Hit");
            Assert.AreEqual(1, humanPlayer.Hand.Count);
        }

        [TestMethod]
        public void TestHitChecksBustAndStandTrue()
        {
            AddCardsToPlayer(humanPlayer, 10, 10);
            _BlackjackLogic.Invoke("Hit");
            Assert.IsTrue(human.PlayerBust);
            Assert.IsTrue(human.PlayerStand);
        }

        [TestMethod]
        public void TestCheckBlackJackHumanAndDealerHaveBJ()
        {
            int StartingMoney = humanPlayer.Money;
            _BlackjackLogic.SetFieldOrProperty("Bet", 200);
            humanPlayer.Money -= (int)_BlackjackLogic.GetFieldOrProperty("Bet");
            AddCardsToPlayer(humanPlayer, 7, 7, 7);
            AddCardsToPlayer(aiPlayer, 7, 7, 7);
            _BlackjackLogic.Invoke("CheckBlackJack");
            Assert.AreEqual(StartingMoney, humanPlayer.Money);
            Assert.IsTrue(human.Has21);
            Assert.IsTrue(ai.Has21);
            Assert.IsTrue(human.PlayerStand);
            Assert.IsTrue(ai.PlayerStand);
        }

        [TestMethod]
        public void TestCheckBlackJackHumanHasBJ()
        {
            _BlackjackLogic.SetFieldOrProperty("Bet", 200);
            humanPlayer.Money -= (int)_BlackjackLogic.GetFieldOrProperty("Bet");
            AddCardsToPlayer(humanPlayer, 7, 7, 7);
            AddCardsToPlayer(aiPlayer, 7, 7, 6);
            _BlackjackLogic.Invoke("CheckBlackJack");
            Assert.IsTrue(human.Has21);
            Assert.IsFalse(ai.Has21);
            Assert.AreEqual(1300, humanPlayer.Money);
        }

        [TestMethod]
        public void TestCheckForWinnerAIBusted()
        {
            ai.PlayerBust = true;
            String Winner = (String)_BlackjackLogic.Invoke("CheckForWinner");
            Assert.AreEqual("Player Wins", Winner);
        }
        
        [TestMethod]
        public void TestCheckForWinnerHumanBusted()
        {
            human.PlayerBust = true;
            String Winner = (String)_BlackjackLogic.Invoke("CheckForWinner");
            Assert.AreEqual("Dealer Wins", Winner);
        }

        [TestMethod]
        public void TestCheckForWinnerDraw()
        {
            AddCardsToPlayer(humanPlayer, 10, 7);
            AddCardsToPlayer(aiPlayer, 6, 7, 4);
            String Winner = (String)_BlackjackLogic.Invoke("CheckForWinner");
            Assert.AreEqual("Draw", Winner);
        }

        [TestMethod]
        public void TestCheckForWinnerIsPlayer()
        {
            AddCardsToPlayer(humanPlayer, 10, 12);
            AddCardsToPlayer(aiPlayer, 6, 7, 4);
            String Winner = (String)_BlackjackLogic.Invoke("CheckForWinner");
            Assert.AreEqual("Player Wins", Winner);
        }

        [TestMethod]
        public void TestCheckForWinnerIsDealer()
        {
            AddCardsToPlayer(humanPlayer, 10, 12);
            AddCardsToPlayer(aiPlayer, 6, 7, 4, 4);
            String Winner = (String)_BlackjackLogic.Invoke("CheckForWinner");
            Assert.AreEqual("Dealer Wins", Winner);
        }
    }
}
