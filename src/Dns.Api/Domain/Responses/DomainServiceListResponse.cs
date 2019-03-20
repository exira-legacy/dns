namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;
    using Infrastructure.Responses;
    using Projections.Api.DomainDetail;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "DomainServices", Namespace = "")]
    public class DomainServiceListResponse
    {
        /// <summary>
        /// All services added to the domain.
        /// </summary>
        [DataMember(Name = "Services", Order = 1)]
        public List<DomainServiceListItemResponse> Services { get; set; }

        /// <summary>
        /// Hypermedia links
        /// </summary>
        [DataMember(Name = "Links", Order = 2)]
        public List<Link> Links { get; set; }

        public DomainServiceListResponse(
            DomainDetail domainDetail)
        {
            Links = new List<Link>
            {
                new Link("/", Link.Relations.Home, WebRequestMethods.Http.Get),
                new Link("/domains", Link.Relations.Domains, WebRequestMethods.Http.Get),
                new Link($"/domains/{domainDetail.Name}", Link.Relations.Domain, WebRequestMethods.Http.Get),
                new Link($"/domains/{domainDetail.Name}/services/{ServiceType.manual.Value}", Link.Relations.Service, WebRequestMethods.Http.Post),
                new Link($"/domains/{domainDetail.Name}/services/{ServiceType.googlesuite.Value}", Link.Relations.Service, WebRequestMethods.Http.Post)
            };
        }
    }

    [DataContract(Name = "DomainService", Namespace = "")]
    public class DomainServiceListItemResponse
    {
        /// <summary>
        /// Id of the domain service.
        /// </summary>
        [DataMember(Name = "Id", Order = 1)]
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Type of the domain service.
        /// </summary>
        [DataMember(Name = "Type", Order = 2)]
        public string Type { get; set; }

        /// <summary>
        /// Descriptive label of the domain service.
        /// </summary>
        [DataMember(Name = "Label", Order = 3)]
        public string Label { get; set; }

        /// <summary>
        /// Hypermedia links
        /// </summary>
        [DataMember(Name = "Links", Order = 4)]
        public List<Link> Links { get; set; }

        public DomainServiceListItemResponse(
            DomainDetail domainDetail,
            DomainDetail.DomainDetailService domainDetailService)
        {
            ServiceId = domainDetailService.ServiceId;
            Type = domainDetailService.Type;
            Label = domainDetailService.Label;

            Links = new List<Link>
            {
                new Link($"/domains/{domainDetail.Name}/services/{domainDetailService.ServiceId}", Link.Relations.Service, WebRequestMethods.Http.Get)
            };
        }
    }

    public class DomainServiceListResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainServiceListResponse(new DomainDetail { Name = "exira.com" })
            {
                Services = new List<DomainServiceListItemResponse>
                {
                    new DomainServiceListItemResponse(
                        new DomainDetail { Name = "exira.com" }, 
                        new DomainDetail.DomainDetailService(
                            Guid.NewGuid(),
                            ServiceType.manual.Value,
                            "My Mail Services")),

                    new DomainServiceListItemResponse(
                        new DomainDetail { Name = "exira.com" },
                        new DomainDetail.DomainDetailService(
                            Guid.NewGuid(),
                            ServiceType.googlesuite.Value,
                            ServiceType.googlesuite.DisplayName))
                }
            };
    }
}
