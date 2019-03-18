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
        private readonly Dictionary<ServiceId, IService> _services = new Dictionary<ServiceId, IService>();

        private Domain()
        {
            Register<DomainWasCreated>(When);
            Register<ManualWasAdded>(When);
            Register<GoogleSuiteWasAdded>(When);
            Register<ServiceWasRemoved>(When);
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
                new ServiceId(@event.ServiceId),
                new ManualService(
                    new ServiceId(@event.ServiceId),
                    new ManualLabel(@event.ServiceLabel),
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
                new ServiceId(@event.ServiceId),
                new GoogleSuiteService(
                    new ServiceId(@event.ServiceId),
                    new GoogleVerificationToken(@event.VerificationToken)));
        }

        private void When(ServiceWasRemoved @event)
        {
            _services.Remove(new ServiceId(@event.ServiceId));
        }
    }
}
