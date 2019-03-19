namespace Dns.Api.Domain.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Projections.Api.DomainDetail;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "Domain", Namespace = "")]
    public class DomainDetailResponse
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

        /// <summary>
        /// Services of the domain.
        /// </summary>
        [DataMember(Name = "Services", Order = 4)]
        public List<DomainDetailServiceResponse> Services { get; set; }

        /// <summary>
        /// Records of the domain.
        /// </summary>
        [DataMember(Name = "Records", Order = 5)]
        public List<DomainDetailRecordResponse> Records { get; set; }

        public DomainDetailResponse(
            DomainDetail domainDetail)
        {
            Name = domainDetail.Name;
            SecondLevelDomain = domainDetail.SecondLevelDomain;
            TopLevelDomain = domainDetail.TopLevelDomain;

            Services = domainDetail
                .Services
                .Select(x => new DomainDetailServiceResponse(x.ServiceId, x.Type, x.Label))
                .ToList();

            Records = domainDetail
                .RecordSet
                .Select(x => new DomainDetailRecordResponse(x.Type, x.TimeToLive, x.Label, x.Value))
                .ToList();
        }
    }

    [DataContract(Name = "DomainService", Namespace = "")]
    public class DomainDetailServiceResponse
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

        public DomainDetailServiceResponse(
            Guid serviceId,
            string type,
            string label)
        {
            ServiceId = serviceId;
            Type = type;
            Label = label;
        }
    }

    [DataContract(Name = "DomainRecord", Namespace = "")]
    public class DomainDetailRecordResponse
    {
        /// <summary>
        /// Type of the domain record.
        /// </summary>
        [DataMember(Name = "Type", Order = 1)]
        public string Type { get; set; }

        /// <summary>
        /// Time to life of the domain record
        /// </summary>
        [DataMember(Name = "TimeToLife", Order = 2)]
        public int TimeToLife { get; }

        /// <summary>
        /// Label of the domain record.
        /// </summary>
        [DataMember(Name = "Label", Order = 3)]
        public string Label { get; set; }

        /// <summary>
        /// Value of the domain record.
        /// </summary>
        [DataMember(Name = "Value", Order = 4)]
        public string Value { get; set; }

        public DomainDetailRecordResponse(
            string type,
            int timeToLife,
            string label,
            string value)
        {
            Type = type;
            TimeToLife = timeToLife;
            Label = label;
            Value = value;
        }
    }

    public class DomainResponseExamples : IExamplesProvider
    {
        private static readonly Random Random = new Random();

        public object GetExamples() =>
            new DomainDetailResponse(
                new DomainDetail
                {
                    Name = "exira.com",
                    SecondLevelDomain = "exira",
                    TopLevelDomain = "com",
                    Services = new[]
                    {
                        new DomainDetail.DomainDetailService(Guid.NewGuid(), ServiceType.googlesuite.Value,
                            ServiceType.googlesuite.DisplayName),
                        new DomainDetail.DomainDetailService(Guid.NewGuid(), ServiceType.manual.Value,
                            "My Mail Server"),
                        new DomainDetail.DomainDetailService(Guid.NewGuid(), ServiceType.manual.Value,
                            "Datacenter Records"),
                    },
                    RecordSet = new[]
                    {
                        new DomainDetail.DomainDetailRecord(RecordType.ns.Value, 3600, "@", "ns1.exira.com"),
                        new DomainDetail.DomainDetailRecord(RecordType.ns.Value, 3600, "@", "ns2.exira.com"),
                        new DomainDetail.DomainDetailRecord(RecordType.a.Value, 3600, "@", $"{Random.Next(1, 255)}.{Random.Next(1, 255)}.{Random.Next(1, 255)}.{Random.Next(1, 255)}"),
                    }
                });
    }
}
