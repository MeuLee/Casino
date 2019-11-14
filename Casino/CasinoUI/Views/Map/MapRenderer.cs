using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.Settings;
using CasinoUI.Views.Map.Tiles;
using System;
using System.Windows;
using System.Windows.Media;

namespace CasinoUI.Views.Map
{
    public static class MapRenderer
    {

        public static int TilesAroundPlayerX { get; set; } = 5;
        public static int TilesAroundPlayerY { get; set; } = 3;

        private static int _cameraCenterX, _cameraCenterY;
        private static readonly HumanPlayer _player = ApplicationSettings.HumanPlayer;
        private static readonly MapTile[,] _map = ApplicationSettings.Map;
        
        #region RenderMap
        public static void RenderMap(DrawingContext dc, float canvasWidth, float canvasHeight)
        {
            SetCameraCenterValues();
            float tileWidth = SetMapTileSize(canvasWidth, canvasHeight);
            DrawTiles(dc, tileWidth);
            DrawPlayer(dc, tileWidth);
        }

        /// <summary>
        /// Sets the x, y point in the entire map where the camera will be centered on.
        /// </summary>
        private static void SetCameraCenterValues()
        {
            _cameraCenterX = SetCameraCenterValue(_player.X, TilesAroundPlayerX, _map.GetLength(0));
            _cameraCenterY = SetCameraCenterValue(_player.Y, TilesAroundPlayerY, _map.GetLength(1));
        }

        /// <summary>
        /// Sets a camera's coordinate (x or y).
        /// </summary>
        /// <param name="playerCoord">Player's X or Y</param>
        /// <param name="tilesAroundPlayer">Tiles around player, x or y</param>
        /// <returns>
        /// Most of the time, playerCoord will be returned.
        /// However, if playerCoord is closer to the edge than tilesAroundPlayer, 
        /// the value returned will be either edge + tilesAroundPlayer (if) or edge - tilesAroundPlayer (else if).
        /// </returns>
        private static int SetCameraCenterValue(
            int playerCoord,
            int tilesAroundPlayer, 
            int mapLength)
        {
            if (0 > playerCoord - tilesAroundPlayer)
            {
                return tilesAroundPlayer;
            }
            else if (playerCoord + tilesAroundPlayer > mapLength - 1)
            {
                return mapLength - 1 - tilesAroundPlayer;
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
        private static float SetMapTileSize(float canvasWidth, float canvasHeight)
        {
            return Math.Min(canvasWidth / (TilesAroundPlayerX * 2 + 1),
                            canvasHeight / (TilesAroundPlayerY * 2 + 1));
        }

        /// <summary>
        /// Draws each tile present in the camera's sight.
        /// </summary>
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
                    MapTile tile = _map[i, j];
                    dc.DrawImage(tile.Sprite, new Rect(index * tileWidth, jndex * tileWidth, tileWidth, tileWidth));
                }
            }
        }

        private static void DrawPlayer(DrawingContext dc, float tileWidth)
        {
            int uiPlayerX = PlayerCoordOnUI(_cameraCenterX, _player.X, _map.GetLength(0), TilesAroundPlayerX),
                uiPlayerY = PlayerCoordOnUI(_cameraCenterY, _player.Y, _map.GetLength(1), TilesAroundPlayerY);
            dc.DrawImage(ApplicationSettings.HumanPlayer.CurrentImage, 
                         new Rect(uiPlayerX * tileWidth,
                                  uiPlayerY * tileWidth, 
                                  tileWidth, 
                                  tileWidth));
        }

        private static int PlayerCoordOnUI(
            int cameraCenterCoord, 
            int playerCoord, 
            int mapLength, 
            int tilesAroundPlayerCoord)
        {
            if (cameraCenterCoord > playerCoord)
            {
                return playerCoord;
            }
            else if (playerCoord > mapLength - 1 - tilesAroundPlayerCoord)
            {
                return tilesAroundPlayerCoord * 2 - (mapLength - 1 - playerCoord);
            }
            else
            {
                return tilesAroundPlayerCoord;
            }
        }
        #endregion

        #region RenderMiniMap
        public static void RenderMiniMap(DrawingContext dc, float canvasWidth, float canvasHeight)
        {
            float tileWidth = SetMiniMapTileSize(canvasWidth, canvasHeight);
            DrawMiniMapTiles(dc, tileWidth);
            DrawPlayerOnMiniMap(dc, tileWidth);
            SetCameraCenterValues();
            DrawCameraSight(dc, tileWidth);
        }

        private static float SetMiniMapTileSize(float canvasWidth, float canvasHeight)
        {
            return Math.Min(canvasWidth / _map.GetLength(0),
                            canvasHeight / _map.GetLength(1));
        }

        private static void DrawMiniMapTiles(DrawingContext dc, float tileWidth)
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    MapTile t = _map[i, j];
                    dc.DrawRectangle(t.MiniMapBrush, t.MiniMapPen, new Rect(i * tileWidth, j * tileWidth, tileWidth, tileWidth));
                }
            }
        }

        private static void DrawPlayerOnMiniMap(DrawingContext dc, float tileWidth)
        {
            dc.DrawEllipse(Brushes.Green,
                            new Pen(Brushes.Green, 1.0),
                            new Point(_player.X * tileWidth + tileWidth / 2,
                                      _player.Y * tileWidth + tileWidth / 2),
                            tileWidth / 3,
                            tileWidth / 3);
        }

        private static void DrawCameraSight(DrawingContext dc, float tileWidth)
        {
            Pen p = new Pen(Brushes.White, 0.75);
            Point p1 = new Point((_cameraCenterX - TilesAroundPlayerX) * tileWidth,
                                 (_cameraCenterY - TilesAroundPlayerY) * tileWidth);
            Point p2 = new Point((_cameraCenterX + 1 + TilesAroundPlayerX) * tileWidth ,
                                 (_cameraCenterY + 1 + TilesAroundPlayerY) * tileWidth);
            dc.DrawRectangle(null, p, new Rect(p1, p2));
        }
        #endregion
    }
}
