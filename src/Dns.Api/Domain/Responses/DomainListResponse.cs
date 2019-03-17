namespace Dns.Api.Domain.Responses
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Projections.Api.DomainList;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "Domains", Namespace = "")]
    public class DomainListResponse
    {
        /// <summary>
        /// All domains.
        /// </summary>
        [DataMember(Name = "Domains", Order = 1)]
        public List<DomainListItemResponse> Domains { get; set; }
    }

    [DataContract(Name = "Domain", Namespace = "")]
    public class DomainListItemResponse
    {
        /// <summary>
        /// Name of the domain.
        /// </summary>
        [DataMember(Name = "Name", Order = 1)]
        public string Name { get; set; }

        public DomainListItemResponse(
            DomainList domainList) :
            this(
                new DomainName(
                    new SecondLevelDomain(domainList.SecondLevelDomain),
                    TopLevelDomain.FromValue(domainList.TopLevelDomain))) { }

        public DomainListItemResponse(
            DomainName domainName) => Name = domainName;
    }

    public class DomainListResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainListResponse
            {
                Domains = new List<DomainListItemResponse>
                {
                    new DomainListItemResponse(new DomainName(new SecondLevelDomain("exira"), TopLevelDomain.com)),
                    new DomainListItemResponse(new DomainName(new SecondLevelDomain("cumps"), TopLevelDomain.be)),
                }
            };
    }
}
