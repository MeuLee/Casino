using System;

namespace CasinoUI.Views.Map.Tiles
{
    public class OnMovedOverEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public OnMovedOverEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
