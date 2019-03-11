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

            // TODO: Value has rules to folllow, encode them!
            // TODO: Additional rules depend on the record type!
            //Name server(NS) syntax
            //Kan een FQDN zijn(eindigen met punt)
            //bv.ns1.example.com.
            //Kan hostname zijn(eindigen zonder punt)

            //Mail record(MX) syntax
            //Moet bestaan uit numerieke prioriteit en een van volgende:
            //een FQDN(eindigen met punt)
            //bv. 10 mail.example.com.
            //een hostname(eindigen zonder punt)
            //bv. 10 mail

            //Host record(A) syntax
            //Moet een geldig IP adres zijn
            //bv. 198.51.100.1

            //Alias record(CNAME) syntax
            //Kan een FQDN zijn(eindigen met punt)
            //bv.alias.example.com.
            //Kan hostname zijn(eindigen zonder punt)
        }
    }
}
