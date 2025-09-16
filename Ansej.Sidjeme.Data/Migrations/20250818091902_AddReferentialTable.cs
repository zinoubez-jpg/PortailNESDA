using Microsoft.EntityFrameworkCore.Migrations;

namespace Ansej.Sidjeme.Data.Migrations
{
    public partial class AddReferentialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "ActivitySector",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "LegalStatus",
                table: "DeclarationsHonneurMI");

            migrationBuilder.RenameColumn(
                name: "TypeOfNeed",
                table: "DeclarationsHonneurMI",
                newName: "TypeOfNeedId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "LegalStatusId",
                table: "DeclarationsHonneurMI",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "SecteurActiviteId",
            //    table: "Activites",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "FormesJuridique",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormesJuridique", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecteurActivites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecteurActivites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfNeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignationFr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationAr = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DesignationEn = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfNeed", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_ActivityId",
                table: "DeclarationsHonneurMI",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_GenderId",
                table: "DeclarationsHonneurMI",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_LegalStatusId",
                table: "DeclarationsHonneurMI",
                column: "LegalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeclarationsHonneurMI_TypeOfNeedId",
                table: "DeclarationsHonneurMI",
                column: "TypeOfNeedId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Activites_SecteurActiviteId",
            //    table: "Activites",
            //    column: "SecteurActiviteId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Activites_SecteurActivites_SecteurActiviteId",
            //    table: "Activites",
            //    column: "SecteurActiviteId",
            //    principalTable: "SecteurActivites",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationsHonneurMI_Activites_ActivityId",
                table: "DeclarationsHonneurMI",
                column: "ActivityId",
                principalTable: "Activites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationsHonneurMI_FormesJuridique_LegalStatusId",
                table: "DeclarationsHonneurMI",
                column: "LegalStatusId",
                principalTable: "FormesJuridique",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationsHonneurMI_Genres_GenderId",
                table: "DeclarationsHonneurMI",
                column: "GenderId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeclarationsHonneurMI_TypesOfNeed_TypeOfNeedId",
                table: "DeclarationsHonneurMI",
                column: "TypeOfNeedId",
                principalTable: "TypesOfNeed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activites_SecteurActivites_SecteurActiviteId",
                table: "Activites");

            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationsHonneurMI_Activites_ActivityId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationsHonneurMI_FormesJuridique_LegalStatusId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationsHonneurMI_Genres_GenderId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropForeignKey(
                name: "FK_DeclarationsHonneurMI_TypesOfNeed_TypeOfNeedId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropTable(
                name: "FormesJuridique");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "SecteurActivites");

            migrationBuilder.DropTable(
                name: "TypesOfNeed");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_ActivityId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_GenderId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_LegalStatusId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropIndex(
                name: "IX_DeclarationsHonneurMI_TypeOfNeedId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropIndex(
                name: "IX_Activites_SecteurActiviteId",
                table: "Activites");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "LegalStatusId",
                table: "DeclarationsHonneurMI");

            migrationBuilder.DropColumn(
                name: "SecteurActiviteId",
                table: "Activites");

            migrationBuilder.RenameColumn(
                name: "TypeOfNeedId",
                table: "DeclarationsHonneurMI",
                newName: "TypeOfNeed");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActivitySector",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LegalStatus",
                table: "DeclarationsHonneurMI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
