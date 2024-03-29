﻿using System.Windows.Controls;
using System.Windows.Media;

namespace CasinoUI.Views.Map
{
    public class GameCanvas : Canvas
    {
        protected override void OnRender(DrawingContext dc)
        {
            MapRenderer.RenderMap(dc, (float)ActualWidth, (float)ActualHeight);
        }
    }
}
