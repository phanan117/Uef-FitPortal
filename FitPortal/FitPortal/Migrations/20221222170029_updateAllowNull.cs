using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPositions_Teachers_TeacherID",
                table: "TeacherPositions");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherID",
                table: "TeacherPositions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPositions_Teachers_TeacherID",
                table: "TeacherPositions",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPositions_Teachers_TeacherID",
                table: "TeacherPositions");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherID",
                table: "TeacherPositions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPositions_Teachers_TeacherID",
                table: "TeacherPositions",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
