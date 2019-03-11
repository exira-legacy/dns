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

            // TODO: Label has rules to follow, encode them! (no spaces, etc)

            //Kan bestaat uit:
            //@; root domein
            //A tot Z ; drukletters
            //a tot z ; kleine letters
            //0 tot 9 ; cijfers
            //- ; koppelteken

            //Kan beginnen of eindigen met een letter

            //Kan beginnen of eindigen met een cijfer

            //Kan niet beginnen of eindigen met een '-'

            //Kan niet bestaan uit allemaal cijfers

            //Kan tot 63 tekens lang zijn
        }
    }
}
