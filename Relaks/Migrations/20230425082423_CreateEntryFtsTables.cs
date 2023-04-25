using Microsoft.EntityFrameworkCore.Migrations;
using Relaks.Database.DbUtils;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntryFtsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                FtsMigrationHelper.CreateFtsTable(new FtsMigrationHelper.TableData() 
                {
                    Table = "FtsEntries",
                    Unindexed = new []{"Id"},
                    Columns = new[] {"Id", "Body"},
                })
            );

            migrationBuilder.Sql(
                FtsMigrationHelper.CreateFtsTable(new FtsMigrationHelper.TableData() 
                {
                    Table = "FtsEntryInfos",
                    Unindexed = new []{"Id", "EntryId"},
                    Columns = new[] {"Id", "EntryId", "Body"}
                })
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FtsEntryInfos");
            
            migrationBuilder.DropTable(
                name: "FtsEntries");
        }
    }
}