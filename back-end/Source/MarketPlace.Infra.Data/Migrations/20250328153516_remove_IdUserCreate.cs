using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class remove_IdUserCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUserCreate",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUserCreate",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
