using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace week6.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "geslachtId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Geslacht",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GeslachtString = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geslacht", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Geslacht",
                columns: new[] { "Id", "GeslachtString" },
                values: new object[] { 1, "Man" });

            migrationBuilder.InsertData(
                table: "Geslacht",
                columns: new[] { "Id", "GeslachtString" },
                values: new object[] { 2, "Vrouw" });

            migrationBuilder.InsertData(
                table: "Geslacht",
                columns: new[] { "Id", "GeslachtString" },
                values: new object[] { 3, "Geheim" });

            migrationBuilder.InsertData(
                table: "Geslacht",
                columns: new[] { "Id", "GeslachtString" },
                values: new object[] { 4, "Anders" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_geslachtId",
                table: "AspNetUsers",
                column: "geslachtId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Geslacht_geslachtId",
                table: "AspNetUsers",
                column: "geslachtId",
                principalTable: "Geslacht",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Geslacht_geslachtId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Geslacht");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_geslachtId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "geslachtId",
                table: "AspNetUsers");
        }
    }
}
