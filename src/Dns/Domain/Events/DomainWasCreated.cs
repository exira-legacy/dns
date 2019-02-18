namespace Dns.Domain.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("DomainWasCreated")]
    [EventDescription("The domain was created.")]
    public class DomainWasCreated
    {
        public string Name { get; }
        public TopLevelDomain TopLevelDomain { get; }

        public DomainWasCreated(
            DomainName name)
        {
            Name = name.Name;
            TopLevelDomain = name.TopLevelDomain;
        }

        [JsonConstructor]
        private DomainWasCreated(
            string name,
            TopLevelDomain topLevelDomain)
            : this(
                new DomainName(name, topLevelDomain)) {}
    }
}
