using CasinoUI.View.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasino
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void DebugMap()
        {
            MapGenerator.CreateMap(10, 20);
        }
    }
}
