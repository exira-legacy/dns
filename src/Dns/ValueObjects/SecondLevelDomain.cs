namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Newtonsoft.Json;

    public class SecondLevelDomain : StringValueObject<SecondLevelDomain>
    {
        public const int MaxLength = 64;

        public SecondLevelDomain([JsonProperty("value")] string name) : base(name?.ToLowerInvariant())
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NoNameException("Name of a second level domain cannot be empty.");

            // TODO: Add validation rules for a domainname + unit tests
        }
    }
}
