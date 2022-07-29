using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddTempCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TempCategory",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.Sql("UPDATE Files SET TempCategory = Category;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempCategory",
                table: "Files");
        }
    }
}
