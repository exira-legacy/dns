using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dns.Projections.Api.Migrations
{
    public partial class AddServiceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceDetails",
                schema: "Api",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 22, nullable: true),
                    Label = table.Column<string>(maxLength: 500, nullable: true),
                    ServiceData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetails", x => x.ServiceId)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceDetails",
                schema: "Api");
        }
    }
}
