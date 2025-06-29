using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactAgenceWilayas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(maxLength: 256, nullable: false),
                    CodeWilaya = table.Column<string>(nullable: true),
                    NomWilaya = table.Column<string>(nullable: true),
                    CodeAgence = table.Column<string>(nullable: true),
                    NomAgence = table.Column<string>(nullable: true),
                    AdresseAr = table.Column<string>(nullable: true),
                    AdresseFr = table.Column<string>(nullable: true),
                    AdresseEn = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    TelephoneFixe = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Observations = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAgenceWilayas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactAgenceWilayas");
        }
    }
}
