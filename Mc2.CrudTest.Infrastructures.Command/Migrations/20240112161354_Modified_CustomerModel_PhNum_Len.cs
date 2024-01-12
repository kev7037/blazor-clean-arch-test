using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Infrastructures.Command.Migrations
{
    /// <inheritdoc />
    public partial class Modified_CustomerModel_PhNum_Len : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "VARCHAR(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "VARCHAR(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(15)");
        }
    }
}
