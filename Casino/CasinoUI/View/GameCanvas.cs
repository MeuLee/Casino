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
        public MapTile[,] Map { get; private set; } = MapGenerator.LoadMapFromFile(Properties.Resources.map);

        public static int TilesAroundPlayerX { get; set; } = 4;
        public static int TilesAroundPlayerY { get; set; } = 3;

        private int _cameraCenterX, _cameraCenterY;

        // Will be removed once the HumanPlayer instance can be accesssed.
        public int PlayerX { get; set; } = 10;
        public int PlayerY { get; set; } = 4;

        protected override void OnRender(DrawingContext dc)
        {
            SetCameraCenterValues(PlayerX, PlayerY);
            float tileWidth = SetTileWidth();
            DrawTiles(dc, tileWidth); // tileWidth could be a property, when TilesAroundPlayer properties are set, TileWidth's value is re-set
            DrawPlayer(dc, tileWidth);
        }

        private float SetTileWidth()
        {
            return (float)ActualWidth / (Math.Max(TilesAroundPlayerX, TilesAroundPlayerY) * 2 + 1);
        }

        private void SetCameraCenterValues(int playerX, int playerY)
        {
            _cameraCenterX = SetCameraCenterValue(playerX, TilesAroundPlayerX, 0);
            _cameraCenterY = SetCameraCenterValue(playerY, TilesAroundPlayerY, 1);
        }

        private int SetCameraCenterValue(int playerCoord, int tilesAroundPlayer, int dimension)
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

        private void DrawTiles(DrawingContext dc, double tileWidth)
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

        private void DrawPlayer(DrawingContext dc, float tileWidth)
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

        private int PlayerCoordOnUI(int cameraCenterCoord, int playerCoord, int dimension, int tilesAroundPlayerCoord)
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
