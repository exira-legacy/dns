namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
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

        public DomainServiceListItemResponse(
            DomainDetail.DomainDetailService domainDetailService)
        {
            ServiceId = domainDetailService.ServiceId;
            Type = domainDetailService.Type;
            Label = domainDetailService.Label;
        }
    }

    public class DomainServiceListResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainServiceListResponse
            {
                Services = new List<DomainServiceListItemResponse>
                {
                    new DomainServiceListItemResponse(
                        new DomainDetail.DomainDetailService(
                            Guid.NewGuid(),
                            ServiceType.manual.Value,
                            "My Mail Services")),

                    new DomainServiceListItemResponse(
                        new DomainDetail.DomainDetailService(
                            Guid.NewGuid(),
                            ServiceType.googlesuite.Value,
                            ServiceType.googlesuite.DisplayName))
                }
            };
    }
}
