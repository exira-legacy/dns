using Microsoft.EntityFrameworkCore.Migrations;

namespace Dns.Projections.Api.Migrations
{
    public partial class AddDomainDetailRecordSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecordSet",
                schema: "Api",
                table: "DomainDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Services",
                schema: "Api",
                table: "DomainDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordSet",
                schema: "Api",
                table: "DomainDetails");

            migrationBuilder.DropColumn(
                name: "Services",
                schema: "Api",
                table: "DomainDetails");
        }
    }
}
