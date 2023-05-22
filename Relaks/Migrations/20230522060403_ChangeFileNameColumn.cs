using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "Files",
                newName: "Filename");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "Files",
                newName: "Extension");
        }
    }
}
