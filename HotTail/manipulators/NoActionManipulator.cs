namespace HotTail.manipulators
{
    public class NoActionManipulator : IManipulator
    {
        public NoActionManipulator() { }

        public string Descriptor { get => "No Action"; }

        public double Level { get; set; } = 0;

        public void DataUpdate() {}

        public void Stop()
        {
            Level = 0;
        }
    }
}
