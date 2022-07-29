using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class DropTempCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO FileCategories (Id, Title, EntryId, Discriminator, CreatedAt, UpdatedAt)
SELECT Id, TempCategory, EntryId, 'EntryFileCategory', datetime(), datetime()
FROM Files
WHERE TempCategory <> ''
GROUP BY EntryId, TempCategory");
            
            migrationBuilder.DropColumn(
                name: "TempCategory",
                table: "Files");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TempCategory",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
