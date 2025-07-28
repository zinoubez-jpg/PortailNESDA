using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class wilayawilayas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communes_Wilayas_WilayaId",
                table: "Communes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Wilayas_WilayaId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilayas_WilayaId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wilayas",
                table: "Wilayas");

            migrationBuilder.RenameTable(
                name: "Wilayas",
                newName: "Wilaya");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wilaya",
                table: "Wilaya",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Communes_Wilaya_WilayaId",
                table: "Communes",
                column: "WilayaId",
                principalTable: "Wilaya",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Wilaya_WilayaId",
                table: "ContactUs",
                column: "WilayaId",
                principalTable: "Wilaya",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilaya_WilayaId",
                table: "PreInscriptionSousTraitence",
                column: "WilayaId",
                principalTable: "Wilaya",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communes_Wilaya_WilayaId",
                table: "Communes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Wilaya_WilayaId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilaya_WilayaId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wilaya",
                table: "Wilaya");

            migrationBuilder.RenameTable(
                name: "Wilaya",
                newName: "Wilayas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wilayas",
                table: "Wilayas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Communes_Wilayas_WilayaId",
                table: "Communes",
                column: "WilayaId",
                principalTable: "Wilayas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Wilayas_WilayaId",
                table: "ContactUs",
                column: "WilayaId",
                principalTable: "Wilayas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilayas_WilayaId",
                table: "PreInscriptionSousTraitence",
                column: "WilayaId",
                principalTable: "Wilayas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
