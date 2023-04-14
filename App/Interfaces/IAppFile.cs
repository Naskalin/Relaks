using System.Text.Json;

namespace App.Interfaces;

public interface IAppFile
{
    public Guid Id { get; set; }
    public string PathName { get; set; }
    public string? DisplayName { get; set; }
    public string Directory { get; set; }
    public string MimeType { get; set; }
    public JsonDocument? MetaJson { get; set; }
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл размещён в директории, вне wwwroot, он скрыт для индексации
    /// </summary>
    public bool IsPrivate { get; set; }

    public bool IsTemp();
    /// <summary>
    /// Путь к директории от webroot
    /// </summary>
    public string DirPath();
    
    /// <summary>
    /// Путь к файлу от webroot
    /// </summary>
    public string FilePath();
}