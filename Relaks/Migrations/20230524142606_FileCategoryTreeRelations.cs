using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class FileCategoryTreeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileCategories_FileCategories_ParentId",
                table: "FileCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_FileCategories_FileCategories_ParentId",
                table: "FileCategories",
                column: "ParentId",
                principalTable: "FileCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileCategories_FileCategories_ParentId",
                table: "FileCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_FileCategories_FileCategories_ParentId",
                table: "FileCategories",
                column: "ParentId",
                principalTable: "FileCategories",
                principalColumn: "Id");
        }
    }
}
