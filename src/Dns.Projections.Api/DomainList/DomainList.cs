namespace Dns.Projections.Api.DomainList
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NodaTime;
    using System;

    public class DomainList
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

    public class DomainListConfiguration : IEntityTypeConfiguration<DomainList>
    {
        private const string TableName = "DomainList";

        public void Configure(EntityTypeBuilder<DomainList> b)
        {
            b.ToTable(TableName, Schema.Api)
                .HasKey(x => x.Name)
                .ForSqlServerIsClustered();

            b.Property(x => x.SecondLevelDomain);
            b.Property(x => x.TopLevelDomain);
            b.Property(DomainList.CreatedAtTimestampBackingPropertyName)
                .HasColumnName("CreatedAt");

            b.Ignore(x => x.CreatedAtTimestamp);
        }
    }
}
