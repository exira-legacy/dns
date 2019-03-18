using Microsoft.EntityFrameworkCore.Migrations;

namespace Dns.Projections.Api.Migrations
{
    public partial class AddDomainListServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TopLevelDomain",
                schema: "Api",
                table: "DomainList",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondLevelDomain",
                schema: "Api",
                table: "DomainList",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Services",
                schema: "Api",
                table: "DomainList",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TopLevelDomain",
                schema: "Api",
                table: "DomainDetails",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondLevelDomain",
                schema: "Api",
                table: "DomainDetails",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Services",
                schema: "Api",
                table: "DomainList");

            migrationBuilder.AlterColumn<string>(
                name: "TopLevelDomain",
                schema: "Api",
                table: "DomainList",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondLevelDomain",
                schema: "Api",
                table: "DomainList",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TopLevelDomain",
                schema: "Api",
                table: "DomainDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondLevelDomain",
                schema: "Api",
                table: "DomainDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);
        }
    }
}
