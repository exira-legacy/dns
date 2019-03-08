namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class RecordValue : StringValueObject<RecordValue>
    {
        public const int MaxLength = 64; // TODO: Look up what the max length is

        public RecordValue([JsonProperty("value")] string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyRecordValueException();
        }
    }
}
