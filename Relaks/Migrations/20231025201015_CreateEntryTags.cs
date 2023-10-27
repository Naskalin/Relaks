using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntryTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntryTagCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TreePath = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryTagCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryTagCategories_EntryTagCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "EntryTagCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryTagTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryTagTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryTagTitles_EntryTagCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EntryTagCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryTags_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryTags_EntryTagTitles_TagId",
                        column: x => x.TagId,
                        principalTable: "EntryTagTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryTagCategories_ParentId",
                table: "EntryTagCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTags_EntryId",
                table: "EntryTags",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTags_TagId",
                table: "EntryTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTagTitles_CategoryId",
                table: "EntryTagTitles",
                column: "CategoryId");
            
            // создаём категорию профессии
            var professionCategoryId = Guid.NewGuid().ToString();
            migrationBuilder.Sql(@$"INSERT INTO EntryTagCategories (Id, TreePath, Title)
            VALUES ('{professionCategoryId.ToUpper()}', '/{professionCategoryId.ToLower()}', 'Профессии');");
            
            // заполняем дочернии категории для профессий
            migrationBuilder.Sql($@"INSERT INTO EntryTagCategories (Id, ParentId, TreePath, Title)
            SELECT Id, '{professionCategoryId.ToUpper()}', '/{professionCategoryId.ToLower()}/' || LOWER(Id), Title FROM ProfessionCategories;");
            
            // заполняем профессии
            migrationBuilder.Sql(@"INSERT INTO EntryTagTitles (Id, Title, CategoryId, CreatedAt, UpdatedAt)
            SELECT Id, Title, CategoryId, datetime('now'), datetime('now') FROM Professions;");
            
            // переносим связи
            migrationBuilder.Sql(@"INSERT INTO EntryTags (Id, EntryId, TagId)
            SELECT hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6)), EntriesId, ProfessionsId FROM BaseEntryProfession;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryTags");

            migrationBuilder.DropTable(
                name: "EntryTagTitles");

            migrationBuilder.DropTable(
                name: "EntryTagCategories");
        }
    }
}
