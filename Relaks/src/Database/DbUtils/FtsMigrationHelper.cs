namespace Relaks.Database.DbUtils;

public class CreateFtsTableData
{
    public string Table { get; set; } = null!;
    // public string[] Columns = Array.Empty<string>();
    // public string[] Unindexed { get; set; } = null!;
    public List<CreateFtsTableColumn> Columns { get; set; } = new();
}


public class CreateFtsTableColumn
{
    public string Name { get; set; } = null!;
    public bool? IsUnindexed { get; set; }
}

public static class FtsMigrationHelper
{
   

    // public static string CreateFtsTable(TableData data)
    // {
    //     var fields = String.Join(", ", data.Columns.Select(x => data.Unindexed.Contains(x) ? x + " UNINDEXED" : x));
    //     return $"CREATE VIRTUAL TABLE {data.Table} USING fts5({fields});";
    // }
    
    public static string CreateFtsTable(CreateFtsTableData data)
    {
        if (!data.Columns.Any()) throw new ArgumentException("Columns is empty");
        if (string.IsNullOrEmpty(data.Table)) throw new ArgumentException("Table name is missing");
        var columns = string.Join(", ", data.Columns.Select(x => x.Name + (x.IsUnindexed == true ? " UNINDEXED" : "")));
        return $"CREATE VIRTUAL TABLE {data.Table} USING fts5({columns});";
    }
}