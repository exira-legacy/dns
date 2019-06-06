namespace Dns.Domain.Events
{
    using System.Collections.Generic;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("RecordSetWasUpdated")]
    [EventDescription("The complete record set was updated.")]
    public class RecordSetWasUpdated
    {
        [JsonIgnore]
        public string DomainName { get; }
        public string SecondLevelDomain { get; }
        public string TopLevelDomain { get; }

        public RecordData[] Records { get; }

        public RecordSetWasUpdated(
            DomainName domainName,
            RecordSet recordSet)
        {
            DomainName = domainName;
            SecondLevelDomain = domainName.SecondLevelDomain;
            TopLevelDomain = domainName.TopLevelDomain.Value;

            Records = recordSet
                .Select(r => new RecordData(r))
                .ToArray();
        }

        [JsonConstructor]
        private RecordSetWasUpdated(
            [JsonProperty("secondLevelDomain")] string secondLevelDomain,
            [JsonProperty("topLevelDomain")] string topLevelDomain,
            [JsonProperty("records")] IReadOnlyCollection<RecordData> records)
            : this(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    Dns.TopLevelDomain.FromValue(topLevelDomain)),
                new RecordSet(records.Select(r => r.ToRecord()).ToList())) { }
    }
}
