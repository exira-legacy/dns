namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Runtime.Serialization;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "DomainService", Namespace = "")]
    public class DomainServiceResponse
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

        public DomainServiceResponse(
            ServiceId id,
            string type,
            string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }
    }

    public class DomainServiceResponseExamples : IExamplesProvider
    {
        // TODO: See if we can turn this type into a value object and have some constants on the services
        public object GetExamples()
            => new DomainServiceResponse(new ServiceId(Guid.NewGuid()), "manual", "My Mail Services");
    }
}
