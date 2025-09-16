using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class uniqueNinAndNumberProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_ProjectNumber",
                table: "DeclarationsHonneurMI",
                column: "ProjectNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_ProjectNumber",
                table: "DeclarationsHonneurMI");
        }
    }
}
