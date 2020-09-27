using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quartz.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ElementMatchup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    AttackingElementID = table.Column<int>(nullable: false),
                    DefendingElementID = table.Column<int>(nullable: false),
                    Multiplier = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementMatchup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ElementMatchup_Elements_AttackingElementID",
                        column: x => x.AttackingElementID,
                        principalTable: "Elements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementMatchup_Elements_DefendingElementID",
                        column: x => x.DefendingElementID,
                        principalTable: "Elements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementMatchup_AttackingElementID",
                table: "ElementMatchup",
                column: "AttackingElementID");

            migrationBuilder.CreateIndex(
                name: "IX_ElementMatchup_DefendingElementID",
                table: "ElementMatchup",
                column: "DefendingElementID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementMatchup");

            migrationBuilder.DropTable(
                name: "Elements");
        }
    }
}
