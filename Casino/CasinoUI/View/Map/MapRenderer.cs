using CasinoUI.View.Map.Tiles;
using System;
using System.Windows;
using System.Windows.Media;

namespace CasinoUI.View.Map
{
    public class MapRenderer
    {
        public static MapTile[,] Map { get; private set; } = MapGenerator.LoadMapFromFile(Properties.Resources.map);

        public static int TilesAroundPlayerX { get; set; } = 15;
        public static int TilesAroundPlayerY { get; set; } = 7;

        private static int _cameraCenterX, _cameraCenterY;

        // Will be removed once the HumanPlayer instance can be accesssed.
        public static int PlayerX { get; set; } = 10;
        public static int PlayerY { get; set; } = 4;

        public static void RenderMap(float canvasWidth, float canvasHeight, DrawingContext dc)
        {
            SetCameraCenterValues(PlayerX, PlayerY);
            float tileWidth = SetTileWidth(canvasWidth, canvasHeight);
            DrawTiles(dc, tileWidth);
            DrawPlayer(dc, tileWidth);
        }

        private static void SetCameraCenterValues(int playerX, int playerY)
        {
            _cameraCenterX = SetCameraCenterValue(playerX, TilesAroundPlayerX, 0);
            _cameraCenterY = SetCameraCenterValue(playerY, TilesAroundPlayerY, 1);
        }

        private static int SetCameraCenterValue(int playerCoord, int tilesAroundPlayer, int dimension)
        {
            if (0 > playerCoord - tilesAroundPlayer)
            {
                return tilesAroundPlayer;
            }
            else if (playerCoord + tilesAroundPlayer > Map.GetLength(dimension) - 1)
            {
                return Map.GetLength(dimension) - 1 - tilesAroundPlayer;
            }
            else
            {
                return playerCoord;
            }
        }

        private static float SetTileWidth(float canvasWidth, float canvasHeight)
        {
            return Math.Min(canvasWidth / (TilesAroundPlayerX * 2 + 1),
                            canvasHeight / (TilesAroundPlayerY * 2 + 1));
        }

        private static void DrawTiles(DrawingContext dc, float tileWidth)
        {
            //i: x coord on the (entire) map. index: x coord on the visible map.
            for (int i = _cameraCenterX - TilesAroundPlayerX, index = 0;
                 i <= _cameraCenterX + TilesAroundPlayerX;
                 i++, index++)
            {
                //j: y coord on the (entire) map. jndex: y coord on the visible map.
                for (int j = _cameraCenterY - TilesAroundPlayerY, jndex = 0;
                     j <= _cameraCenterY + TilesAroundPlayerY;
                     j++, jndex++)
                {
                    MapTile tile = Map[i, j];
                    dc.DrawImage(tile.Sprite, new Rect(index * tileWidth, jndex * tileWidth, tileWidth, tileWidth));
                }
            }
        }

        private static void DrawPlayer(DrawingContext dc, float tileWidth)
        {
            int uiPlayerX = PlayerCoordOnUI(_cameraCenterX, PlayerX, 0, TilesAroundPlayerX),
                uiPlayerY = PlayerCoordOnUI(_cameraCenterY, PlayerY, 1, TilesAroundPlayerY);
            dc.DrawEllipse(Brushes.Green,
                           new Pen(Brushes.Green, 1.0),
                           new Point(uiPlayerX * tileWidth + tileWidth / 2,
                                     uiPlayerY * tileWidth + tileWidth / 2),
                           tileWidth / 3,
                           tileWidth / 3);
        }

        private static int PlayerCoordOnUI(int cameraCenterCoord, int playerCoord, int dimension, int tilesAroundPlayerCoord)
        {
            if (cameraCenterCoord > playerCoord)
            {
                return playerCoord;
            }
            else if (playerCoord > Map.GetLength(dimension) - 1 - tilesAroundPlayerCoord)
            {
                return tilesAroundPlayerCoord * 2 - (Map.GetLength(dimension) - 1 - playerCoord);
            }
            else
            {
                return tilesAroundPlayerCoord;
            }
        }
    }
}
