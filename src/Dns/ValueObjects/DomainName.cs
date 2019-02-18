namespace Dns
{
    using System;
    using System.Collections.Generic;
    using Be.Vlaanderen.Basisregisters.AggregateSource;

    public class DomainName : ValueObject<DomainName>
    {
        public SecondLevelDomain SecondLevelDomain { get; }

        public TopLevelDomain TopLevelDomain { get; }

        public DomainName(
            SecondLevelDomain secondLevelDomain,
            TopLevelDomain topLevelDomain)
        {
            SecondLevelDomain = secondLevelDomain
                                ?? throw new ArgumentNullException(nameof(secondLevelDomain), "Second level domain of domain name is missing.");

            TopLevelDomain = topLevelDomain
                             ?? throw new ArgumentNullException(nameof(topLevelDomain), "Top level domain of domain name is missing.");
        }

        protected override IEnumerable<object> Reflect()
        {
            yield return SecondLevelDomain;
            yield return TopLevelDomain;
        }

        public override string ToString() => $"{SecondLevelDomain}{TopLevelDomain}";
    }
}
