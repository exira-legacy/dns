namespace Dns.Domain.Events
{
    using System;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("ServiceWasRemoved")]
    [EventDescription("The service was removed.")]
    public class ServiceWasRemoved
    {
        public Guid ServiceId { get; }

        public ServiceWasRemoved(ServiceId serviceId)
        {
            ServiceId = serviceId;
        }

        [JsonConstructor]
        private ServiceWasRemoved(
            Guid serviceId)
            : this(
                new ServiceId(serviceId)) {}
    }
}
