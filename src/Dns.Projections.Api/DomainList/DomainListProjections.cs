namespace Dns.Projections.Api.DomainList
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
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

            When<Envelope<GoogleSuiteWasAdded>>(async (context, message, ct) =>
                await AddService(context,
                    message.Message.DomainName,
                    message.Message.ServiceId,
                    message.Message.ServiceType,
                    message.Message.ServiceLabel,
                    ct));

            When<Envelope<ManualWasAdded>>(async (context, message, ct) =>
                await AddService(context,
                    message.Message.DomainName,
                    message.Message.ServiceId,
                    message.Message.ServiceType,
                    message.Message.ServiceLabel,
                    ct));

            When<Envelope<ServiceWasRemoved>>(async (context, message, ct) =>
                await context.FindAndUpdateDomainList(
                    message.Message.DomainName,
                    domain => domain.RemoveService(message.Message.ServiceId),
                    ct));

            When<Envelope<RecordSetUpdated>>(async (context, message, ct) =>
                await context.FindAndUpdateDomainList(
                    message.Message.DomainName,
                    domain => { }, // We don't care about recordset in the domain list
                    ct));
        }

        private static async Task AddService(
            ApiProjectionsContext context,
            string domainName,
            Guid serviceId,
            string serviceType,
            string serviceLabel,
            CancellationToken ct) =>
            await context.FindAndUpdateDomainList(
                domainName,
                domain => domain.AddService(new DomainList.DomainListService(serviceId, serviceType, serviceLabel)),
                ct);
    }
}
