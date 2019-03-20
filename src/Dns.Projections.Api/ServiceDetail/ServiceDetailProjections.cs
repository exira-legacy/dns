namespace Dns.Projections.Api.ServiceDetail
{
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using Domain.Events;
    using Domain.Services.GoogleSuite;
    using Domain.Services.GoogleSuite.Events;
    using Domain.Services.Manual;
    using Domain.Services.Manual.Events;

    public class ServiceDetailProjections : ConnectedProjection<ApiProjectionsContext>
    {
        public ServiceDetailProjections()
        {
            When<Envelope<GoogleSuiteWasAdded>>(async (context, message, ct) =>
            {
                await context
                    .ServiceDetails
                    .AddAsync(
                        new ServiceDetail
                        {
                            ServiceId = message.Message.ServiceId,
                            Type = message.Message.ServiceType,
                            Label = message.Message.ServiceLabel,
                            ServiceData = new GoogleSuiteService(
                                new ServiceId(message.Message.ServiceId),
                                new GoogleVerificationToken(message.Message.VerificationToken)).GetServiceData()
                        }, ct);
            });

            When<Envelope<ManualWasAdded>>(async (context, message, ct) =>
            {
                await context
                    .ServiceDetails
                    .AddAsync(
                        new ServiceDetail
                        {
                            ServiceId = message.Message.ServiceId,
                            Type = message.Message.ServiceType,
                            Label = message.Message.ServiceLabel,
                            ServiceData = new ManualService(
                                new ServiceId(message.Message.ServiceId),
                                new ManualLabel(message.Message.ServiceLabel),
                                new RecordSet(
                                    message.Message.Records.Select(r => new Record(
                                        RecordType.FromValue(r.Type),
                                        new TimeToLive(r.TimeToLive),
                                        new RecordLabel(r.Label),
                                        new RecordValue(r.Value))).ToList())).GetServiceData()
                        }, ct);
            });

            When<Envelope<ServiceWasRemoved>>(async (context, message, ct) =>
            {
                var service = await context
                    .ServiceDetails
                    .FindAsync(message.Message.ServiceId);

                if (service != null)
                    context
                        .ServiceDetails
                        .Remove(service);
            });
        }
    }
}
