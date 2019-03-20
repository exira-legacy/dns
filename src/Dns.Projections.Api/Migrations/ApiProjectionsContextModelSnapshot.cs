﻿// <auto-generated />
using System;
using Dns.Projections.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dns.Projections.Api.Migrations
{
    [DbContext(typeof(ApiProjectionsContext))]
    partial class ApiProjectionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.ProjectionHandling.Runner.ProjectionStates.ProjectionStateItem", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Position");

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("ProjectionStates","Api");
                });

            modelBuilder.Entity("Dns.Projections.Api.DomainDetail.DomainDetail", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAtTimestampAsDateTimeOffset")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("RecordSetAsString")
                        .HasColumnName("RecordSet");

                    b.Property<string>("SecondLevelDomain")
                        .HasMaxLength(64);

                    b.Property<string>("ServicesAsString")
                        .HasColumnName("Services");

                    b.Property<string>("TopLevelDomain")
                        .HasMaxLength(20);

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("DomainDetails","Api");
                });

            modelBuilder.Entity("Dns.Projections.Api.DomainList.DomainList", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAtTimestampAsDateTimeOffset")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("SecondLevelDomain")
                        .HasMaxLength(64);

                    b.Property<string>("ServicesAsString")
                        .HasColumnName("Services");

                    b.Property<string>("TopLevelDomain")
                        .HasMaxLength(20);

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("DomainList","Api");
                });

            modelBuilder.Entity("Dns.Projections.Api.ServiceDetail.ServiceDetail", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label")
                        .HasMaxLength(500);

                    b.Property<string>("ServiceData");

                    b.Property<string>("Type")
                        .HasMaxLength(22);

                    b.HasKey("ServiceId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("ServiceDetails","Api");
                });
#pragma warning restore 612, 618
        }
    }
}
