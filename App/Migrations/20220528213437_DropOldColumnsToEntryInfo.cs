using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class DropOldColumnsToEntryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "EntryInfos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EntryInfos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "EntryInfos");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "EntryInfos");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "EntryInfos");

            migrationBuilder.DropColumn(
                name: "PhoneRegion",
                table: "EntryInfos");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "EntryInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "EntryInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EntryInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EntryInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "EntryInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "EntryInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneRegion",
                table: "EntryInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "EntryInfos",
                type: "TEXT",
                nullable: true);
        }
    }
}
