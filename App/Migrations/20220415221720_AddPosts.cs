using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AnyField = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });
            
            migrationBuilder.Sql(@"
CREATE VIRTUAL TABLE PostFts USING fts5(Id UNINDEXED, Title, Description);

-- Triggers to keep the FTS index up to date.
CREATE TRIGGER PostFts_ai AFTER INSERT ON Posts BEGIN
    INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
END;
CREATE TRIGGER PostFts_ad AFTER DELETE ON Posts BEGIN
    DELETE FROM PostFts WHERE Id = old.Id;
END;
CREATE TRIGGER PostFts_au AFTER UPDATE ON Posts BEGIN
    DELETE FROM PostFts WHERE Id = old.Id;
    INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
END;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostFts");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
