namespace Dns
{
    using System;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Newtonsoft.Json;

    public class SecondLevelDomain : StringValueObject<SecondLevelDomain>
    {
        public const int MaxLength = 64;

        public SecondLevelDomain([JsonProperty("value")] string name) : base(name?.ToLowerInvariant())
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NoNameException("Name of a second level domain cannot be empty.");

            // https://github.com/dotnet/corefx/blob/db7daabdb592da062d8a80f27bbad1178c364530/src/System.Private.Uri/src/System/Uri.cs#L1285
            if (Uri.CheckHostName(name) != UriHostNameType.Dns)
                throw new InvalidHostnameException("Name of a second level domain must be a valid hostname.");
        }
    }
}
