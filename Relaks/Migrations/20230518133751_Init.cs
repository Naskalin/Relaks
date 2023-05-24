using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Alpha2 = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TitleRu = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    TitleEn = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Native = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Continent = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Capital = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartAtWithTime = table.Column<bool>(type: "INTEGER", nullable: false),
                    EndAtWithTime = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntryInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: true),
                    CustomValue = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithTime = table.Column<bool>(type: "INTEGER", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Region = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryInfos_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FileTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileTags_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Filename = table.Column<string>(type: "TEXT", nullable: false),
                    MimeType = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedReason = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Keyword = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Files_FileCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FileCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BaseFileBaseFileTag",
                columns: table => new
                {
                    FilesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseFileBaseFileTag", x => new { x.FilesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BaseFileBaseFileTag_FileTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "FileTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseFileBaseFileTag_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseFileBaseFileTag_TagsId",
                table: "BaseFileBaseFileTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryInfos_EntryId",
                table: "EntryInfos",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_EntryId",
                table: "FileCategories",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_ParentId",
                table: "FileCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CategoryId",
                table: "Files",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_EntryId",
                table: "Files",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FileTags_EntryId",
                table: "FileTags",
                column: "EntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseFileBaseFileTag");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "EntryInfos");

            migrationBuilder.DropTable(
                name: "FileTags");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "FileCategories");

            migrationBuilder.DropTable(
                name: "Entries");
        }
    }
}
