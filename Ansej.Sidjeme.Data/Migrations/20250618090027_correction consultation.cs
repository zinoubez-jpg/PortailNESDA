using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class correctionconsultation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministrativeStructureId",
                table: "Consultations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministrativeStructureId",
                table: "Consultations",
                type: "int",
                nullable: true);
        }
    }
}
