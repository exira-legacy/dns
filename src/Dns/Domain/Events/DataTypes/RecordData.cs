namespace Dns.Domain.Events
{
    using Newtonsoft.Json;

    public class RecordData
    {
        public string Type { get; }
        public int TimeToLive { get; }
        public string Label { get; }
        public string Value { get; }

        public RecordData(Record record)
        {
            Type = record.Type.Value;
            TimeToLive = record.TimeToLive;
            Label = record.Label;
            Value = record.Value;
        }

        [JsonConstructor]
        private RecordData(
            string type,
            int timeToLive,
            string label,
            string value)
            : this(
                new Record(
                    RecordType.FromValue(type.ToLowerInvariant()),
                    new TimeToLive(timeToLive),
                    new RecordLabel(label),
                    new RecordValue(value))) { }

        public Record ToRecord() =>
            new Record(
                RecordType.FromValue(Type.ToLowerInvariant()),
                new TimeToLive(TimeToLive),
                new RecordLabel(Label),
                new RecordValue(Value));
    }
}
