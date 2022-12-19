using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostInformation_AspNetUsers_UserID",
                table: "PostInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_PostInformation_PostCategories_CategoryID",
                table: "PostInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPosition_Specialization_SpecializationID",
                table: "TeacherPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPosition_Teachers_TeacherID",
                table: "TeacherPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherPosition",
                table: "TeacherPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInformation",
                table: "PostInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories");

            migrationBuilder.RenameTable(
                name: "TeacherPosition",
                newName: "TeacherPositions");

            migrationBuilder.RenameTable(
                name: "PostInformation",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "PostCategories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherPosition_TeacherID",
                table: "TeacherPositions",
                newName: "IX_TeacherPositions_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherPosition_SpecializationID",
                table: "TeacherPositions",
                newName: "IX_TeacherPositions_SpecializationID");

            migrationBuilder.RenameIndex(
                name: "IX_PostInformation_UserID",
                table: "Posts",
                newName: "IX_Posts_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_PostInformation_CategoryID",
                table: "Posts",
                newName: "IX_Posts_CategoryID");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Teachers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Specialization",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherPositions",
                table: "TeacherPositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPositions_Specialization_SpecializationID",
                table: "TeacherPositions",
                column: "SpecializationID",
                principalTable: "Specialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPositions_Teachers_TeacherID",
                table: "TeacherPositions",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPositions_Specialization_SpecializationID",
                table: "TeacherPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPositions_Teachers_TeacherID",
                table: "TeacherPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherPositions",
                table: "TeacherPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Specialization");

            migrationBuilder.RenameTable(
                name: "TeacherPositions",
                newName: "TeacherPosition");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "PostInformation");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "PostCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherPositions_TeacherID",
                table: "TeacherPosition",
                newName: "IX_TeacherPosition_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherPositions_SpecializationID",
                table: "TeacherPosition",
                newName: "IX_TeacherPosition_SpecializationID");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserID",
                table: "PostInformation",
                newName: "IX_PostInformation_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CategoryID",
                table: "PostInformation",
                newName: "IX_PostInformation_CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherPosition",
                table: "TeacherPosition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInformation",
                table: "PostInformation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostInformation_AspNetUsers_UserID",
                table: "PostInformation",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostInformation_PostCategories_CategoryID",
                table: "PostInformation",
                column: "CategoryID",
                principalTable: "PostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPosition_Specialization_SpecializationID",
                table: "TeacherPosition",
                column: "SpecializationID",
                principalTable: "Specialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPosition_Teachers_TeacherID",
                table: "TeacherPosition",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
