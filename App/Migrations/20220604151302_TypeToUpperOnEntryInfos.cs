using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class TypeToUpperOnEntryInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'EMAIL' WHERE Type == 'Email';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'PHONE' WHERE Type == 'Phone';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'NOTE' WHERE Type == 'Note';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'URL' WHERE Type == 'Url';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'DATE' WHERE Type == 'Date';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'Email' WHERE Type == 'EMAIL';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'Phone' WHERE Type == 'PHONE';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'Note' WHERE Type == 'NOTE';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'Url' WHERE Type == 'URL';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = 'Date' WHERE Type == 'DATE';");
        }
    }
}
