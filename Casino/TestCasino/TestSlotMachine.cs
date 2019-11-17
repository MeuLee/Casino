using CasinoUI.Models.SlotMachine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasino
{
    [TestClass]
    public class TestSlotMachine
    {
        [TestMethod]
        public void MyTestMethod()
        {
            // how test random
            while (true)
            {
                new SlotMachineLogic(null);
            }
        }
    }
}
