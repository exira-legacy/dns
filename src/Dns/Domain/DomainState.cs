namespace Dns.Domain
{
    using Events;

    public partial class Domain
    {
        private DomainName _name;

        private Domain()
        {
            Register<DomainWasCreated>(When);
        }

        private void When(DomainWasCreated @event)
        {
            _name = new DomainName(
                new SecondLevelDomain(@event.SecondLevelDomain),
                TopLevelDomain.FromValue(@event.TopLevelDomain));
        }
    }
}
