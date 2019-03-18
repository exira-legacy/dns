namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class ServiceLabel : StringValueObject<ServiceLabel>
    {
        public ServiceLabel([JsonProperty("value")] string label) : base(label)
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new EmptyServiceLabelException();
        }
    }
}
