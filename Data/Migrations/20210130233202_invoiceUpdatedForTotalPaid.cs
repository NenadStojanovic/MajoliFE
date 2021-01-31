using Microsoft.EntityFrameworkCore.Migrations;

namespace MajoliFE.Data.Migrations
{
    public partial class invoiceUpdatedForTotalPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalPaid",
                table: "Invoices",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "Invoices");
        }
    }
}
