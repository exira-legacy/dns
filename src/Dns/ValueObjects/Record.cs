namespace Dns
{
    public class Record
    {
        // TODO: Spec out more
        public string Label { get; }
        public string Value { get; }

        public Record(
            string label,
            string value)
        {
            Label = label;
            Value = value;
        }
    }
}
