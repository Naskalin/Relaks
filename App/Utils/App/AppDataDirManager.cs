using System.Text.Json;

namespace App.Utils.App;

public class AppConfigDirModel
{
    public string DirPath { get; set; } = null!;
}

public static class AppDataDirManager
{
    public static string ConfigName = "dir_config.json";

    public static void UpdateConfigDir(string projectDir, string dirPath)
    {
        var model = new AppConfigDirModel {DirPath = dirPath};
        var json = JsonSerializer.Serialize(model);
        var path = Path.Combine(projectDir, ConfigName);

        File.WriteAllText(path, json);
    }

    // public static void RemoveConfigDir(string projectDir)
    // {
    //     var path = Path.Combine(projectDir, ConfigName);
    //     if (!File.Exists(path)) return;
    //     File.Delete(path);
    // }

    public static string? GetDirPath(string projectDir)
    {
        var path = Path.Combine(projectDir, ConfigName);
        // файл не найден
        if (!File.Exists(path)) return null;
        
        using var r = new StreamReader(path);
        var json = r.ReadToEnd();
        var dirModel = JsonSerializer.Deserialize<AppConfigDirModel>(json);
        // Не смог десериализоваться правильно
        // Или путь не задан
        if (
            dirModel == null 
            || String.IsNullOrEmpty(dirModel.DirPath)
            || !Directory.Exists(dirModel.DirPath)
            ) return null;
        
        return dirModel.DirPath;
    }
}