using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateCreate",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DateUpdate",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUserCreate",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUserUpdate",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdUserCreate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdUserUpdate",
                table: "Users");
        }
    }
}
