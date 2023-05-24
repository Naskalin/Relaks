using Microsoft.EntityFrameworkCore.Migrations;
using Relaks.Database.DbUtils;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class CreateFtsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql(
                FtsMigrationHelper.CreateFtsTable(new CreateFtsTableData()
                {
                    Table = "FtsEntries",
                    Columns = new List<CreateFtsTableColumn>()
                    {
                        new() {Name = "Id", IsUnindexed = true},
                        new() {Name = "DeletedAt", IsUnindexed = true},
                        new() {Name = "Discriminator", IsUnindexed = true},
                        new() {Name = "Body"},
                    }
                })
            );

            migrationBuilder.Sql(
                FtsMigrationHelper.CreateFtsTable(new CreateFtsTableData()
                {
                    Table = "FtsEntryInfos",
                    Columns = new List<CreateFtsTableColumn>()
                    {
                        new() {Name = "Id", IsUnindexed = true},
                        new() {Name = "EntryId", IsUnindexed = true},
                        new() {Name = "DeletedAt", IsUnindexed = true},
                        new() {Name = "Discriminator", IsUnindexed = true},
                        new() {Name = "Body"},
                    }
                })
            );
            
            migrationBuilder.Sql(
                FtsMigrationHelper.CreateFtsTable(new CreateFtsTableData()
                {
                    Table = "FtsFiles",
                    Columns = new List<CreateFtsTableColumn>()
                    {
                        new() {Name = "Id", IsUnindexed = true},
                        new() {Name = "DeletedAt", IsUnindexed = true},
                        new() {Name = "Discriminator", IsUnindexed = true},
                        new() {Name = "Body"},
                    }
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
            
            migrationBuilder.DropTable(
                name: "FtsFiles");
        }
    }
}