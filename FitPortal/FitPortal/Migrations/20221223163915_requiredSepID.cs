using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class requiredSepID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Specializations_SpeID",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Class_ClassID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUser_AspNetUsers_UserID",
                table: "StudentUser");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUser_Students_StudentID",
                table: "StudentUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUser",
                table: "StudentUser");

            migrationBuilder.RenameTable(
                name: "StudentUser",
                newName: "StudentUsers");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUser_UserID",
                table: "StudentUsers",
                newName: "IX_StudentUsers_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUser_StudentID",
                table: "StudentUsers",
                newName: "IX_StudentUsers_StudentID");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpeID",
                table: "Class",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUsers",
                table: "StudentUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Specializations_SpeID",
                table: "Class",
                column: "SpeID",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Class_ClassID",
                table: "Students",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_AspNetUsers_UserID",
                table: "StudentUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsers_Students_StudentID",
                table: "StudentUsers",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Specializations_SpeID",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Class_ClassID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_AspNetUsers_UserID",
                table: "StudentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsers_Students_StudentID",
                table: "StudentUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUsers",
                table: "StudentUsers");

            migrationBuilder.RenameTable(
                name: "StudentUsers",
                newName: "StudentUser");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUsers_UserID",
                table: "StudentUser",
                newName: "IX_StudentUser_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUsers_StudentID",
                table: "StudentUser",
                newName: "IX_StudentUser_StudentID");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SpeID",
                table: "Class",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUser",
                table: "StudentUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Specializations_SpeID",
                table: "Class",
                column: "SpeID",
                principalTable: "Specializations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Class_ClassID",
                table: "Students",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUser_AspNetUsers_UserID",
                table: "StudentUser",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUser_Students_StudentID",
                table: "StudentUser",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
