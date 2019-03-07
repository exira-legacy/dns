namespace Dns.Domain.Events
{
    using System.Collections.Generic;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("RecordSetUpdated")]
    [EventDescription("The complete record set was updated.")]
    public class RecordSetUpdated
    {
        public RecordData[] Records { get; }

        public RecordSetUpdated(
            RecordSet recordSet)
        {
            Records = recordSet
                .Select(r => new RecordData
                {
                    Label = r.Label,
                    Value = r.Value
                })
                .ToArray();
        }

        [JsonConstructor]
        private RecordSetUpdated(
            IReadOnlyCollection<RecordData> records)
            : this(new RecordSet(records.Select(r => new Record(r.Label, r.Value)).ToList())) { }
    }

    // TODO: Refactor this
    public class RecordData
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
