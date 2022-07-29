﻿using App.Models;

namespace App.Endpoints.Entries;

public class EntryFormRequest : ISoftDelete
{
    public string Name { get; set; } = null!;
    public string EntryType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public Guid? Avatar { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class EntryCreateRequest : EntryFormRequest
{
}

public class EntryPutRequest : EntryFormRequest
{
    public Guid EntryId { get; set; }
}

public class EntryGetRequest
{
    public Guid EntryId { get; set; }
}

public class EntryListRequest : BaseListRequest
{
    public EntryTypeEnum? EntryType { get; set; }
}