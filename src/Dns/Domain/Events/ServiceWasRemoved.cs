namespace Dns.Domain.Events
{
    using System;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("ServiceWasRemoved")]
    [EventDescription("The service was removed.")]
    public class ServiceWasRemoved
    {
        [JsonIgnore]
        public string DomainName { get; }
        public string SecondLevelDomain { get; }
        public string TopLevelDomain { get; }

        public Guid ServiceId { get; }

        public ServiceWasRemoved(
            DomainName domainName,
            ServiceId serviceId)
        {
            DomainName = domainName;
            SecondLevelDomain = domainName.SecondLevelDomain;
            TopLevelDomain = domainName.TopLevelDomain.Value;

            ServiceId = serviceId;
        }

        [JsonConstructor]
        private ServiceWasRemoved(
            [JsonProperty("secondLevelDomain")] string secondLevelDomain,
            [JsonProperty("topLevelDomain")] string topLevelDomain,
            [JsonProperty("serviceId")] Guid serviceId)
            : this(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    Dns.TopLevelDomain.FromValue(topLevelDomain)),
                new ServiceId(serviceId)) { }
    }
}
