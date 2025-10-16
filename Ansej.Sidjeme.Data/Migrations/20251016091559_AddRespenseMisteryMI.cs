using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddRespenseMisteryMI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GarantedMC",
                table: "DeclarationsHonneurMI",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "GarantedMCDate",
                table: "DeclarationsHonneurMI",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservationMC",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GarantedMC",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "GarantedMCDate",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "ObservationMC",
                table: "DeclarationsHonneurMI");
        }
    }
}
