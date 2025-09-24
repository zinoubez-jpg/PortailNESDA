using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NotificationDate",
                table: "DeclarationsHonneurMI",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Notified",
                table: "DeclarationsHonneurMI",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationDate",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "Notified",
                table: "DeclarationsHonneurMI");
        }
    }
}
