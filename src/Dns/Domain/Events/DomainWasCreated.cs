namespace Dns.Domain.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("DomainWasCreated")]
    [EventDescription("The domain was created.")]
    public class DomainWasCreated
    {
        [JsonIgnore]
        public string DomainName { get; }
        public string SecondLevelDomain { get; }
        public string TopLevelDomain { get; }

        public DomainWasCreated(
            DomainName domainName)
        {
            DomainName = domainName;
            SecondLevelDomain = domainName.SecondLevelDomain;
            TopLevelDomain = domainName.TopLevelDomain.Value;
        }

        [JsonConstructor]
        private DomainWasCreated(
            [JsonProperty("secondLevelDomain")] string secondLevelDomain,
            [JsonProperty("topLevelDomain")] string topLevelDomain)
            : this(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    Dns.TopLevelDomain.FromValue(topLevelDomain))) { }
    }
}
