namespace Dns.Api.Domain.Requests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Dns.Domain.Services.Manual;
    using Dns.Domain.Services.Manual.Commands;
    using Swashbuckle.AspNetCore.Filters;

    public class RecordData
    {
        public string Type { get; set; }
        public int TimeToLive { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public Record ToRecord() =>
            new Record(
                RecordType.FromValue(Type.ToLowerInvariant()),
                new TimeToLive(TimeToLive),
                new RecordLabel(Label),
                new RecordValue(Value));
    }

    public class AddManualServiceRequest
    {
        /// <summary>Service id of the manual service to add.</summary>
        [Required]
        public Guid ServiceId { get; set; }

        /// <summary>Label of the manual service to add.</summary>
        [Required]
        public string Label { get; set; }

        /// <summary>Records of the manual service to add.</summary>
        [Required]
        public List<RecordData> Records { get; set; }
    }

    public class AddManualServiceRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddManualServiceRequest
            {
                ServiceId = Guid.NewGuid(),
                Label = "My Special Set",
                Records = new List<RecordData>
                {
                    new RecordData
                    {
                        Type = "TXT",
                        TimeToLive = 3600,
                        Label = "@",
                        Value = "some-value"
                    }
                }
            };
        }
    }

    public static class AddManualServiceRequestMapping
    {
        public static AddManual Map(
            DomainName domainName,
            AddManualServiceRequest message)
        {
            return new AddManual(
                domainName,
                new ServiceId(message.ServiceId),
                new ManualLabel(message.Label),
                new RecordSet(message.Records.Select(x => x.ToRecord())));
        }
    }
}
