using System.Text.Json;

namespace Relaks.Interfaces;

public interface IAppFile
{
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public string DisplayName { get; set; }
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Имя файла
    /// </summary>
    /// <returns></returns>
    public string FileName();
    
    /// <summary>
    /// Путь к директории от webroot
    /// </summary>
    public string DirPath();
    
    /// <summary>
    /// Путь к файлу от webroot
    /// </summary>
    public string FilePath();
}