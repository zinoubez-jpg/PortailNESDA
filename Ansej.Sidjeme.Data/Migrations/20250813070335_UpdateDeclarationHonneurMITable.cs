using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class UpdateDeclarationHonneurMITable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.RenameColumn(
                name: "LegalStatusId",
                table: "DeclarationsHonneurMI",
                newName: "TypeOfNeed");

            migrationBuilder.AlterColumn<string>(
                name: "LegalStatus",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Activity",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivitySector",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeclarationHonorFilePath",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForecastProgramFilePath",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivitySector",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "DeclarationHonorFilePath",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "ForecastProgramFilePath",
                table: "DeclarationsHonneurMI");

            migrationBuilder.RenameColumn(
                name: "TypeOfNeed",
                table: "DeclarationsHonneurMI",
                newName: "LegalStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "LegalStatus",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Activity",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "DeclarationsHonneurMI",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "DeclarationsHonneurMI",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
