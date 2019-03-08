namespace Dns.Domain.Services.Manual.Events
{
    using System.Collections.Generic;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Manual;
    using Newtonsoft.Json;

    [EventName("ManualWasAdded")]
    [EventDescription("The manual service was added.")]
    public class ManualWasAdded
    {
        public string Label { get; }

        public RecordData[] Records { get; }

        public ManualWasAdded(
            ManualLabel label,
            RecordSet recordSet)
        {
            Label = label;

            Records = recordSet
                .Select(r => new RecordData
                {
                    Type = r.Type.Value,
                    TimeToLive = r.TimeToLive,
                    Label = r.Label,
                    Value = r.Value
                })
                .ToArray();
        }

        [JsonConstructor]
        private ManualWasAdded(
            string label,
            IReadOnlyCollection<RecordData> records)
            : this(
                new ManualLabel(label),
                new RecordSet(
                    records.Select(r => new Record(
                        RecordType.FromValue(r.Type),
                        new TimeToLive(r.TimeToLive),
                        new RecordLabel(r.Label),
                        new RecordValue(r.Value))).ToList()))
        { }

        public class RecordData
        {
            public string Type { get; set; }
            public int TimeToLive { get; set; }
            public string Label { get; set; }
            public string Value { get; set; }
        }
    }
}
