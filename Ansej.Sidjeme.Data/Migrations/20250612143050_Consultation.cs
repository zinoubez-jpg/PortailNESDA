using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class Consultation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructureManager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(maxLength: 150, nullable: false),
                    NameFr = table.Column<string>(maxLength: 150, nullable: false),
                    NameEn = table.Column<string>(maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(maxLength: 500, nullable: false),
                    TitleFr = table.Column<string>(maxLength: 500, nullable: false),
                    TitleEn = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    StructureManagerId = table.Column<int>(nullable: false),
                    AdministrativeStructureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_StructureManager_StructureManagerId",
                        column: x => x.StructureManagerId,
                        principalTable: "StructureManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_StructureManagerId",
                table: "Consultations",
                column: "StructureManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "StructureManager");
        }
    }
}
