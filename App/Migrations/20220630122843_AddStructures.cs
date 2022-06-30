using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddStructures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Structures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structures_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Structures_Structures_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructureConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Direction = table.Column<string>(type: "TEXT", nullable: false),
                    StructureFirstId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StructureSecondId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureConnections_Structures_StructureFirstId",
                        column: x => x.StructureFirstId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructureConnections_Structures_StructureSecondId",
                        column: x => x.StructureSecondId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructureItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StructureId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                        name: "FK_StructureItems_Structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StructureConnections_StructureFirstId",
                table: "StructureConnections",
                column: "StructureFirstId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureConnections_StructureSecondId",
                table: "StructureConnections",
                column: "StructureSecondId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_EntryId",
                table: "StructureItems",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_StructureId",
                table: "StructureItems",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_EntryId",
                table: "Structures",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_ParentId",
                table: "Structures",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructureConnections");

            migrationBuilder.DropTable(
                name: "StructureItems");

            migrationBuilder.DropTable(
                name: "Structures");
        }
    }
}
