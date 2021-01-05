using Microsoft.EntityFrameworkCore.Migrations;

namespace MajoliFE.Data.Migrations
{
    public partial class changeDatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "BaseTotal",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Total",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<long>(
                name: "BaseTotal",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
