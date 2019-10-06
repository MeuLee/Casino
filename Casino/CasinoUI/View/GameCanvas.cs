using CasinoUI.View.Map;
using CasinoUI.View.Map.Tiles;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CasinoUI.View
{
    public class GameCanvas : Canvas
    {
        public MapTile[,] _map { get; private set; } = MapGenerator.LoadMapFromFile(Properties.Resources.map);
        private const int TILES_AROUND_PLAYER_X = 5,
                          TILES_AROUND_PLAYER_Y = 3;
        private int _cameraCenterX, _cameraCenterY;

        public int PlayerX { get; set; } = 10;
        public int PlayerY { get; set; } = 4;

        protected override void OnRender(DrawingContext dc)
        {
            float tileWidth = (float)ActualWidth / (Math.Max(TILES_AROUND_PLAYER_X, TILES_AROUND_PLAYER_Y) * 2 + 1);
            SetCameraCenterValues(PlayerX, PlayerY);
            for (int i = _cameraCenterX - TILES_AROUND_PLAYER_X, index = 0;
                 i <= _cameraCenterX + TILES_AROUND_PLAYER_X;
                 i++, index++)
            {
                for (int j = _cameraCenterY - TILES_AROUND_PLAYER_Y, jndex = 0; 
                     j <= _cameraCenterY + TILES_AROUND_PLAYER_Y;
                     j++, jndex++)
                {
                    MapTile tile = _map[i, j];
                    dc.DrawImage(tile.Sprite, new Rect(index * tileWidth, jndex * tileWidth, tileWidth, tileWidth));
                    if (i == PlayerX && j == PlayerY)
                    {
                        dc.DrawEllipse(Brushes.Black, 
                                       new Pen(Brushes.Black, 1.0), 
                                       new Point(index * tileWidth + tileWidth / 2, 
                                                 jndex * tileWidth + tileWidth / 2), 
                                       tileWidth / 3, 
                                       tileWidth / 3);
                    }
                }
            }
        }

        private void SetCameraCenterValues(int playerX, int playerY)
        {
            if (0 > playerX - TILES_AROUND_PLAYER_X)
            {
                _cameraCenterX = TILES_AROUND_PLAYER_X;
            }
            else if (playerX + TILES_AROUND_PLAYER_X > _map.GetLength(0) - 1)
            {
                _cameraCenterX = _map.GetLength(0) - 1 - TILES_AROUND_PLAYER_X;
            }
            else
            {
                _cameraCenterX = playerX;
            }

            if (0 > playerY - TILES_AROUND_PLAYER_Y)
            {
                _cameraCenterY = TILES_AROUND_PLAYER_Y;
            }
            else if (playerY + TILES_AROUND_PLAYER_Y > _map.GetLength(1) - 1)
            {
                _cameraCenterY = _map.GetLength(1) - 1 - TILES_AROUND_PLAYER_Y;
            }
            else
            {
                _cameraCenterY = playerY;
            }
        }
    }
}
