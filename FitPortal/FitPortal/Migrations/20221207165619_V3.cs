using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitPortal.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PosstInfor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory");

            migrationBuilder.RenameTable(
                name: "PostCategory",
                newName: "PostCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PostInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostInformation_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostInformation_PostCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "PostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostInformation_CategoryID",
                table: "PostInformation",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PostInformation_UserID",
                table: "PostInformation",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories");

            migrationBuilder.RenameTable(
                name: "PostCategories",
                newName: "PostCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategory",
                table: "PostCategory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PosstInfor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosstInfor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosstInfor_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PosstInfor_PostCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "PostCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PosstInfor_CategoryID",
                table: "PosstInfor",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PosstInfor_UserID",
                table: "PosstInfor",
                column: "UserID");
        }
    }
}
