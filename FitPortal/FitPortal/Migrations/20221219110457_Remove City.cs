using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPositions_Specialization_SpecializationID",
                table: "TeacherPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialization",
                table: "Specialization");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Specialization",
                newName: "Specializations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPositions_Specializations_SpecializationID",
                table: "TeacherPositions",
                column: "SpecializationID",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPositions_Specializations_SpecializationID",
                table: "TeacherPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations");

            migrationBuilder.RenameTable(
                name: "Specializations",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teachers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialization",
                table: "Specialization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPositions_Specialization_SpecializationID",
                table: "TeacherPositions",
                column: "SpecializationID",
                principalTable: "Specialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
