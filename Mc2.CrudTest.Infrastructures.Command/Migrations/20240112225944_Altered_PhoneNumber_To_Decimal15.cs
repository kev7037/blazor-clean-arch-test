using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Infrastructures.Command.Migrations
{
    /// <inheritdoc />
    public partial class Altered_PhoneNumber_To_Decimal15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PhoneNumber",
                table: "Customers",
                type: "DECIMAL(15,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "VARCHAR(15)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(15,0)");
        }
    }
}
