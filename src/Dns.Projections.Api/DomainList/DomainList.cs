namespace Dns.Projections.Api.DomainList
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NodaTime;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class DomainList
    {
        public static string CreatedAtTimestampBackingPropertyName = nameof(CreatedAtTimestampAsDateTimeOffset);
        public static string ServicesBackingPropertyName = nameof(ServicesAsString);

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

        public IReadOnlyCollection<DomainListService> Services
        {
            get => GetDeserializedServices();
            set => ServicesAsString = JsonConvert.SerializeObject(value);
        }

        public void AddService(DomainListService service)
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

        private List<DomainListService> GetDeserializedServices()
        {
            return string.IsNullOrEmpty(ServicesAsString)
                ? new List<DomainListService>()
                : JsonConvert.DeserializeObject<List<DomainListService>>(ServicesAsString);
        }

        public class DomainListService
        {
            public Guid ServiceId { get; set; }
            public string Type { get; }
            public string Label { get; set; }

            public DomainListService(
                Guid serviceId,
                string type,
                string label)
            {
                ServiceId = serviceId;
                Type = type;
                Label = label;
            }
        }
    }

    public class DomainListConfiguration : IEntityTypeConfiguration<DomainList>
    {
        private const string TableName = "DomainList";

        public void Configure(EntityTypeBuilder<DomainList> b)
        {
            b.ToTable(TableName, Schema.Api)
                .HasKey(x => x.Name)
                .ForSqlServerIsClustered();

            b.Property(x => x.SecondLevelDomain)
                .HasMaxLength(SecondLevelDomain.MaxLength);

            b.Property(x => x.TopLevelDomain)
                .HasMaxLength(TopLevelDomain.GetAll().Max(x => x.Value.Length) * 2);

            b.Property(DomainList.CreatedAtTimestampBackingPropertyName)
                .HasColumnName("CreatedAt");

            b.Property(DomainList.ServicesBackingPropertyName)
                .HasColumnName("Services");

            b.Ignore(x => x.CreatedAtTimestamp);
            b.Ignore(x => x.Services);
        }
    }
}
