namespace App.Endpoints.Entries.EntryFiles.Meta;

public class GetMetaResult
{
    public List<string> Categories { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}