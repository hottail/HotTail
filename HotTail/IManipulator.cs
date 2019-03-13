using System;
using System.Windows.Forms;

namespace HotTail
{
    public interface IManipulator
    {
        event EventHandler LevelChanged;
        string Descriptor { get; }
        UserControl ManipulatorSettings { get; }
        double Level { get; set; }
        void DataUpdate(long ticksElapsed);
        void Stop();
    }
}
