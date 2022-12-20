using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "TeacherPositions");

            migrationBuilder.AddColumn<int>(
                name: "ManagerID",
                table: "Specializations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_ManagerID",
                table: "Specializations",
                column: "ManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_Teachers_ManagerID",
                table: "Specializations",
                column: "ManagerID",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_Teachers_ManagerID",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_ManagerID",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "ManagerID",
                table: "Specializations");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "TeacherPositions",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
