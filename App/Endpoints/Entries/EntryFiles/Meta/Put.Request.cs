using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles.Meta;

public class PutRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }

    [FromBody]
    public PutDetails Details { get; set; } = null!;
}
public enum FileMetaFieldsEnum
{
    Category,
    Tag
}
public class PutDetails
{
    public string Value { get; set; } = null!;
    public string NewValue { get; set; } = null!;
    public FileMetaFieldsEnum Field { get; set; }
}