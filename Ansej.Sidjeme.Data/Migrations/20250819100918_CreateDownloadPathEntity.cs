using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class CreateDownloadPathEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessRegisterFilePath",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "CertificateNifFilePath",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "DeclarationHonorFilePath",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "ForecastProgramFilePath",
                table: "DeclarationsHonneurMI");

            migrationBuilder.CreateTable(
                name: "TypeFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationFr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DownloadFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeFileId = table.Column<int>(type: "int", nullable: false),
                    DeclarationHonneurMIId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadFile_DeclarationsHonneurMI_DeclarationHonneurMIId",
                        column: x => x.DeclarationHonneurMIId,
                        principalTable: "DeclarationsHonneurMI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadFile_TypeFile_TypeFileId",
                        column: x => x.TypeFileId,
                        principalTable: "TypeFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadFile_DeclarationHonneurMIId",
                table: "DownloadFile",
                column: "DeclarationHonneurMIId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadFile_TypeFileId",
                table: "DownloadFile",
                column: "TypeFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadFile");

            migrationBuilder.DropTable(
                name: "TypeFile");

            migrationBuilder.AddColumn<string>(
                name: "BusinessRegisterFilePath",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateNifFilePath",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
