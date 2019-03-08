namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class RecordLabel : StringValueObject<RecordLabel>
    {
        public const int MaxLength = 64; // TODO: Look up what the max length is

        public RecordLabel([JsonProperty("value")] string label) : base(label?.ToLowerInvariant())
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new EmptyRecordLabelException();
        }
    }
}
