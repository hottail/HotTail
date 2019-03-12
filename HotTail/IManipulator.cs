namespace HotTail
{
    public interface IManipulator
    {
        string Descriptor { get; }
        double Level { get; set; }
        void DataUpdate();
        void Stop();
    }
}
