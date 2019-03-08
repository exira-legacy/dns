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
                .Select(r => new RecordData(r))
                .ToArray();
        }

        [JsonConstructor]
        private RecordSetUpdated(
            IReadOnlyCollection<RecordData> records)
            : this(
                new RecordSet(records.Select(r => r.ToRecord()).ToList())) { }
    }
}
