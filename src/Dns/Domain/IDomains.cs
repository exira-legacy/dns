namespace Dns.Domain
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;

    public interface IDomains : IAsyncRepository<Domain, DomainName> { }
}
