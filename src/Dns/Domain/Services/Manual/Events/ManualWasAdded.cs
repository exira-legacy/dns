namespace Dns.Domain.Services.Manual.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Dns.Domain.Events;
    using Manual;
    using Newtonsoft.Json;

    [EventName("ManualWasAdded")]
    [EventDescription("The manual service was added.")]
    public class ManualWasAdded
    {
        public Guid ServiceId { get; }

        public string Label { get; }

        public RecordData[] Records { get; }

        public ManualWasAdded(
            ServiceId serviceId,
            ManualLabel label,
            RecordSet recordSet)
        {
            ServiceId = serviceId;
            Label = label;

            Records = recordSet
                .Select(r => new RecordData(r))
                .ToArray();
        }

        [JsonConstructor]
        private ManualWasAdded(
            Guid serviceId,
            string label,
            IReadOnlyCollection<RecordData> records)
            : this(
                new ServiceId(serviceId),
                new ManualLabel(label),
                new RecordSet(records.Select(r => r.ToRecord()).ToList()))
        { }
    }
}
