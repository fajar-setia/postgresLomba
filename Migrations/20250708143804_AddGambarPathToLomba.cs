using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteLomba.Migrations
{
    /// <inheritdoc />
    public partial class AddGambarPathToLomba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GambarPath",
                table: "Lombas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GambarPath",
                table: "Lombas");
        }
    }
}
