namespace App.Models;

// public class EntryText : BaseEntity, ITimestampResource, ISoftDelete
// {
//     public Entry Entry { get; set; } = null!;
//     public Guid EntryId { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public DateTime UpdatedAt { get; set; }
//     public string Title { get; set; } = null!;
//     public string Val { get; set; } = null!;
//     public TextTypeEnum TextType { get; set; }
//     
//     public DateTime? DeletedAt { get; set; }
//     public string DeletedReason { get; set; } = null!;
//
//     public List<EntryTextFile> Files { get; set; } = new();
// }
//

public abstract class EntryInfo : BaseEntity, ITimestampResource, ISoftDelete
{
    public const string PhoneType = "Phone";
    public const string EmailType = "Email";
    public const string UrlType = "Url";
    public const string NoteType = "Note";
    public const string DateType = "Date";
        
    public Guid EntryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; } = null!;
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;

    public List<EntryInfoFile> Files { get; set; } = new();

    public string Discriminator { get; set; } = null!;
}

public class EntryInfoFts : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public double? Rank { get; set; }
    
    public Guid Id { get; set; }
    public Guid EntryId { get; set; }
    
    public string Title { get; set; } = null!;
    public string DeletedReason { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Url { get; set; }
    public string? Note { get; set; }
}

public class EntryNote : EntryInfo
{
    public string Note { get; set; } = null!;
}

public class EntryPhone : EntryInfo
{
    public string PhoneNumber { get; set; } = null!;
    public string PhoneRegion { get; set; } = null!;
}

public class EntryEmail : EntryInfo
{
    public string Email { get; set; } = null!;
}

public class EntryUrl : EntryInfo
{
    public string Url { get; set; } = null!;
}

public class EntryDate : EntryInfo
{
    public DateTime Date { get; set; }
}