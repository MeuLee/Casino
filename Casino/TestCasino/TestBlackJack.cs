﻿using CasinoUI.Models.Blackjack;
using CasinoUI.Models.Cards;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Settings;
using CasinoUI.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Player aiPlayer;
        [TestInitialize]
        public void Setup()
        {
            ai = ((List<Player>)_BlackjackLogic.GetFieldOrProperty("_players")).OfType<BlackjackAI>().First().GetGameType<IBlackjackAction>();
            aiPlayer = (Player)_BlackjackLogic.Invoke("GetAI");
        }

        [TestMethod]
        public void TestCheckSoft17True()
        {      
            aiPlayer.GetHand().Add(new Card(Card.CardRank.Ace, Card.CardSuit.Clubs, null));
            aiPlayer.GetHand().Add(new Card(Card.CardRank.Six, Card.CardSuit.Clubs, null));
            bool res = (bool)_BlackjackLogic.Invoke("CheckSoft17", ai);
            Assert.IsTrue(res);
        }


        
    }
}
