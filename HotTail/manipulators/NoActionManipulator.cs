using System;
using System.Windows.Forms;

namespace HotTail.manipulators
{
    public class NoActionManipulator : IManipulator
    {
        public NoActionManipulator()
        {
            manipulatorSettings = new UserControl();
        }

        public string Descriptor { get => "No Action"; }

        public UserControl ManipulatorSettings { get => manipulatorSettings; }
        private UserControl manipulatorSettings;

        public double Level
        {
            get => _level;
            set
            {
                _level = Math.Min(Math.Max(value, 0), 1);
                LevelChanged?.Invoke(this, new LevelChangedEventArgs(_level));
            }
        }
        private double _level = 0;

        public event EventHandler LevelChanged;

        public void DataUpdate(long ticksElapsed) {}

        public void Stop()
        {
            Level = 0;
        }
    }
}
