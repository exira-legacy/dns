namespace Dns.Projections.Api.DomainDetail
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
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
                await context.FindAndUpdateDomainDetail(
                    message.Message.DomainName,
                    domain => domain.RemoveService(message.Message.ServiceId),
                    ct));

            When<Envelope<RecordSetUpdated>>(async (context, message, ct) =>
                await context.FindAndUpdateDomainDetail(
                    message.Message.DomainName,
                    domain => domain.RecordSet = message
                            .Message
                            .Records
                            .Select(x => new DomainDetail.DomainDetailRecord(x.Type, x.TimeToLive, x.Label, x.Value))
                            .ToList(),
                    ct));
        }

        private static async Task AddService(
            ApiProjectionsContext context,
            string domainName,
            Guid serviceId,
            string serviceType,
            string serviceLabel,
            CancellationToken ct) =>
            await context.FindAndUpdateDomainDetail(
                domainName,
                domain => domain.AddService(new DomainDetail.DomainDetailService(serviceId, serviceType, serviceLabel)),
                ct);
    }
}
