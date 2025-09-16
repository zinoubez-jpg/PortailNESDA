using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddDeclarationHonneurMI_TABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeclarationsHonneurMI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    FirstlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIN = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIF = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LegalStatusId = table.Column<int>(type: "int", nullable: false),
                    LegalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WilayaId = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestProcessed = table.Column<bool>(type: "bit", nullable: false),
                    Processingdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecaptchaResponse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateNifFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessRegisterFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeclarationsHonneurMI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeclarationsHonneurMI_Wilaya_WilayaId",
                        column: x => x.WilayaId,
                        principalTable: "Wilaya",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_WilayaId",
                table: "DeclarationsHonneurMI",
                column: "WilayaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeclarationsHonneurMI");
        }
    }
}
