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