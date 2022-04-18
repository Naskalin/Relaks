using System;
using App.Models;
using App.Utils.Extensions.Database;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class CreateEntryInfoFtsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var columns = new[]
            {
                nameof(EntryInfoFts.Id),
                nameof(EntryInfoFts.EntryId),
                nameof(EntryInfoFts.Title),
                nameof(EntryInfoFts.DeletedReason),
                nameof(EntryInfoFts.PhoneNumber),
                nameof(EntryInfoFts.Email),
                nameof(EntryInfoFts.Url),
                nameof(EntryInfoFts.Note),
            };

            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateFtsTable(new TableNames()
                {
                    Table = nameof(EntryInfoFts),
                    Unindexed = new []{nameof(EntryInfoFts.Id), nameof(EntryInfoFts.EntryId)},
                    Columns = columns
                })
            );

            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateTriggers(new TriggerNames()
                {
                    Columns = columns,
                    TriggerTable = nameof(EntryInfoFts),
                    WatchTable = "EntryInfos",
                    TriggerTableId = nameof(EntryInfoFts.Id),
                    WatchTableId = nameof(EntryInfo.Id),
                })
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                SqliteMigrationHelper.DeleteTriggers(nameof(EntryInfoFts))
            );
            migrationBuilder.DropTable(
                name: "EntryInfoFts");
        }
    }
}