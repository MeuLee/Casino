using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CasinoUI.View.Map
{
    public class MapTile
    {
        public MapTile(int x, int y)
        {
            X = x;
            Y = x;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        
        // create one class per terrain type, with this class as parent ?
    }
}
