using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class AddValueToEntryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "EntryInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE EntryInfos SET Value = json_object('email', Email) WHERE Discriminator == 'Email';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Value = json_object('note', Note) WHERE Discriminator == 'Note';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Value = json_object('url', Url) WHERE Discriminator == 'Url';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Value = json_object('date', Date) WHERE Discriminator == 'Date';");
            migrationBuilder.Sql("UPDATE EntryInfos SET Value = json_object('number', PhoneNumber, 'region', PhoneRegion) WHERE Discriminator == 'Phone';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "EntryInfos");
        }
    }
}
