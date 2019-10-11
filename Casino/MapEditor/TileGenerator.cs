using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    static class TileGenerator
    {
        static Tile CreateTile(string tileType)
        {
            switch (tileType)
            {
                case "Floor1":
                    return new Tile(Color.Red, tileType);
                case "Floor2":
                    return new Tile(Color.Gray, tileType);
                case "Table1":
                    return new Tile(Color.Brown, tileType);
                case "Table2":
                    return new Tile(Color.Green, tileType);
                case "SlotMachine":
                    return new Tile(Color.Yellow, tileType);
                case "Bar":
                    return new Tile(Color.Blue, tileType);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
