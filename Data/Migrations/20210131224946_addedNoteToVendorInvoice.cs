using Microsoft.EntityFrameworkCore.Migrations;

namespace MajoliFE.Data.Migrations
{
    public partial class addedNoteToVendorInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "VendorInvoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "VendorInvoices");
        }
    }
}
