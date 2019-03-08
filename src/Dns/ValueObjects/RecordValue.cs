namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Newtonsoft.Json;

    public class RecordValue : StringValueObject<RecordValue>
    {
        public const int MaxLength = 64; // TODO: Look up what the max length is

        public RecordValue([JsonProperty("value")] string value) : base(value)
        {
            // TODO: Throw different exception
            if (string.IsNullOrWhiteSpace(value))
                throw new NoNameException("Value of a record cannot be empty.");
        }
    }
}
