using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorMap
{
    class Tile
    {
        Color Color { get; set; }
        string TileType { get; set; }

        public Tile(Color c, string tileType)
        {
            Color = c;
            TileType = tileType;
        }
    }
}