using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddMotifTraitementAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotifTraitementId",
                table: "DeclarationsHonneurMI",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_MotifTraitementId",
                table: "DeclarationsHonneurMI",
                column: "MotifTraitementId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationsHonneurMI_MotifsTraitement_MotifTraitementId",
                table: "DeclarationsHonneurMI",
                column: "MotifTraitementId",
                principalTable: "MotifsTraitement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationsHonneurMI_MotifsTraitement_MotifTraitementId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_MotifTraitementId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "MotifTraitementId",
                table: "DeclarationsHonneurMI");
        }
    }
}
