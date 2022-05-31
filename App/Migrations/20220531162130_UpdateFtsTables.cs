

#nullable disable

using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore.Migrations;
namespace App.Migrations
{
    public partial class UpdateFtsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove triggers
            migrationBuilder.Sql(SqliteMigrationHelper.DeleteTriggers("EntryFts"));
            migrationBuilder.Sql(SqliteMigrationHelper.DeleteTriggers("EntryInfoFts"));
            
            // Drop Fts tables
            migrationBuilder.DropTable(name: "EntryFts");
            migrationBuilder.DropTable(name: "EntryInfoFts");

            // Create new Fts tables
            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateFtsTable(new TableNames() 
                {
                    Table = "FtsEntries",
                    Unindexed = new []{"Id"},
                    Columns = new[] {"Id", "Data"},
                })
            );

            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateFtsTable(new TableNames()
                {
                    Table = "FtsEntryInfos",
                    Unindexed = new []{"Id", "EntryId"},
                    Columns = new[] {"Id", "EntryId", "Data"}
                })
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
