using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dns.Projections.Api.Migrations
{
    public partial class AddDomainDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Api");

            migrationBuilder.CreateTable(
                name: "DomainDetails",
                schema: "Api",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    SecondLevelDomain = table.Column<string>(nullable: true),
                    TopLevelDomain = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainDetails", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "ProjectionStates",
                schema: "Api",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectionStates", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainDetails",
                schema: "Api");

            migrationBuilder.DropTable(
                name: "ProjectionStates",
                schema: "Api");
        }
    }
}
