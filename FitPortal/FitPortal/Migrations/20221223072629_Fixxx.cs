using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class Fixxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersWorks_Works_WorksId",
                table: "TeachersWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachersWorks",
                table: "TeachersWorks");

            migrationBuilder.DropIndex(
                name: "IX_TeachersWorks_WorksId",
                table: "TeachersWorks");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeachersWorks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "WorkId",
                table: "TeachersWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachersWorks",
                table: "TeachersWorks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWorks_TeachersId",
                table: "TeachersWorks",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWorks_WorkId",
                table: "TeachersWorks",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersWorks_Works_WorkId",
                table: "TeachersWorks",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersWorks_Works_WorkId",
                table: "TeachersWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachersWorks",
                table: "TeachersWorks");

            migrationBuilder.DropIndex(
                name: "IX_TeachersWorks_TeachersId",
                table: "TeachersWorks");

            migrationBuilder.DropIndex(
                name: "IX_TeachersWorks_WorkId",
                table: "TeachersWorks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeachersWorks");

            migrationBuilder.DropColumn(
                name: "WorkId",
                table: "TeachersWorks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachersWorks",
                table: "TeachersWorks",
                columns: new[] { "TeachersId", "WorksId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWorks_WorksId",
                table: "TeachersWorks",
                column: "WorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersWorks_Works_WorksId",
                table: "TeachersWorks",
                column: "WorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
