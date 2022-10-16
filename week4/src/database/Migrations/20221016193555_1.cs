using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attracties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attracties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dateTimeBereik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Begin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eind = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dateTimeBereik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gastInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaatstBezochteURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gastInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ondehoud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Probleem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aanAttractieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ondehoud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ondehoud_attracties_aanAttractieId",
                        column: x => x.aanAttractieId,
                        principalTable: "attracties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "coordinaat",
                columns: table => new
                {
                    GastInfoId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinaat", x => x.GastInfoId);
                    table.ForeignKey(
                        name: "FK_coordinaat_gastInfo_GastInfoId",
                        column: x => x.GastInfoId,
                        principalTable: "gastInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gasten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EersteBezoek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BegeleiderId = table.Column<int>(type: "int", nullable: true),
                    gastInfoId = table.Column<int>(type: "int", nullable: false),
                    FavorietId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gasten_attracties_FavorietId",
                        column: x => x.FavorietId,
                        principalTable: "attracties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gasten_Gasten_BegeleiderId",
                        column: x => x.BegeleiderId,
                        principalTable: "Gasten",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gasten_gastInfo_gastInfoId",
                        column: x => x.gastInfoId,
                        principalTable: "gastInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gasten_Gebruikers_Id",
                        column: x => x.Id,
                        principalTable: "Gebruikers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Gebruikers_Id",
                        column: x => x.Id,
                        principalTable: "Gebruikers",
                        principalColumn: "Id");
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
                name: "IX_Gasten_BegeleiderId",
                table: "Gasten",
                column: "BegeleiderId",
                unique: true,
                filter: "[BegeleiderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Gasten_FavorietId",
                table: "Gasten",
                column: "FavorietId");

            migrationBuilder.CreateIndex(
                name: "IX_Gasten_gastInfoId",
                table: "Gasten",
                column: "gastInfoId",
                unique: true,
                filter: "[gastInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ondehoud_aanAttractieId",
                table: "ondehoud",
                column: "aanAttractieId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coordinaat");

            migrationBuilder.DropTable(
                name: "Medewerkers");

            migrationBuilder.DropTable(
                name: "ondehoud");

            migrationBuilder.DropTable(
                name: "reservering");

            migrationBuilder.DropTable(
                name: "dateTimeBereik");

            migrationBuilder.DropTable(
                name: "Gasten");

            migrationBuilder.DropTable(
                name: "attracties");

            migrationBuilder.DropTable(
                name: "gastInfo");

            migrationBuilder.DropTable(
                name: "Gebruikers");
        }
    }
}
