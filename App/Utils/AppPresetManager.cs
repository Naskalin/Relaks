using System.Text.Json;

namespace App.Utils;

public class AppDefaultPreset
{
    public const string PresetName = "app_preset.json";
    public const string DirName = "Data";
    public const string Timezone = "Europe/Moscow";
    public const string PhoneRegion = "RU";
}

public class AppPresetWriteModel
{
    public string DataDir { get; set; } = null!;
    public string Timezone { get; set; } = null!;
    public string PhoneRegion { get; set; } = null!;
}

public class AppPreset : AppPresetWriteModel
{
    public string SqliteConnection { get; set; } = null!;
    public string FilesDir { get; set; } = null!;
    public string CacheDir { get; set; } = null!;
}

public class AppPresetManager
{
    private readonly string _projectDir;

    public AppPresetManager(string projectDir)
    {
        _projectDir = projectDir;
    }

    public void CreateDefaultPreset(string dirPath, string configPath)
    {
        if (File.Exists(configPath)) return;
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        var data = new AppPresetWriteModel()
        {
            DataDir = dirPath,
            Timezone = AppDefaultPreset.Timezone,
            PhoneRegion = AppDefaultPreset.PhoneRegion
        };
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(configPath, json);
    }

    public AppPreset GetPreset()
    {
        var dirPath = Path.Combine(_projectDir, "..", AppDefaultPreset.DirName);
        var configPath = Path.Combine(dirPath, AppDefaultPreset.PresetName);

        CreateDefaultPreset(dirPath, configPath);

        if (!File.Exists(configPath))
        {
            throw new ArgumentException("AppPreset, файл конфигурации не найден.");
        }

        using var r = new StreamReader(configPath);
        var json = r.ReadToEnd();
        var appPresetModel = JsonSerializer.Deserialize<AppPreset>(json);

        if (appPresetModel == null)
        {
            throw new ArgumentException("AppPreset, файл конфигурации не распознан.");
        }

        var dbPath = Path.Combine(appPresetModel.DataDir, "db.sqlite");
        appPresetModel.SqliteConnection = "Data Source=" + dbPath;

        var filesDir = Path.Combine(appPresetModel.DataDir, "files");
        if (!Directory.Exists(filesDir)) Directory.CreateDirectory(filesDir);
        appPresetModel.FilesDir = filesDir;

        var cacheDir = Path.Combine(appPresetModel.DataDir, "cache");
        if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
        appPresetModel.CacheDir = cacheDir;

        return appPresetModel;
    }
}