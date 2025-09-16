using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class UpdateDeclarationHonneurMICaptchadelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecaptchaResponse",
                table: "DeclarationsHonneurMI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecaptchaResponse",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
