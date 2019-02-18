namespace Dns
{
    using System.Collections.Generic;
    using Be.Vlaanderen.Basisregisters.AggregateSource;

    public class DomainName : ValueObject<DomainName>
    {
        public string Name { get; }

        public TopLevelDomain TopLevelDomain { get; }

        public DomainName(string name, TopLevelDomain topLevelDomain)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NoNameException("Name of a domain cannot be empty.");

            // TODO: Add validation rules for a domainname + unit tests

            Name = name;
            TopLevelDomain = topLevelDomain;
        }

        protected override IEnumerable<object> Reflect()
        {
            yield return Name;
            yield return TopLevelDomain;
        }

        public override string ToString() => $"{Name}.{TopLevelDomain}";
    }
}
