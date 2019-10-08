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
        MapTile[,] _map = MapGenerator.LoadMapFromFile(CasinoUI.Properties.Resources.map);

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

        [TestMethod]
        public void TestSetCameraCenterValue()
        {
            PrivateType mapRenderer = new PrivateType(typeof(MapRenderer));
            Type[] types = { typeof(int), typeof(int), typeof(int) };
            TestCamera(mapRenderer, types, new object[] { 0, 5, 19 }, 5); // near left or top edge
            TestCamera(mapRenderer, types, new object[] { 7, 5, 22 }, 7); // not near any edge
            TestCamera(mapRenderer, types, new object[] { 19, 5, 22 }, 16); // near bottom or right edge
        }

        private void TestCamera(
            PrivateType mapRenderer,
            Type[] types,
            object[] args, 
            int expected)
        {
            int actual = (int)mapRenderer.InvokeStatic("SetCameraCenterValue", types, args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPlayerCoordOnUI()
        {
            // Test MapRenderer.PlayerCoordOnUI
        }

        [TestMethod]
        public void TestSetTileSize()
        {
            // Test MapRenderer.SetTileSize
        }
    }
}
