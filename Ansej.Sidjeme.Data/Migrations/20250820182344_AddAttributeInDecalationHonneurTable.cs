using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddAttributeInDecalationHonneurTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Requestdate",
                table: "DeclarationsHonneurMI",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StructureManagerId",
                table: "DeclarationsHonneurMI",
                type: "int",
                nullable: false,
                defaultValue: 4);

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_StructureManagerId",
                table: "DeclarationsHonneurMI",
                column: "StructureManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationsHonneurMI_StructureManager_StructureManagerId",
                table: "DeclarationsHonneurMI",
                column: "StructureManagerId",
                principalTable: "StructureManager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationsHonneurMI_StructureManager_StructureManagerId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_StructureManagerId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "Requestdate",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "StructureManagerId",
                table: "DeclarationsHonneurMI");
        }
    }
}
