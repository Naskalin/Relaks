namespace App.Utils;

public record InsertModel
{
    public string TableName { get; set; } = null!;
    public Dictionary<string, object> Data = new();
}

public record UpdateModel : InsertModel
{
    public Guid WhereId { get; set; }
}

public record DeleteModel
{
    public string TableName { get; set; } = null!;
    public Guid WhereId { get; set; }
}

public static class FtsHelper
{
    public static string InsertRaw(InsertModel model)
    {
        var columns = String.Join(", ", model.Data.Keys.ToList());
        var values = String.Join(", ", model.Data.Values.ToList().Select(x => "'" + x + "'"));
        return $"INSERT INTO {model.TableName}({columns}) VALUES ({values})";
    }

    public static string UpdateRaw(UpdateModel model)
    {
        var values = new List<string>();
        foreach (var item in model.Data)
        {
            values.Add(item.Key + " = " + "'" + item.Value + "'");
        }
        return $"UPDATE {model.TableName} SET {String.Join(", ", values)} WHERE Id = '{model.WhereId}'";
    }

    public static string DeleteRaw(DeleteModel model)
    {
        return $"DELETE FROM {model.TableName} WHERE Id = '{model.WhereId}'";
    }
}