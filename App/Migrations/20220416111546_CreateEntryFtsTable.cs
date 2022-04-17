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
                nameof(EntryFts.Id),
                nameof(EntryFts.Name),
                nameof(EntryFts.Description),
                nameof(EntryFts.DeletedReason),
            };
            
            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateFtsTable(
                    new TableNames() 
                    {
                        Table = nameof(EntryFts),
                        Unindexed = new []{nameof(EntryFts.Id)},
                        Columns = columns,
                    }
                )
            );
            
            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateTriggers(
                    new TriggerNames()
                    {
                        Columns = columns,
                        TriggerTable = nameof(EntryFts),
                        WatchTable = "Entries",
                        TriggerTableId = nameof(EntryFts.Id),
                        WatchTableId = nameof(Post.Id),
                    }
                )
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                SqliteMigrationHelper.DeleteTriggers(nameof(EntryFts))
            );
            
            migrationBuilder.DropTable(
                name: "EntryFts");
        }
    }
}
