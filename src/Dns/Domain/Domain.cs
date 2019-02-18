namespace Dns.Domain
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using System;
    using Dns.Domain.Events;

    public partial class Domain : AggregateRootEntity
    {
        public static readonly Func<Domain> Factory = () => new Domain();

        public static Domain Register(DomainName name)
        {
            var domain = Factory();
            domain.ApplyChange(new DomainWasCreated(name));
            return domain;
        }
    }
}
