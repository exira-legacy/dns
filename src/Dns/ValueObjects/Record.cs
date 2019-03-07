namespace Dns
{
    public class Record
    {
        public RecordType Type { get; }
        public int TimeToLive { get; }

        public string Label { get; }
        public string Value { get; }

        public Record(
            RecordType type,
            int timeToLive,
            string label,
            string value)
        {
            Type = type;
            TimeToLive = timeToLive;
            Label = label;
            Value = value;
        }
    }

    public enum RecordType
    {
        Txt,
        Mx,
        CName
    }
}
