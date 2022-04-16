namespace App.Utils.Extensions.Database;

public class TriggerModel
{
    public string TargetTable { get; set; } = null!;
    public string TriggeredTable { get; set; } = null!;
    public string IdField { get; set; } = null!;
    public string[] Fields = Array.Empty<string>();
}

public class SqliteMigrationHelper
{
    public static string CreateFtsTable(TriggerModel model)
    {
        return $"CREATE VIRTUAL TABLE PostFts USING fts5(Id UNINDEXED, Title, Description);";
    }
    
    // CREATE TRIGGER PostFts_ai AFTER INSERT ON Posts BEGIN
    // ...
    // END;
    public static string CreateAfterInsertTrigger(TriggerModel model)
    {
        return $@"
            CREATE TRIGGER {model.TriggeredTable}_ai AFTER INSERT ON {model.TargetTable} BEGIN
                {InsertTriggerBody(model)}
            END;  
";
    }

    // INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
    private static string InsertTriggerBody(TriggerModel model)
    {
        var fields = String.Join(", ", model.Fields);
        var values = String.Join(", ", model.Fields.Select(x => "new." + x));

        return $"INSERT INTO {model.TriggeredTable}({fields}) VALUES ({values});";
    }

    // CREATE TRIGGER PostFts_ad AFTER DELETE ON Posts BEGIN
    // ...
    // END;
    public static string CreateAfterDeleteTrigger(TriggerModel model)
    {
        return $@"
            CREATE TRIGGER {model.TriggeredTable}_ad AFTER DELETE ON {model.TargetTable} BEGIN
                {DeleteTriggerBody(model)}
            END;
";
    }
    
    // DELETE FROM PostFts WHERE Id = old.Id;
    private static string DeleteTriggerBody(TriggerModel model)
    {
        return $"DELETE FROM {model.TriggeredTable} WHERE {model.IdField} = old.{model.IdField};";
    }

    // CREATE TRIGGER PostFts_au AFTER UPDATE ON Posts BEGIN
    // DELETE FROM PostFts WHERE Id = old.Id;
    // INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
    // END;
    public static string CreateAfterUpdateTrigger(TriggerModel model)
    {
        return $@"
            CREATE TRIGGER {nameof(model.TriggeredTable)}_au AFTER UPDATE ON {nameof(model.TargetTable)} BEGIN
                {DeleteTriggerBody(model)}
                {InsertTriggerBody(model)}
            END;
";
    }
    

    // CREATE VIRTUAL TABLE PostFts USING fts5(Id UNINDEXED, Title, Description);
    //
    //     -- Triggers to keep the FTS index up to date.
    //CREATE TRIGGER PostFts_ai AFTER INSERT ON Posts BEGIN
    // INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
    // END;
    // CREATE TRIGGER PostFts_ad AFTER DELETE ON Posts BEGIN
    // DELETE FROM PostFts WHERE Id = old.Id;
    // END;
    // CREATE TRIGGER PostFts_au AFTER UPDATE ON Posts BEGIN
    // DELETE FROM PostFts WHERE Id = old.Id;
    // INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
    // END;
}