using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoUI.View.Map
{
    public class MapGenerator
    {

        public static MapTile[,] CreateMap(int width, int height)
        {
            MapTile[,] map = new MapTile[width, height];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = new MapTile(i, j);
                }
            }

            return map;
        }
    }
}
