using CasinoUI.View.Map;
using CasinoUI.View.Map.Tiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasino
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void NoTileNullMap()
        {
            NoTileNull(CasinoUI.Properties.Resources.map);
        }

        private void NoTileNull(string mapContent)
        {
            //Arrange
            int nbTileNull;

            //Act
            MapTile[,] map = MapGenerator.LoadMapFromFile(mapContent);
            nbTileNull = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == null)
                    {
                        nbTileNull++;
                    }
                }
            }

            //Assert
            Assert.AreEqual(0, nbTileNull);
        }
    }
}
