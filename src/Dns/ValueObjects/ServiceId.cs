namespace Dns
{
    using System;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class ServiceId : GuidValueObject<ServiceId>
    {
        public ServiceId([JsonProperty("value")] Guid serviceId) : base(serviceId)
        {
            if (Value == Guid.Empty)
                throw new EmptyServiceIdException();
        }
    }
}
