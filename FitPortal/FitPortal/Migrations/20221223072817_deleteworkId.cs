using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class deleteworkId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersWorks_Works_WorkId",
                table: "TeachersWorks");

            migrationBuilder.DropIndex(
                name: "IX_TeachersWorks_WorkId",
                table: "TeachersWorks");

            migrationBuilder.DropColumn(
                name: "WorkId",
                table: "TeachersWorks");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersWorks_Works_WorksId",
                table: "TeachersWorks");

            migrationBuilder.DropIndex(
                name: "IX_TeachersWorks_WorksId",
                table: "TeachersWorks");

            migrationBuilder.AddColumn<int>(
                name: "WorkId",
                table: "TeachersWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
