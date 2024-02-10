using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4everStore.Migrations
{
    /// <inheritdoc />
    public partial class addq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalprice",
                table: "requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "totalprice",
                table: "requests");
        }
    }
}
