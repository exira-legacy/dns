namespace Dns.Domain
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using System;
    using System.Linq;
    using Events;
    using Exceptions;
    using Services.GoogleSuite;
    using Services.GoogleSuite.Events;
    using Services.Manual;
    using Services.Manual.Events;

    public partial class Domain : AggregateRootEntity
    {
        public static readonly Func<Domain> Factory = () => new Domain();

        public static Domain Register(DomainName domainName)
        {
            var domain = Factory();
            domain.ApplyChange(new DomainWasCreated(domainName));
            return domain;
        }

        public void AddManual(ServiceId serviceId, ManualLabel label, RecordSet records)
        {
            CheckIfServiceAlreadyExists(serviceId);
            ApplyChange(new ManualWasAdded(_name, serviceId, label, records));
            UpdateRecordSet();
        }

        public void AddGoogleSuite(ServiceId serviceId, GoogleVerificationToken verificationToken)
        {
            CheckIfServiceAlreadyExists(serviceId);
            ApplyChange(new GoogleSuiteWasAdded(_name, serviceId, verificationToken));
            UpdateRecordSet();
        }

        public void RemoveService(ServiceId serviceId)
        {
            if (!_services.ContainsKey(serviceId))
                return;

            ApplyChange(new ServiceWasRemoved(_name, serviceId));
            UpdateRecordSet();
        }

        private void CheckIfServiceAlreadyExists(ServiceId serviceId)
        {
            if (_services.ContainsKey(serviceId))
                throw new ServiceAlreadyExistsException(serviceId);
        }

        private void UpdateRecordSet()
        {
            ApplyChange(
                new RecordSetWasUpdated(
                    _name,
                    _services.Values.Aggregate(
                        new RecordSet(),
                        (r, service) => r.AddRecords(service.GetRecords()),
                        r => r)));
        }
    }
}
