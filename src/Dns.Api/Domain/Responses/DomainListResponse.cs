namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using Infrastructure.Responses;
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

        /// <summary>
        /// Hypermedia links
        /// </summary>
        [DataMember(Name = "Links", Order = 2)]
        public List<Link> Links { get; set; }

        public DomainListResponse()
        {
            Links = new List<Link>
            {
                new Link("/", Link.Relations.Home, WebRequestMethods.Http.Get),
                new Link("/domains", Link.Relations.Domains, WebRequestMethods.Http.Post)
            };
        }
    }

    [DataContract(Name = "Domain", Namespace = "")]
    public class DomainListItemResponse
    {
        /// <summary>
        /// Name of the domain.
        /// </summary>
        [DataMember(Name = "Name", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Services of the domain.
        /// </summary>
        [DataMember(Name = "Services", Order = 2)]
        public List<DomainListItemServiceResponse> Services { get; set; }

        /// <summary>
        /// Hypermedia links
        /// </summary>
        [DataMember(Name = "Links", Order = 3)]
        public List<Link> Links { get; set; }

        public DomainListItemResponse(
            DomainList domainList)
        {
            Name = domainList.Name;

            Services = domainList
                .Services
                .Select(x => new DomainListItemServiceResponse(domainList.Name, x.ServiceId, x.Type, x.Label))
                .ToList();

            Links = new List<Link>
            {
                new Link($"/domains/{domainList.Name}", Link.Relations.Domain, WebRequestMethods.Http.Get),
                new Link($"/domains/{domainList.Name}/services", Link.Relations.Services, WebRequestMethods.Http.Get)
            };
        }
    }

    [DataContract(Name = "DomainService", Namespace = "")]
    public class DomainListItemServiceResponse
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
        public string Type { get; }

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

        public DomainListItemServiceResponse(
            string domainName,
            Guid serviceId,
            string type,
            string label)
        {
            ServiceId = serviceId;
            Type = type;
            Label = label;

            Links = new List<Link>
            {
                new Link($"/domains/{domainName}/services/{serviceId}", Link.Relations.Service, WebRequestMethods.Http.Get)
            };
        }
    }

    public class DomainListResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainListResponse
            {
                Domains = new List<DomainListItemResponse>
                {
                    new DomainListItemResponse(
                        new DomainList
                        {
                            Name = "exira.com",
                            Services = new[]
                            {
                                new DomainList.DomainListService(Guid.NewGuid(), ServiceType.googlesuite.Value, ServiceType.googlesuite.DisplayName),
                                new DomainList.DomainListService(Guid.NewGuid(), ServiceType.manual.Value, "My Mail Server"),
                                new DomainList.DomainListService(Guid.NewGuid(), ServiceType.manual.Value, "Datacenter Records"),
                            }
                        }),
                    new DomainListItemResponse(new DomainList
                    {
                        Name = "cumps.be",
                        Services = new[]
                        {
                            new DomainList.DomainListService(Guid.NewGuid(), ServiceType.googlesuite.Value, ServiceType.googlesuite.DisplayName),
                        }
                    }),
                }
            };
    }
}
