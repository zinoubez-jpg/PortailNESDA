using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class initialmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeAgence",
                table: "ContactAgenceWilayas");

            migrationBuilder.DropColumn(
                name: "NomAgence",
                table: "ContactAgenceWilayas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeAgence",
                table: "ContactAgenceWilayas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomAgence",
                table: "ContactAgenceWilayas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
