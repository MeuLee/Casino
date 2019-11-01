using CasinoUI.Views.Map;
using CasinoUI.Views.Map.Tiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCasino
{
    [TestClass]
    public class TestMap
    {
        static readonly MapTile[,] _map = MapGenerator.LoadMapFromFile(CasinoUI.Properties.Resources.map);

        [TestMethod]
        public void NoTileNullMap()
        {
            int nbTileNull = 0;
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    if (_map[i, j] == null)
                    {
                        nbTileNull++;
                    }
                }
            }
            Assert.AreEqual(0, nbTileNull);
        }

        [TestMethod]
        public void NoImageNullMap()
        {
            MapTile[,] map = MapGenerator.LoadMapFromFile(CasinoUI.Properties.Resources.map);
            int nbImageNull = 0;
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
            Assert.AreEqual(0, nbImageNull);
        }
    }
}
