using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class initialmigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomWilaya",
                table: "ContactAgenceWilayas");

            migrationBuilder.AddColumn<string>(
                name: "NomWilayaAr",
                table: "ContactAgenceWilayas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomWilayaEn",
                table: "ContactAgenceWilayas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomWilayaFr",
                table: "ContactAgenceWilayas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomWilayaAr",
                table: "ContactAgenceWilayas");

            migrationBuilder.DropColumn(
                name: "NomWilayaEn",
                table: "ContactAgenceWilayas");

            migrationBuilder.DropColumn(
                name: "NomWilayaFr",
                table: "ContactAgenceWilayas");

            migrationBuilder.AddColumn<string>(
                name: "NomWilaya",
                table: "ContactAgenceWilayas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
