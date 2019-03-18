namespace Dns
{
    using System;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class SecondLevelDomain : StringValueObject<SecondLevelDomain>
    {
        public const int MaxLength = 64;

        public SecondLevelDomain([JsonProperty("value")] string name) : base(name?.ToLowerInvariant())
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new EmptySecondLevelDomainException();

            if (Value.Length > MaxLength)
                throw new SecondLevelDomainTooLongException();

            // https://github.com/dotnet/corefx/blob/db7daabdb592da062d8a80f27bbad1178c364530/src/System.Private.Uri/src/System/Uri.cs#L1285
            if (Uri.CheckHostName(Value) != UriHostNameType.Dns)
                throw new InvalidHostNameException();
        }
    }
}
