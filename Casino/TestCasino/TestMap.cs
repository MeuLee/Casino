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
            //Arrange
            int nbTileNull;

            //Act
            MapTile[,] map = MapGenerator.LoadMapFromFile(CasinoUI.Properties.Resources.map);
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

        [TestMethod]
        public void NoImageNullMap()
        {
            //Arrange
            int nbImageNull;

            //Act
            MapTile[,] map = MapGenerator.LoadMapFromFile(CasinoUI.Properties.Resources.map);
            nbImageNull = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j].Sprite == null)
                    {
                        nbImageNull++;
                    }
                }
            }

            //Assert
            Assert.AreEqual(0, nbImageNull);
        }
    }
}
