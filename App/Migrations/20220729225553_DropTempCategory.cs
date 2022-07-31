

#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
namespace App.Migrations
{
    public partial class DropTempCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Создаём новые категории из TempCategory
            migrationBuilder.Sql(@"INSERT INTO FileCategories (Id, Title, EntryId, Discriminator, CreatedAt, UpdatedAt)
SELECT hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6)),    
       TempCategory,
       EntryId,
       'EntryFileCategory',
       datetime(),
       datetime()
FROM Files
WHERE TempCategory <> ''
GROUP BY EntryId, TempCategory");

            // Проставляем созданный Files.CategoryId
            migrationBuilder.Sql(@"UPDATE Files
SET CategoryId = (SELECT fc.Id
                        FROM FileCategories fc
                        WHERE Files.EntryId = fc.EntryId
                          AND Files.TempCategory = fc.Title)
WHERE Files.TempCategory != '';");
            
            migrationBuilder.DropColumn(
                name: "TempCategory",
                table: "Files");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete from FileCategories");
            migrationBuilder.AddColumn<string>(
                name: "TempCategory",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
