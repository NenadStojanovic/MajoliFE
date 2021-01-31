using Microsoft.EntityFrameworkCore.Migrations;

namespace MajoliFE.Data.Migrations
{
    public partial class addedAccountNumberToVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Vendors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Vendors");
        }
    }
}
