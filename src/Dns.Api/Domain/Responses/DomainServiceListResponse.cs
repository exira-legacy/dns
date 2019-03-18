namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
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
        public ServiceId Id { get; set; }

        /// <summary>
        /// Type of the domain service.
        /// </summary>
        [DataMember(Name = "Type", Order = 2)]
        public string Type { get; set; }

        /// <summary>
        /// Name of the domain service.
        /// </summary>
        [DataMember(Name = "Name", Order = 3)]
        public string Name { get; set; }

        public DomainServiceListItemResponse(
            ServiceId id,
            string type,
            string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }
    }

    public class DomainServiceListResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new DomainServiceListResponse
            {
                Services = new List<DomainServiceListItemResponse>
                {
                    new DomainServiceListItemResponse(new ServiceId(Guid.NewGuid()), ServiceType.manual.Value, "My Mail Services"),
                    new DomainServiceListItemResponse(new ServiceId(Guid.NewGuid()), ServiceType.googlesuite.Value, ServiceType.googlesuite.DisplayName)
                }
            };
    }
}
