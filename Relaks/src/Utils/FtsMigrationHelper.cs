namespace Relaks.Utils;

public static class FtsMigrationHelper
{
    // public class TriggerNames
    // {
    //     public string WatchTable { get; set; } = null!;
    //     public string WatchTableId { get; set; } = null!;
    //     public string[] Columns = Array.Empty<string>();
    //     
    //     public string TriggerTable { get; set; } = null!;
    //     public string TriggerTableId { get; set; } = null!;
    // }
    
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
//     public enum TriggerTypeEnum
//     {
//         Insert,
//         Delete,
//         Update,
//     }
//     
//
//     public static string DeleteTriggers(string triggerTableName)
//     {
//         return $@"
// DROP TRIGGER IF EXISTS {GetTriggerName(triggerTableName, TriggerTypeEnum.Insert)};
// DROP TRIGGER IF EXISTS {GetTriggerName(triggerTableName, TriggerTypeEnum.Delete)};
// DROP TRIGGER IF EXISTS {GetTriggerName(triggerTableName, TriggerTypeEnum.Update)};
// ";
//     }
//     
//     public static string CreateTriggers(TriggerNames names)
//     {
//         var insertBody = InsertTriggerBody(names.TriggerTable, names.Columns);
//         var deleteBody = DeleteTriggerBody(names.TriggerTable, names.TriggerTableId, names.WatchTableId);
//         var updateBody = $@"{deleteBody}
//     {insertBody}";
//
//         var insertTrigger = CreateTriggerWrap(TriggerTypeEnum.Insert, names.TriggerTable, names.WatchTable, insertBody);
//         var deleteTrigger = CreateTriggerWrap(TriggerTypeEnum.Delete, names.TriggerTable, names.WatchTable, deleteBody);
//         var updateTrigger = CreateTriggerWrap(TriggerTypeEnum.Update, names.TriggerTable, names.WatchTable, updateBody);
//
//         return $@"
// {insertTrigger}
// {deleteTrigger}
// {updateTrigger}
// ";
//     }
//     
//     // INSERT INTO PostFts(Id, Title, Description) VALUES (new.Id, new.Title, new.Description);
//     private static string InsertTriggerBody(string triggerTableName, string[] fieldNames)
//     {
//         var fields = String.Join(", ", fieldNames);
//         var values = String.Join(", ", fieldNames.Select(x => "new." + x));
//
//         return $"INSERT INTO {triggerTableName}({fields}) VALUES ({values});";
//     }
//
//     // DELETE FROM PostFts WHERE Id = old.Id;
//     private static string DeleteTriggerBody(string triggerTableName, string triggerTableId, string outerIdName)
//     {
//         return $"DELETE FROM {triggerTableName} WHERE {triggerTableId} = old.{outerIdName};";
//     }
//
//     public static string CreateTriggerWrap(
//         TriggerTypeEnum triggerTypeEnum,
//         string triggerTableName,
//         string watchTableName,
//         string triggerBody)
//     {
//         var operation = GetOperationName(triggerTypeEnum);
//         var triggerName = GetTriggerName(triggerTableName, triggerTypeEnum);
//         return $@"CREATE TRIGGER {triggerName} AFTER {operation.ToUpper()} ON {watchTableName} BEGIN
//     {triggerBody}
// END;";
//     }
//
//     public static string GetOperationName(TriggerTypeEnum triggerTypeEnum)
//     {
//         var operation = Enum.GetName(typeof(TriggerTypeEnum), triggerTypeEnum);
//         if (operation == null)
//         {
//             throw new ArgumentException("Operation name not recognized.");
//         }
//
//         return operation;
//     }
//     
//     public static string GetTriggerName(string triggerTableName, TriggerTypeEnum triggerTypeEnum)
//     {
//         var operation = GetOperationName(triggerTypeEnum);
//         return $"{triggerTableName}_after{operation}";
//     }


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