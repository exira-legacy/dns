namespace Dns
{
    using System;
    using System.Collections.Generic;
    using Be.Vlaanderen.Basisregisters.AggregateSource;

    public class Record : ValueObject<Record>
    {
        public RecordType Type { get; }
        public TimeToLive TimeToLive { get; }

        public RecordLabel Label { get; }
        public RecordValue Value { get; }

        public Record(
            RecordType type,
            TimeToLive timeToLive,
            RecordLabel label,
            RecordValue value)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type), "Type of record is missing.");
            TimeToLive = timeToLive ?? throw new ArgumentNullException(nameof(timeToLive), "Time to live of record is missing.");
            Label = label ?? throw new ArgumentNullException(nameof(label), "Label of record is missing.");
            Value = value ?? throw new ArgumentNullException(nameof(value), "Value of record is missing.");
        }

        protected override IEnumerable<object> Reflect()
        {
            yield return Type;
            yield return TimeToLive;
            yield return Label;
            yield return Value;
        }

        public override string ToString() => $"{Type} {TimeToLive} {Label} {Value}";
    }
}
