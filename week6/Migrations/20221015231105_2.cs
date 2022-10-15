using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace week6.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedBy",
                columns: table => new
                {
                    GastId = table.Column<string>(type: "TEXT", nullable: false),
                    AttractieId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedBy", x => new { x.GastId, x.AttractieId });
                    table.ForeignKey(
                        name: "FK_LikedBy_AspNetUsers_GastId",
                        column: x => x.GastId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedBy_Attractie_AttractieId",
                        column: x => x.AttractieId,
                        principalTable: "Attractie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedBy_AttractieId",
                table: "LikedBy",
                column: "AttractieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedBy");
        }
    }
}
