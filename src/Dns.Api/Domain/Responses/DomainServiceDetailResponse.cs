namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Runtime.Serialization;
    using Projections.Api.ServiceDetail;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "DomainService", Namespace = "")]
    public class DomainServiceDetailResponse
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
        /// Domain service specific data.
        /// </summary>
        [DataMember(Name = "Data", Order = 3)]
        public string Data { get; set; }

        public DomainServiceDetailResponse(
            ServiceDetail service)
        {
            ServiceId = service.ServiceId;
            Type = service.Type;
            Label = service.Label;
            Data = service.ServiceData;
        }
    }

    public class DomainServiceResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainServiceDetailResponse(
                new ServiceDetail
                {
                    ServiceId = Guid.NewGuid(),
                    Type = ServiceType.manual.Value,
                    Label = "My Mail Services"
                });
    }
}
