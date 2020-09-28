using Microsoft.EntityFrameworkCore.Migrations;

namespace Quartz.Core.Migrations
{
    public partial class ElementMatchup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementMatchup_Elements_AttackingElementID",
                table: "ElementMatchup");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementMatchup_Elements_DefendingElementID",
                table: "ElementMatchup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElementMatchup",
                table: "ElementMatchup");

            migrationBuilder.RenameTable(
                name: "ElementMatchup",
                newName: "ElementMatchups");

            migrationBuilder.RenameIndex(
                name: "IX_ElementMatchup_DefendingElementID",
                table: "ElementMatchups",
                newName: "IX_ElementMatchups_DefendingElementID");

            migrationBuilder.RenameIndex(
                name: "IX_ElementMatchup_AttackingElementID",
                table: "ElementMatchups",
                newName: "IX_ElementMatchups_AttackingElementID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElementMatchups",
                table: "ElementMatchups",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementMatchups_Elements_AttackingElementID",
                table: "ElementMatchups",
                column: "AttackingElementID",
                principalTable: "Elements",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementMatchups_Elements_DefendingElementID",
                table: "ElementMatchups",
                column: "DefendingElementID",
                principalTable: "Elements",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementMatchups_Elements_AttackingElementID",
                table: "ElementMatchups");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementMatchups_Elements_DefendingElementID",
                table: "ElementMatchups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElementMatchups",
                table: "ElementMatchups");

            migrationBuilder.RenameTable(
                name: "ElementMatchups",
                newName: "ElementMatchup");

            migrationBuilder.RenameIndex(
                name: "IX_ElementMatchups_DefendingElementID",
                table: "ElementMatchup",
                newName: "IX_ElementMatchup_DefendingElementID");

            migrationBuilder.RenameIndex(
                name: "IX_ElementMatchups_AttackingElementID",
                table: "ElementMatchup",
                newName: "IX_ElementMatchup_AttackingElementID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElementMatchup",
                table: "ElementMatchup",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ElementMatchup_Elements_AttackingElementID",
                table: "ElementMatchup",
                column: "AttackingElementID",
                principalTable: "Elements",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementMatchup_Elements_DefendingElementID",
                table: "ElementMatchup",
                column: "DefendingElementID",
                principalTable: "Elements",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
