using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "coordinateId",
                table: "GastInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "coordinaat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinaat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reservering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GastId = table.Column<int>(type: "int", nullable: false),
                    AttractieId = table.Column<int>(type: "int", nullable: false),
                    GedurendeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservering", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservering_attracties_AttractieId",
                        column: x => x.AttractieId,
                        principalTable: "attracties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservering_dateTimeBereik_GedurendeId",
                        column: x => x.GedurendeId,
                        principalTable: "dateTimeBereik",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reservering_Gasten_GastId",
                        column: x => x.GastId,
                        principalTable: "Gasten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GastInfo_coordinateId",
                table: "GastInfo",
                column: "coordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_reservering_AttractieId",
                table: "reservering",
                column: "AttractieId");

            migrationBuilder.CreateIndex(
                name: "IX_reservering_GastId",
                table: "reservering",
                column: "GastId");

            migrationBuilder.CreateIndex(
                name: "IX_reservering_GedurendeId",
                table: "reservering",
                column: "GedurendeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GastInfo_coordinaat_coordinateId",
                table: "GastInfo",
                column: "coordinateId",
                principalTable: "coordinaat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GastInfo_coordinaat_coordinateId",
                table: "GastInfo");

            migrationBuilder.DropTable(
                name: "coordinaat");

            migrationBuilder.DropTable(
                name: "reservering");

            migrationBuilder.DropIndex(
                name: "IX_GastInfo_coordinateId",
                table: "GastInfo");

            migrationBuilder.DropColumn(
                name: "coordinateId",
                table: "GastInfo");
        }
    }
}
