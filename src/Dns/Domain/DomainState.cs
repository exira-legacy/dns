namespace Dns.Domain
{
    using Dns.Domain.Events;

    public partial class Domain
    {
        private DomainName _name;

        private Domain()
        {
            Register<DomainWasCreated>(When);
        }

        private void When(DomainWasCreated @event)
        {
            _name = new DomainName(@event.Name, @event.TopLevelDomain);
        }
    }
}
