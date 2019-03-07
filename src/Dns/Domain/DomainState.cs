namespace Dns.Domain
{
    using System.Collections.Generic;
    using Events;
    using Services.GoogleSuite;
    using Services.GoogleSuite.Events;

    public partial class Domain
    {
        private DomainName _name;
        private readonly List<IService> _services = new List<IService>();

        private Domain()
        {
            Register<DomainWasCreated>(When);
            Register<GoogleSuiteWasAdded>(When);
        }

        private void When(DomainWasCreated @event)
        {
            _name = new DomainName(
                new SecondLevelDomain(@event.SecondLevelDomain),
                TopLevelDomain.FromValue(@event.TopLevelDomain));
        }

        private void When(GoogleSuiteWasAdded @event)
        {
            _services.Add(
                new GoogleSuiteService(
                    new GoogleVerificationToken(@event.VerificationToken)));
        }
    }
}
