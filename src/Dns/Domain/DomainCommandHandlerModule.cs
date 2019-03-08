namespace Dns.Domain
{
    using System;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.CommandHandling;
    using Be.Vlaanderen.Basisregisters.CommandHandling.SqlStreamStore;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Commands;
    using Services.GoogleSuite.Commands;
    using Services.Manual.Commands;
    using SqlStreamStore;

    public sealed class DomainCommandHandlerModule : CommandHandlerModule
    {
        public DomainCommandHandlerModule(
            Func<IDomains> getDomains,
            Func<ConcurrentUnitOfWork> getUnitOfWork,
            Func<IStreamStore> getStreamStore,
            EventMapping eventMapping,
            EventSerializer eventSerializer)
        {
            For<CreateDomain>()
                .AddSqlStreamStore(getStreamStore, getUnitOfWork, eventMapping, eventSerializer)
                .Handle(async (message, ct) =>
                {
                    var domains = getDomains();

                    var domainName = message.Command.DomainName;
                    var domain = Domain.Register(domainName);

                    domains.Add(domainName, domain);
                });

            For<AddManual>()
                .AddSqlStreamStore(getStreamStore, getUnitOfWork, eventMapping, eventSerializer)
                .Handle(async (message, ct) =>
                {
                    var domains = getDomains();

                    var domainName = message.Command.DomainName;
                    var domain = await domains.GetAsync(domainName, ct);

                    domain.AddManual(
                        message.Command.ServiceId,
                        message.Command.Label,
                        message.Command.Records);
                });

            For<AddGoogleSuite>()
                .AddSqlStreamStore(getStreamStore, getUnitOfWork, eventMapping, eventSerializer)
                .Handle(async (message, ct) =>
                {
                    var domains = getDomains();

                    var domainName = message.Command.DomainName;
                    var domain = await domains.GetAsync(domainName, ct);

                    domain.AddGoogleSuite(
                        message.Command.ServiceId,
                        message.Command.VerificationToken);
                });
        }
    }
}
