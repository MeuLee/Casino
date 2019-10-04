using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorMap
{
    public class Tile
    {
        Color Color { get; set; }
        public string TileType { get; set; }

        public Tile(Color c, string tileType)
        {
            Color = c;
            TileType = tileType;
        }
    }
}