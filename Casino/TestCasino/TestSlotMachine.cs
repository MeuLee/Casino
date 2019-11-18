using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Settings;
using CasinoUI.Models.SlotMachine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasino
{
    [TestClass]
    public class TestSlotMachine
    {
        private SlotMachineLogic _model = new SlotMachineLogic(ApplicationSettings.HumanPlayer);
        private PrivateObject _slotMachineLogic = new PrivateObject(typeof(SlotMachineLogic),
                                                                    new[] { typeof(HumanPlayer)},
                                                                    new[] { ApplicationSettings.HumanPlayer });
        [TestMethod]
        public void NoIconNull()
        {
            SlotMachineIcon[,] icons = (SlotMachineIcon[,])_slotMachineLogic.GetFieldOrProperty("_columns");
            Assert.AreEqual(0, icons.Cast<SlotMachineIcon>().Count(s => s == null));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            while (true)
            {
                _model.PullLever();
            }
        }
    }
}
