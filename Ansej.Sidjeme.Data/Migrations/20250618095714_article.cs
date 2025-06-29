using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Summary = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(maxLength: 500, nullable: true),
                    IsPinned = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    AuthorId = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    deletedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_Slug",
                table: "Article",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");
        }
    }
}
