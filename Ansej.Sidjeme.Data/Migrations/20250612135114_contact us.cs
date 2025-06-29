using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class contactus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wilaya",
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
                    table.PrimaryKey("PK_Wilaya", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Subject = table.Column<string>(maxLength: 150, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(maxLength: 10, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 512, nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    WilayaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_Wilaya_WilayaId",
                        column: x => x.WilayaId,
                        principalTable: "Wilaya",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_WilayaId",
                table: "ContactUs",
                column: "WilayaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "Wilaya");
        }
    }
}
