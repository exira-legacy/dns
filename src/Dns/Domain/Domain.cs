namespace Dns.Domain
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using System;
    using System.Linq;
    using Events;
    using Services.GoogleSuite;
    using Services.GoogleSuite.Events;

    public partial class Domain : AggregateRootEntity
    {
        public static readonly Func<Domain> Factory = () => new Domain();

        public static Domain Register(DomainName domainName)
        {
            var domain = Factory();
            domain.ApplyChange(new DomainWasCreated(domainName));
            return domain;
        }

        public void AddGoogleSuite(GoogleVerificationToken verificationToken)
        {
            ApplyChange(new GoogleSuiteWasAdded(verificationToken));

            UpdateRecordSet();
        }

        private void UpdateRecordSet()
        {
            ApplyChange(
                new RecordSetUpdated(
                    _services.Aggregate(
                        new RecordSet(),
                        (r, service) => r.AddRecords(service.GetRecords()),
                        r => r)));
        }
    }
}
