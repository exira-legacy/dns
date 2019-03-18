namespace Dns.Api.Domain.Responses
{
    using System.Runtime.Serialization;
    using Projections.Api.DomainDetail;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "Domain", Namespace = "")]
    public class DomainResponse
    {
        /// <summary>
        /// Name of the domain.
        /// </summary>
        [DataMember(Name = "Name", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Second Level Domain of the domain
        /// </summary>
        [DataMember(Name = "SecondLevelDomain", Order = 2)]
        public string SecondLevelDomain { get; set; }

        /// <summary>
        /// Top Level Domain of the domain
        /// </summary>
        [DataMember(Name = "TopLevelDomain", Order = 3)]
        public string TopLevelDomain { get; set; }

        public DomainResponse(
            DomainName domainName)
        {
            Name = domainName;
            SecondLevelDomain = domainName.SecondLevelDomain;
            TopLevelDomain = domainName.TopLevelDomain.Value;
        }

        public DomainResponse(
            DomainDetail domainDetail) :
            this(
                new DomainName(
                    new SecondLevelDomain(domainDetail.SecondLevelDomain),
                    Dns.TopLevelDomain.FromValue(domainDetail.TopLevelDomain))) { }
    }

    public class DomainResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainResponse(
                new DomainName(
                    new SecondLevelDomain("exira"),
                    TopLevelDomain.com));
    }
}
