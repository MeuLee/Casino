using CasinoUI.Views.Map;
using CasinoUI.Views.Map.Tiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace TestCasino.Views.Map
{
    [TestClass]
    public class TestMap
    {
        private static readonly MapTile[,] _map = MapGenerator.LoadMapFromFile(CasinoUI.Properties.Resources.map);
        private static readonly PrivateType _mapRenderer = new PrivateType(typeof(MapRenderer));

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

        [TestMethod]
        public void TestCameraCenterValues()
        {
            TestCameraCenterValue(5, 5, 5, 20); // center screen
            TestCameraCenterValue(5, 3, 5, 20); // near left/top border
            TestCameraCenterValue(16, 19, 5, 22); // near right/bottom border
        }

        private void TestCameraCenterValue(int expected, int playerCoord, int tilesAroundPlayer, int mapLength)
        {
            int actual = (int)_mapRenderer.InvokeStatic("SetCameraCenterValue",
                                                        BindingFlags.Static | BindingFlags.NonPublic,
                                                        playerCoord, tilesAroundPlayer, mapLength);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMapTileSizes()
        {
            TestMapTileSize(42.86f, 500, 300);
            TestMapTileSize(40.91f, 450, 300);
        }

        private void TestMapTileSize(float expected, float width, float height)
        {
            float actual = (float)_mapRenderer.InvokeStatic("SetMapTileSize",
                                                            BindingFlags.NonPublic | BindingFlags.Static,
                                                            width, height);
            Assert.AreEqual(Math.Round(expected, 2), Math.Round(actual, 2));
        }

        [TestMethod]
        public void TestPlayerCoordOnUIs()
        {
            TestPlayerCoordOnUI(5, 10, 10, 50, 5); // centered
            TestPlayerCoordOnUI(2, 3, 2, 40, 3); // near left/top edge
            TestPlayerCoordOnUI(5, 36, 38, 40, 3); // near right/bottom edge
        }

        private void TestPlayerCoordOnUI(int expected, params object[] args)
        {
            int actual = (int)_mapRenderer.InvokeStatic("PlayerCoordOnUI",
                                                        BindingFlags.NonPublic | BindingFlags.Static,
                                                        args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMiniMapTileSizes()
        {
            _mapRenderer.SetStaticField("_map",
                                        BindingFlags.NonPublic | BindingFlags.Static,
                                        _map);
            TestMiniMapTileSize(6, 300, 250);
            TestMiniMapTileSize(1, 50, 50);
        }

        private void TestMiniMapTileSize(float expected, float width, float height)
        {
            float actual = (float)_mapRenderer.InvokeStatic("SetMiniMapTileSize",
                                                        BindingFlags.NonPublic | BindingFlags.Static,
                                                        width, height);
            Assert.AreEqual(expected, actual);
        }
    }
}
