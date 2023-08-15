using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class EntryFileRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntryBaseFile",
                columns: table => new
                {
                    BaseEntryRelationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BaseFileRelationsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntryBaseFile", x => new { x.BaseEntryRelationsId, x.BaseFileRelationsId });
                    table.ForeignKey(
                        name: "FK_BaseEntryBaseFile_Entries_BaseEntryRelationsId",
                        column: x => x.BaseEntryRelationsId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntryBaseFile_Files_BaseFileRelationsId",
                        column: x => x.BaseFileRelationsId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntryBaseFile_BaseFileRelationsId",
                table: "BaseEntryBaseFile",
                column: "BaseFileRelationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntryBaseFile");
        }
    }
}
