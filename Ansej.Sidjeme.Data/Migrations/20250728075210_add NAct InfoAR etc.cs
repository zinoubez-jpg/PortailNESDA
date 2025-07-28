using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class addNActInfoARetc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Wilaya_WilayaId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilaya_WilayaId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wilaya",
                table: "Wilaya");

            migrationBuilder.DropColumn(
                name: "NumeroSiejeV3",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.RenameTable(
                name: "Wilaya",
                newName: "Wilayas");

            migrationBuilder.AddColumn<int>(
                name: "CommuneNaissanceId",
                table: "PreInscriptionSousTraitence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNaissance",
                table: "PreInscriptionSousTraitence",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NomAr",
                table: "PreInscriptionSousTraitence",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumActNais",
                table: "PreInscriptionSousTraitence",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrenomAr",
                table: "PreInscriptionSousTraitence",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WilayaNaissanceId",
                table: "PreInscriptionSousTraitence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wilayas",
                table: "Wilayas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Communes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WilayaId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communes_Wilayas_WilayaId",
                        column: x => x.WilayaId,
                        principalTable: "Wilayas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptionSousTraitence_CommuneNaissanceId",
                table: "PreInscriptionSousTraitence",
                column: "CommuneNaissanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Communes_WilayaId",
                table: "Communes",
                column: "WilayaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Wilayas_WilayaId",
                table: "ContactUs",
                column: "WilayaId",
                principalTable: "Wilayas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreInscriptionSousTraitence_Communes_CommuneNaissanceId",
                table: "PreInscriptionSousTraitence",
                column: "CommuneNaissanceId",
                principalTable: "Communes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilayas_WilayaId",
                table: "PreInscriptionSousTraitence",
                column: "WilayaId",
                principalTable: "Wilayas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Wilayas_WilayaId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PreInscriptionSousTraitence_Communes_CommuneNaissanceId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropForeignKey(
                name: "FK_PreInscriptionSousTraitence_Wilayas_WilayaId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropTable(
                name: "Communes");

            migrationBuilder.DropIndex(
                name: "IX_PreInscriptionSousTraitence_CommuneNaissanceId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wilayas",
                table: "Wilayas");

            migrationBuilder.DropColumn(
                name: "CommuneNaissanceId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropColumn(
                name: "DateNaissance",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropColumn(
                name: "NomAr",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropColumn(
                name: "NumActNais",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropColumn(
                name: "PrenomAr",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.DropColumn(
                name: "WilayaNaissanceId",
                table: "PreInscriptionSousTraitence");

            migrationBuilder.RenameTable(
                name: "Wilayas",
                newName: "Wilaya");

            migrationBuilder.AddColumn<string>(
                name: "NumeroSiejeV3",
                table: "PreInscriptionSousTraitence",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wilaya",
                table: "Wilaya",
                column: "Id");

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
                onDelete: ReferentialAction.NoAction);
        }
    }
}
