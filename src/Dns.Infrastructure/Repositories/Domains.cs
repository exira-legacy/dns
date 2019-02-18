namespace Dns.Infrastructure.Repositories
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.AggregateSource.SqlStreamStore;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Domain;
    using SqlStreamStore;

    public class Domains : Repository<Domain, DomainName>, IDomains
    {
        public Domains(ConcurrentUnitOfWork unitOfWork, IStreamStore eventStore, EventMapping eventMapping, EventDeserializer eventDeserializer)
            : base(Domain.Factory, unitOfWork, eventStore, eventMapping, eventDeserializer) { }
    }
}
