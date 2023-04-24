namespace Relaks.Database.DbUtils;

public static class FtsMigrationHelper
{
    public class TableData
    {
        public string Table { get; set; } = null!;
        public string[] Columns = Array.Empty<string>();
        public string[] Unindexed { get; set; } = null!;
    }

    public static string CreateFtsTable(TableData data)
    {
        var fields = String.Join(", ", data.Columns.Select(x => data.Unindexed.Contains(x) ? x + " UNINDEXED" : x));
        return $"CREATE VIRTUAL TABLE {data.Table} USING fts5({fields});";
    }
}