﻿// <auto-generated />
using System;
using Dns.Projections.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dns.Projections.Api.Migrations
{
    [DbContext(typeof(ApiProjectionsContext))]
    [Migration("20190317105454_AddDomainDetails")]
    partial class AddDomainDetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("SecondLevelDomain");

                    b.Property<string>("TopLevelDomain");

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("DomainDetails","Api");
                });
#pragma warning restore 612, 618
        }
    }
}
