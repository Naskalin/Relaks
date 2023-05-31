using System.Text.Json;

namespace Relaks.Interfaces;

public interface IAppFile
{
    public string Filename { get; set; }
    public string MimeType { get; set; }
    public string DisplayName { get; set; }
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Путь к директории от webroot
    /// </summary>
    public string DirPath();
    
    /// <summary>
    /// Путь к файлу от webroot
    /// </summary>
    public string FilePath();
}

public interface IBaseFile : IAppFile, ISoftDeletedReason
{
    public Guid Id { get; set; }
    public string Discriminator { get; set; }
    public string? Keyword { get; set; }
    
    public Guid? CategoryId { get; set; }
}