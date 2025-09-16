using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class soustraitence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecaptchaResponse",
                table: "ContactUs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Activites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerimetreActivites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerimetreActivites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreInscriptionSousTraitence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    NumeroSiejeV3 = table.Column<string>(nullable: true),
                    RaisonSocial = table.Column<string>(nullable: true),
                    WilayaId = table.Column<int>(nullable: false),
                    PerimetreActiviteId = table.Column<int>(nullable: false),
                    ActiviteId = table.Column<int>(nullable: false),
                    NIN = table.Column<string>(maxLength: 18, nullable: false),
                    NomFr = table.Column<string>(maxLength: 128, nullable: false),
                    PrenomFr = table.Column<string>(maxLength: 128, nullable: false),
                    NumTelephone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    RecaptchaResponse = table.Column<string>(nullable: false),
                    IsTraited = table.Column<bool>(nullable: false),
                    IsNesda = table.Column<bool>(nullable: false),
                    Observation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreInscriptionSousTraitence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreInscriptionSousTraitence_Activites_ActiviteId",
                        column: x => x.ActiviteId,
                        principalTable: "Activites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PreInscriptionSousTraitence_PerimetreActivites_PerimetreActiviteId",
                        column: x => x.PerimetreActiviteId,
                        principalTable: "PerimetreActivites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PreInscriptionSousTraitence_Wilaya_WilayaId",
                        column: x => x.WilayaId,
                        principalTable: "Wilaya",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptionSousTraitence_ActiviteId",
                table: "PreInscriptionSousTraitence",
                column: "ActiviteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptionSousTraitence_PerimetreActiviteId",
                table: "PreInscriptionSousTraitence",
                column: "PerimetreActiviteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptionSousTraitence_WilayaId",
                table: "PreInscriptionSousTraitence",
                column: "WilayaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreInscriptionSousTraitence");

            migrationBuilder.DropTable(
                name: "Activites");

            migrationBuilder.DropTable(
                name: "PerimetreActivites");

            migrationBuilder.DropColumn(
                name: "RecaptchaResponse",
                table: "ContactUs");
        }
    }
}
