using System.Windows.Controls;
using System.Windows.Media;

namespace CasinoUI.Views.Map
{
    public class MiniMapCanvas : Canvas
    {
        protected override void OnRender(DrawingContext dc)
        {
            MapRenderer.RenderMiniMap(dc, (float)ActualWidth, (float)ActualHeight);
        }
    }
}
