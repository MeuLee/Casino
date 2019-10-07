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
            SetCameraCenterValues();
            float tileWidth = SetTileSize(canvasWidth, canvasHeight);
            DrawTiles(dc, tileWidth);
            DrawPlayer(dc, tileWidth);
        }

        /// <summary>
        /// Sets the x, y point in the entire map where the camera will be centered on.
        /// </summary>
        private static void SetCameraCenterValues()
        {
            _cameraCenterX = SetCameraCenterValue(PlayerX, TilesAroundPlayerX, 0);
            _cameraCenterY = SetCameraCenterValue(PlayerY, TilesAroundPlayerY, 1);
        }

        /// <summary>
        /// Sets a camera's coordinate (x or y).
        /// </summary>
        /// <param name="playerCoord">Player's X or Y</param>
        /// <param name="tilesAroundPlayer">Tiles around player, x or y</param>
        /// <param name="dimension">Should be 0 or 1 as the map is 2d. 0 for horizontal (x), 1 for vertical (y)</param>
        /// <returns>
        /// Most of the time, playerCoord will be returned.
        /// However, if playerCoord is closer to the edge than tilesAroundPlayer, 
        /// the value returned will be either edge + tilesAroundPlayer (if) or edge - tilesAroundPlayer (else if).
        /// </returns>
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

        /// <summary>
        /// Divides the available space (height or width) by the camera size (x or y) 
        /// and returns the lowest value.
        /// The whole map will fit in the canvas
        /// </summary>
        private static float SetTileSize(float canvasWidth, float canvasHeight)
        {
            return Math.Min(canvasWidth / (TilesAroundPlayerX * 2 + 1),
                            canvasHeight / (TilesAroundPlayerY * 2 + 1));
        }

        /// <summary>
        /// Draws each tile present in the camera's sight.
        /// </summary>
        private static void DrawTiles(DrawingContext dc, float tileSize)
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
                    dc.DrawImage(tile.Sprite, new Rect(index * tileSize, jndex * tileSize, tileSize, tileSize));
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
