using Microsoft.EntityFrameworkCore.Migrations;

namespace Quartz.Core.Migrations
{
    public partial class AddNameToTileBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TileBases",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TileBases");
        }
    }
}
