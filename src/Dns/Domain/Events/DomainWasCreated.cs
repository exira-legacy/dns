namespace Dns.Domain.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("DomainWasCreated")]
    [EventDescription("The domain was created.")]
    public class DomainWasCreated
    {
        public string SecondLevelDomain { get; }
        public string TopLevelDomain { get; }

        public DomainWasCreated(
            DomainName domainName)
        {
            SecondLevelDomain = domainName.SecondLevelDomain;
            TopLevelDomain = domainName.TopLevelDomain.Value;
        }

        [JsonConstructor]
        private DomainWasCreated(
            string secondLevelDomain,
            string topLevelDomain)
            : this(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    Dns.TopLevelDomain.FromValue(topLevelDomain))) {}
    }
}
