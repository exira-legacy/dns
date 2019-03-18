namespace Dns.Projections.Api.DomainList
{
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using Domain.Events;
    using Domain.Services.GoogleSuite.Events;
    using Domain.Services.Manual.Events;

    public class DomainListProjections : ConnectedProjection<ApiProjectionsContext>
    {
        public DomainListProjections()
        {
            When<Envelope<DomainWasCreated>>(async (context, message, ct) =>
            {
                await context
                    .DomainList
                    .AddAsync(
                        new DomainList
                        {
                            Name = message.Message.DomainName,
                            SecondLevelDomain = message.Message.SecondLevelDomain,
                            TopLevelDomain = message.Message.TopLevelDomain,
                            //CreatedAtTimestamp = message.CreatedUtc // TODO: Decide where to get this from
                        }, ct);
            });

            When<Envelope<RecordSetUpdated>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainList(
                    message.Message.DomainName,
                    domain =>
                    {
                        // TODO: Implement
                    },
                    ct);
            });

            When<Envelope<GoogleSuiteWasAdded>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainList(
                    message.Message.DomainName,
                    domain => domain.AddService(
                        new DomainList.DomainListService(
                            message.Message.ServiceId,
                            "googlesuite", // TODO: 'googlesuite' should come from the service
                            "Google Suite")),
                    ct);
            });

            When<Envelope<ManualWasAdded>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainList(
                    message.Message.DomainName,
                    domain => domain.AddService(
                        new DomainList.DomainListService(
                            message.Message.ServiceId,
                            "manual", // TODO: 'manual' should come from the service
                            message.Message.Label)), 
                    ct);
            });

            When<Envelope<ServiceWasRemoved>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainList(
                    message.Message.DomainName,
                    domain => domain.RemoveService(message.Message.ServiceId),
                    ct);
            });
        }
    }
}
