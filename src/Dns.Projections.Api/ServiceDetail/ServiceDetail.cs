namespace Dns.Projections.Api.ServiceDetail
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Linq;

    public class ServiceDetail
    {
        public string Domain { get; set; }

        public Guid ServiceId { get; set; }

        public string Type { get; set; }

        public string Label { get; set; }

        public string ServiceData { get; set; }
    }

    public class ServiceDetailConfiguration : IEntityTypeConfiguration<ServiceDetail>
    {
        private const string TableName = "ServiceDetails";

        public void Configure(EntityTypeBuilder<ServiceDetail> b)
        {
            b.ToTable(TableName, Schema.Api)
                .HasKey(x => x.ServiceId)
                .ForSqlServerIsClustered();

            b.Property(x => x.Type)
                .HasMaxLength(ServiceType.GetAll().Max(x => x.Value.Length) * 2);

            b.Property(x => x.Label)
                .HasMaxLength(ServiceLabel.MaxLength);

            b.Property(x => x.ServiceData);

            b.Property(x => x.Domain)
                .HasMaxLength(SecondLevelDomain.MaxLength + (TopLevelDomain.GetAll().Max(x => x.Value.Length) * 2));
        }
    }
}
