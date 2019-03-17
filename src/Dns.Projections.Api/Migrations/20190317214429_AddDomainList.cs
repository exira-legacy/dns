using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dns.Projections.Api.Migrations
{
    public partial class AddDomainList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomainList",
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
                    table.PrimaryKey("PK_DomainList", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainList",
                schema: "Api");
        }
    }
}
