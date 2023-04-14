// using System.ComponentModel.DataAnnotations;
// using System.Text.Json;
// using System.Text.Json.Nodes;
// using App.Interfaces;
//
// namespace App.Models;
//
// public class AppFile : IAppFile, IDisposable
// {
//     public Guid Id { get; set; }
//
//     [StringLength(50)]
//     public string PathName { get; set; } = null!;
//
//     public string? DisplayName { get; set; }
//
//     [StringLength(50)]
//     public string Directory { get; set; } = null!;
//
//     [StringLength(255)]
//     public string MimeType { get; set; } = null!;
//
//     public DateTime CreatedAt { get; set; }
//     public bool IsPrivate { get; set; }
//
//     [StringLength(50)]
//     public string Keyword { get; set; } = null!;
//
//     public AppFile()
//     {
//         Id = Guid.NewGuid();
//         CreatedAt = DateTime.UtcNow;
//         IsPrivate = false;
//     }
//
//     public string NameWithExtension()
//     {
//         var n = DisplayName ?? Id.ToString();
//         return n + Path.GetExtension(PathName);
//     }
//
//     public bool IsTemp() => Directory.Equals(AppFileDirs.Temp);
//     public bool IsImage() => MimeType.StartsWith("image");
//
//     /// <summary>
//     /// Путь к директории от webroot
//     /// </summary>
//     /// <returns></returns>
//     public string DirPath()
//     {
//         var paths = new List<string>() {"files"};
//         if (!IsTemp()) paths.Add(UserId.ToString());
//         paths.Add(Directory);
//         paths.Add(CreatedAt.Year.ToString());
//         paths.Add(CreatedAt.Month.ToString());
//         paths.Add(CreatedAt.Day.ToString());
//         return Path.Combine(paths.ToArray());
//     }
//
//     /// <summary>
//     /// Путь к файлу от webroot
//     /// </summary>
//     /// <returns></returns>
//     public string FilePath() => Path.Combine(DirPath(), PathName);
//
//     public void Dispose() => MetaJson?.Dispose();
// }
//
// public class AppFileDto
// {
//     public Guid Id { get; set; }
//     public string? DisplayName { get; set; }
//     public bool IsPrivate { get; set; }
//     public bool IsTemp { get; set; }
//     public string? Path { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public Dictionary<string, string> SrcSet { get; set; } = new();
// }
//
// public class AppImageDto
// {
//     public Guid Id { get; set; }
//     public Dictionary<string, string> SrcSet { get; set; } = new();
// }