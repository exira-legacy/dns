namespace Dns.Projections.Api.DomainDetail
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NodaTime;
    using System;

    public class DomainDetail
    {
        public static string CreatedAtTimestampBackingPropertyName = nameof(CreatedAtTimestampAsDateTimeOffset);

        public string Name { get; set; }

        public string SecondLevelDomain { get; set; }

        public string TopLevelDomain { get; set; }

        private DateTimeOffset CreatedAtTimestampAsDateTimeOffset { get; set; }

        public Instant CreatedAtTimestamp
        {
            get => Instant.FromDateTimeOffset(CreatedAtTimestampAsDateTimeOffset);
            set => CreatedAtTimestampAsDateTimeOffset = value.ToDateTimeOffset();
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

            b.Property(x => x.SecondLevelDomain);
            b.Property(x => x.TopLevelDomain);
            b.Property(DomainDetail.CreatedAtTimestampBackingPropertyName)
                .HasColumnName("CreatedAt");

            b.Ignore(x => x.CreatedAtTimestamp);
        }
    }
}
