namespace Dns.Projections.Api.DomainDetail
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NodaTime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class DomainDetail
    {
        public static string CreatedAtTimestampBackingPropertyName = nameof(CreatedAtTimestampAsDateTimeOffset);
        public static string ServicesBackingPropertyName = nameof(ServicesAsString);
        public static string RecordSetBackingPropertyName = nameof(RecordSetAsString);

        public string Name { get; set; }

        public string SecondLevelDomain { get; set; }

        public string TopLevelDomain { get; set; }

        private DateTimeOffset CreatedAtTimestampAsDateTimeOffset { get; set; }

        public Instant CreatedAtTimestamp
        {
            get => Instant.FromDateTimeOffset(CreatedAtTimestampAsDateTimeOffset);
            set => CreatedAtTimestampAsDateTimeOffset = value.ToDateTimeOffset();
        }

        private string ServicesAsString { get; set; }

        public IReadOnlyCollection<DomainDetailService> Services
        {
            get => GetDeserializedServices();
            set => ServicesAsString = JsonConvert.SerializeObject(value);
        }

        public void AddService(DomainDetailService service)
        {
            var services = GetDeserializedServices();
            services.Add(service);
            Services = services;
        }

        public void RemoveService(Guid serviceId)
        {
            var services = GetDeserializedServices();
            var service = services.First(x => x.ServiceId == serviceId);
            services.Remove(service);
            Services = services;
        }

        private List<DomainDetailService> GetDeserializedServices()
        {
            return string.IsNullOrEmpty(ServicesAsString)
                ? new List<DomainDetailService>()
                : JsonConvert.DeserializeObject<List<DomainDetailService>>(ServicesAsString);
        }

        public class DomainDetailService
        {
            public Guid ServiceId { get; set; }
            public string Type { get; }
            public string Label { get; set; }

            public DomainDetailService(
                Guid serviceId,
                string type,
                string label)
            {
                ServiceId = serviceId;
                Type = type;
                Label = label;
            }
        }

        private string RecordSetAsString { get; set; }

        public IReadOnlyCollection<DomainDetailRecord> RecordSet
        {
            get => GetDeserializedRecordSet();
            set => RecordSetAsString = JsonConvert.SerializeObject(value);
        }

        private List<DomainDetailRecord> GetDeserializedRecordSet()
        {
            return string.IsNullOrEmpty(RecordSetAsString)
                ? new List<DomainDetailRecord>()
                : JsonConvert.DeserializeObject<List<DomainDetailRecord>>(RecordSetAsString);
        }

        public class DomainDetailRecord
        {
            // TODO: Services + complete recordset
        }
    }

    public class DomainDetailConfiguration : IEntityTypeConfiguration<DomainDetail>
    {
        private const string TableName = "DomainDetails";

        public void Configure(EntityTypeBuilder<DomainDetail> b)
        {
            b.ToTable(TableName, Schema.Api)
                .HasKey(x => x.Name)
                .ForSqlServerIsClustered();

            b.Property(x => x.SecondLevelDomain)
                .HasMaxLength(SecondLevelDomain.MaxLength);

            b.Property(x => x.TopLevelDomain)
                .HasMaxLength(TopLevelDomain.GetAll().Max(x => x.Value.Length) * 2);

            b.Property(DomainDetail.CreatedAtTimestampBackingPropertyName)
                .HasColumnName("CreatedAt");

            b.Property(DomainDetail.ServicesBackingPropertyName)
                .HasColumnName("Services");

            b.Property(DomainDetail.RecordSetBackingPropertyName)
                .HasColumnName("RecordSet");

            b.Ignore(x => x.CreatedAtTimestamp);
            b.Ignore(x => x.Services);
            b.Ignore(x => x.RecordSet);
        }
    }
}
