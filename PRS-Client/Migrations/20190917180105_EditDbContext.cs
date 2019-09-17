using Microsoft.EntityFrameworkCore.Migrations;

namespace PRS_Client.Migrations
{
    public partial class EditDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Requests",
                type: "decimal(11,2)",
                nullable: false,
                defaultValueSql: "(0)",
                oldClrType: typeof(decimal),
                oldType: "decimal(11,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                maxLength: 10,
                nullable: false,
                defaultValueSql: "('NEW')",
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "Requests",
                maxLength: 20,
                nullable: false,
                defaultValueSql: "('Pickup')",
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RequestLines",
                nullable: false,
                defaultValueSql: "(0)",
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Requests",
                type: "decimal(11,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,2)",
                oldDefaultValueSql: "(0)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldDefaultValueSql: "('NEW')");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryMode",
                table: "Requests",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldDefaultValueSql: "('Pickup')");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RequestLines",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValueSql: "(0)");
        }
    }
}
