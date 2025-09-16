using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddSectorAttributeInActivityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Activites_SecteurActivites_SecteurActiviteId",
            //    table: "Activites");

            //migrationBuilder.RenameColumn(
            //    name: "SecteurActiviteId",
            //    table: "Activites",
            //    newName: "secteurActiviteId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Activites_SecteurActiviteId",
            //    table: "Activites",
            //    newName: "IX_Activites_secteurActiviteId");

            migrationBuilder.AddColumn<int>(
                name: "secteurActiviteId",
                table: "Activites",
                type: "int",
                nullable: false,
                defaultValue: 1);
               //// oldClrType: typeof(int),
               //// oldType: "int",
               // oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activites_SecteurActivites_secteurActiviteId",
                table: "Activites",
                column: "secteurActiviteId",
                principalTable: "SecteurActivites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
               name: "IX_Activites_SecteurActiviteId",
               table: "Activites",
               column: "secteurActiviteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activites_SecteurActivites_secteurActiviteId",
                table: "Activites");

            migrationBuilder.RenameColumn(
                name: "secteurActiviteId",
                table: "Activites",
                newName: "SecteurActiviteId");

            migrationBuilder.RenameIndex(
                name: "IX_Activites_secteurActiviteId",
                table: "Activites",
                newName: "IX_Activites_SecteurActiviteId");

            migrationBuilder.AlterColumn<int>(
                name: "SecteurActiviteId",
                table: "Activites",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activites_SecteurActivites_SecteurActiviteId",
                table: "Activites",
                column: "SecteurActiviteId",
                principalTable: "SecteurActivites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
