using System.Text.Json;

namespace App.Utils.AppPreset;

public class AppDataDirModel
{
    public string DirPath { get; set; } = null!;
}

public static class AppDataDirManager
{
    public static string ConfigName = "dir_config.json";

    public static void SaveDataDir(string projectDir, string dirPath)
    {
        var model = new AppDataDirModel {DirPath = dirPath};
        var json = JsonSerializer.Serialize(model);
        var path = Path.Combine(projectDir, ConfigName);

        File.WriteAllText(path, json);
    }

    public static AppDataDirModel? GetDataDir(string projectDir)
    {
        var path = Path.Combine(projectDir, ConfigName);
        // файл не найден
        if (!File.Exists(path)) return null;
        
        using var r = new StreamReader(path);
        var json = r.ReadToEnd();
        var dirModel = JsonSerializer.Deserialize<AppDataDirModel>(json);
        // Не смог десериализоваться правильно
        // Или путь не задан
        if (dirModel == null || String.IsNullOrEmpty(dirModel.DirPath)) return null;
        
        return dirModel;
    }
}