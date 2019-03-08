namespace Dns.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using Events;
    using Services.GoogleSuite;
    using Services.GoogleSuite.Events;
    using Services.Manual;
    using Services.Manual.Events;

    public partial class Domain
    {
        private DomainName _name;
        private readonly List<IService> _services = new List<IService>();

        private Domain()
        {
            Register<DomainWasCreated>(When);
            Register<ManualWasAdded>(When);
            Register<GoogleSuiteWasAdded>(When);
        }

        private void When(DomainWasCreated @event)
        {
            _name = new DomainName(
                new SecondLevelDomain(@event.SecondLevelDomain),
                TopLevelDomain.FromValue(@event.TopLevelDomain));
        }

        private void When(ManualWasAdded @event)
        {
            _services.Add(
                new ManualService(
                    new ManualLabel(@event.Label),
                    new RecordSet(
                        @event.Records.Select(r => new Record(
                            RecordType.FromValue(r.Type),
                            new TimeToLive(r.TimeToLive),
                            new RecordLabel(r.Label),
                            new RecordValue(r.Value))).ToList())));
        }

        private void When(GoogleSuiteWasAdded @event)
        {
            _services.Add(
                new GoogleSuiteService(
                    new GoogleVerificationToken(@event.VerificationToken)));
        }
    }
}
