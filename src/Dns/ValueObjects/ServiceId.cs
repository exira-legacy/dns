namespace Dns
{
    using System;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Newtonsoft.Json;

    public class ServiceId : GuidValueObject<ServiceId>
    {
        public ServiceId([JsonProperty("value")] Guid serviceId) : base(serviceId)
        {
            // TODO: Throw different exception
            if (serviceId == Guid.Empty)
                throw new NoNameException("Service id cannot be empty.");
        }
    }
}
