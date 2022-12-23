using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class addWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTaked = table.Column<bool>(type: "bit", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastMofify = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachersWorks",
                columns: table => new
                {
                    TeachersId = table.Column<int>(type: "int", nullable: false),
                    WorksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersWorks", x => new { x.TeachersId, x.WorksId });
                    table.ForeignKey(
                        name: "FK_TeachersWorks_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersWorks_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWorks_WorksId",
                table: "TeachersWorks",
                column: "WorksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachersWorks");

            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}
