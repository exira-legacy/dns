namespace Dns.Domain.Services.Manual
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class ManualLabel : StringValueObject<ManualLabel>
    {
        public const int MaxLength = 500; // TODO: Determine some sane value

        public ManualLabel([JsonProperty("value")] string label) : base(label)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new EmptyManualLabelException();
        }
    }
}
