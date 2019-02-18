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
    }
}
