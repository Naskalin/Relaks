using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class CreateFileCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileCategories_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileCategories_FileCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FileCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CategoryId",
                table: "Files",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_EntryId",
                table: "FileCategories",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_ParentId",
                table: "FileCategories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileCategories_CategoryId",
                table: "Files",
                column: "CategoryId",
                principalTable: "FileCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileCategories_CategoryId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "FileCategories");

            migrationBuilder.DropIndex(
                name: "IX_Files_CategoryId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
