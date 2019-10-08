using CasinoUI.View.Map;
using System.Windows.Controls;
using System.Windows.Media;

namespace CasinoUI.View
{
    public class GameCanvas : Canvas
    {
        protected override void OnRender(DrawingContext dc)
        {
            MapRenderer.RenderMap((float)ActualWidth, (float)ActualHeight, dc);
        }
    }
}
