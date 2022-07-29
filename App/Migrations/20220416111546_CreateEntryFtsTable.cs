using System;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class CreateEntryFtsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var columns = new[]
            {
                "Id",
                "Name",
                "Description",
                "DeletedReason",
            };
            
            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateFtsTable(
                    new TableNames() 
                    {
                        Table = "EntryFts",
                        Unindexed = new []{"Id"},
                        Columns = columns,
                    }
                )
            );
            
            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateTriggers(
                    new TriggerNames()
                    {
                        Columns = columns,
                        TriggerTable = "EntryFts",
                        WatchTable = "Entries",
                        TriggerTableId = "Id",
                        WatchTableId = nameof(Entry.Id),
                    }
                )
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                SqliteMigrationHelper.DeleteTriggers("EntryFts")
            );
            
            migrationBuilder.DropTable(
                name: "EntryFts");
        }
    }
}
