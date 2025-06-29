using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class consultation_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentStructureManagerId",
                table: "StructureManager",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StructureManager_ParentStructureManagerId",
                table: "StructureManager",
                column: "ParentStructureManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StructureManager_StructureManager_ParentStructureManagerId",
                table: "StructureManager",
                column: "ParentStructureManagerId",
                principalTable: "StructureManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StructureManager_StructureManager_ParentStructureManagerId",
                table: "StructureManager");

            migrationBuilder.DropIndex(
                name: "IX_StructureManager_ParentStructureManagerId",
                table: "StructureManager");

            migrationBuilder.DropColumn(
                name: "ParentStructureManagerId",
                table: "StructureManager");
        }
    }
}
