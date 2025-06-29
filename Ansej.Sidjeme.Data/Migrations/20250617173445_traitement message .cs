using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class traitementmessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTraited",
                table: "ContactUs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "ContactUs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TraitedAt",
                table: "ContactUs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTraited",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "TraitedAt",
                table: "ContactUs");
        }
    }
}
