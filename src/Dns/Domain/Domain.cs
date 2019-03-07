namespace Dns.Domain
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using System;
    using Events;

    public partial class Domain : AggregateRootEntity
    {
        public static readonly Func<Domain> Factory = () => new Domain();

        public static Domain Register(DomainName domainName)
        {
            var domain = Factory();
            domain.ApplyChange(new DomainWasCreated(domainName));
            return domain;
        }

        public void AddGoogleSuite()
        {
            ApplyChange(new GoogleSuiteWasAdded());

            CalculateRecordSet();
        }

        private void CalculateRecordSet()
        {
            // TODO: Build RecordSet from services and update if changed
            ApplyChange(new RecordSetUpdated());
        }
    }
}
