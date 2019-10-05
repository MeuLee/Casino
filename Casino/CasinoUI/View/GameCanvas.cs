using CasinoUI.View.Map;
using CasinoUI.View.Map.Tiles;
using System.Windows.Controls;
using System.Windows.Media;

namespace CasinoUI.View
{
    public class GameCanvas : Canvas
    {
        private MapTile[,] _map = MapGenerator.LoadMapFromFile(Properties.Resources.map);
        protected override void OnRender(DrawingContext dc)
        {
            
        }
    }
}
