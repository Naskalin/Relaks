using App.Endpoints;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class ListRequest : BaseListRequest
{
    public EntryTypeEnum? EntryType { get; set; }

    // public EntryDateFieldEnum DateField;
}

// public enum EntryDateFieldEnum
// {
//     StartAt,
//     EndAt,
//     CreatedAt,
//     UpdatedAt,
//     ActualStartAt,
//     ActualEndAt,
// }