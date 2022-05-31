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
                "Id",
                "EntryId",
                "Title",
                "DeletedReason",
                "PhoneNumber",
                "Email",
                "Url",
                "Note",
            };

            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateFtsTable(new TableNames()
                {
                    Table = "EntryInfoFts",
                    Unindexed = new []{"Id", "EntryId"},
                    Columns = columns
                })
            );

            migrationBuilder.Sql(
                SqliteMigrationHelper.CreateTriggers(new TriggerNames()
                {
                    Columns = columns,
                    TriggerTable = "EntryInfoFts",
                    WatchTable = "EntryInfos",
                    TriggerTableId = "Id",
                    WatchTableId = "Id",
                })
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                SqliteMigrationHelper.DeleteTriggers("EntryInfoFts")
            );
            migrationBuilder.DropTable(
                name: "EntryInfoFts");
        }
    }
}