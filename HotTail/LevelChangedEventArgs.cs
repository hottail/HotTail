using System;

namespace HotTail
{
    public class LevelChangedEventArgs : EventArgs
    {
        public readonly double Level;

        public LevelChangedEventArgs(double newLevel)
        {
            Level = newLevel;
        }
    }
}
