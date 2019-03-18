namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        /// <summary>
        /// Services of the domain.
        /// </summary>
        [DataMember(Name = "Services", Order = 2)]
        public DomainListItemServiceResponse[] Services { get; set; }

        public DomainListItemResponse(
            DomainList domainList)
        {
            Name = domainList.Name;

            Services = domainList
                .Services
                .Select(x => new DomainListItemServiceResponse(x.ServiceId, x.Type, x.Label))
                .ToArray();
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

        public DomainListItemServiceResponse(
            Guid serviceId,
            string type,
            string label)
        {
            ServiceId = serviceId;
            Type = type;
            Label = label;
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
                            Services = new DomainList.DomainListService[]
                            {
                                new DomainList.DomainListService(Guid.NewGuid(), "googlesuite", "Google Suite"),
                                new DomainList.DomainListService(Guid.NewGuid(), "manual", "My Mail Server"), // TODO: get rid of magic types
                                new DomainList.DomainListService(Guid.NewGuid(), "manual", "Datacenter Records"),
                            }
                        }),
                    new DomainListItemResponse(new DomainList
                    {
                        Name = "cumps.be",
                        Services = new DomainList.DomainListService[]
                        {
                            new DomainList.DomainListService(Guid.NewGuid(), "googlesuite", "Google Suite"),
                        }
                    }),
                }
            };
    }
}
