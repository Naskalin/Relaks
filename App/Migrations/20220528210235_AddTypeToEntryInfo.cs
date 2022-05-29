using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddTypeToEntryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EntryInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.Sql("UPDATE EntryInfos SET Type = Discriminator;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "EntryInfos");
        }
    }
}
