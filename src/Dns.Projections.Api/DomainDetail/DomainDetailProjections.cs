namespace Dns.Projections.Api.DomainDetail
{
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using Domain.Events;
    using Domain.Services.GoogleSuite.Events;
    using Domain.Services.Manual.Events;

    public class DomainDetailProjections : ConnectedProjection<ApiProjectionsContext>
    {
        public DomainDetailProjections()
        {
            When<Envelope<DomainWasCreated>>(async (context, message, ct) =>
            {
                await context
                    .DomainDetails
                    .AddAsync(
                        new DomainDetail
                        {
                            Name = message.Message.DomainName,
                            SecondLevelDomain = message.Message.SecondLevelDomain,
                            TopLevelDomain = message.Message.TopLevelDomain,
                            //CreatedAtTimestamp = message.CreatedUtc // TODO: Decide where to get this from
                        }, ct);
            });

            When<Envelope<RecordSetUpdated>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainDetail(
                    message.Message.DomainName,
                    domain =>
                    {
                        // TODO: Implement
                    },
                    ct);
            });

            When<Envelope<GoogleSuiteWasAdded>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainDetail(
                    message.Message.DomainName,
                    domain =>
                    {
                        // TODO: Implement
                    },
                    ct);
            });

            When<Envelope<ManualWasAdded>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainDetail(
                    message.Message.DomainName,
                    domain =>
                    {
                        // TODO: Implement
                    },
                    ct);
            });

            When<Envelope<ServiceWasRemoved>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateDomainDetail(
                    message.Message.DomainName,
                    domain =>
                    {
                        // TODO: Implement
                    },
                    ct);
            });
        }
    }
}
