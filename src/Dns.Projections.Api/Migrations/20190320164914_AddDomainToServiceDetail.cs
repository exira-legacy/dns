using Microsoft.EntityFrameworkCore.Migrations;

namespace Dns.Projections.Api.Migrations
{
    public partial class AddDomainToServiceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domain",
                schema: "Api",
                table: "ServiceDetails",
                maxLength: 84,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                schema: "Api",
                table: "ServiceDetails");
        }
    }
}
