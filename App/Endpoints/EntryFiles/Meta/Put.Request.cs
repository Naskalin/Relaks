namespace App.Endpoints.EntryFiles.Meta;

public class PutMetaRequest
{
    public Guid EntryId { get; set; }
    
    public string Value { get; set; } = null!;
    public string NewValue { get; set; } = null!;
    public FileMetaFieldsEnum Field { get; set; }
}

public enum FileMetaFieldsEnum
{
    Category,
    Tag
}