using App.Models.Entry;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

public class Note : Identifiable<Guid>
{
    public BaseEntry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }
    
    public string? Title { get; set; }
    public string Description { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

// public record NoteCreateDto
// {
//     public string? Title { get; set; }
//     public string Description { get; set; } = null!;
//     [Required]
//     public Guid EntryId { get; set; }
// }
//
// public record NotePatchDto
// {
//     [JsonConverter(typeof(OptionalConverterFactory))]
//     [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
//     public Optional<string?> Title { get; set; }
//     
//     [JsonConverter(typeof(OptionalConverterFactory))]
//     [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
//     public Optional<string> Description { get; set; }
//     
//     [JsonConverter(typeof(OptionalConverterFactory))]
//     [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
//     public Optional<Guid> EntryId { get; set; }
// }
//
