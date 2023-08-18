using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class CreateStructureTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureGroups_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructureGroups_StructureGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "StructureGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StructureItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: true),
                    StructureGroupId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureItems_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructureItems_StructureGroups_StructureGroupId",
                        column: x => x.StructureGroupId,
                        principalTable: "StructureGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StructureGroups_EntryId",
                table: "StructureGroups",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureGroups_ParentId",
                table: "StructureGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_EntryId",
                table: "StructureItems",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_StructureGroupId",
                table: "StructureItems",
                column: "StructureGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructureItems");

            migrationBuilder.DropTable(
                name: "StructureGroups");
        }
    }
}
