using System;

namespace CasinoUI.Views.Map.Tiles
{
    public class OnMovedOverEventArgs : EventArgs
    {
        public int OldX { get; private set; }
        public int OldY { get; private set; }

        public OnMovedOverEventArgs(int oldX, int oldY)
        {
            OldX = oldX;
            OldY = oldY;
        }
    }
}
