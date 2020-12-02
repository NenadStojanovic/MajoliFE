using Microsoft.EntityFrameworkCore.Migrations;

namespace MajoliFE.Data.Migrations
{
    public partial class CustomersExpanded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MB",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PIB",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartnerId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MB",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PIB",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Customers");
        }
    }
}
