using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Utils.App;

public class AppDefaultPreset
{
    public const string PresetName = "app_preset.json";
    public const string Timezone = "Europe/Moscow";
    public const string PhoneRegion = "RU";
}

public class AppPresetPublic
{
    public string Timezone { get; set; } = null!;
    public string PhoneRegion { get; set; } = null!;
}

public class AppPresetFull : AppPresetPublic
{
    [JsonIgnore]
    public string SqliteConnection { get; set; } = null!;
    
    [JsonIgnore]
    public string FilesDir { get; set; } = null!;
    
    [JsonIgnore]
    public string CacheDir { get; set; } = null!;
}

public static class AppPresetManager
{
    public static void CreateDefaultPreset(string dataDir, string configPath)
    {
        if (!Directory.Exists(dataDir)) throw new ArgumentException($"Directory not exists: {dataDir}");

        var data = new AppPresetPublic()
        {
            Timezone = AppDefaultPreset.Timezone,
            PhoneRegion = AppDefaultPreset.PhoneRegion
        };
        
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(configPath, json);
    }

    public static AppPresetFull? GetPreset(string projectDir)
    {
        var dirPath = AppDataDirManager.GetDirPath(projectDir);
        if (!Directory.Exists(dirPath)) return null;
        
        var configPath = Path.Combine(dirPath, AppDefaultPreset.PresetName);
        if (!File.Exists(configPath)) CreateDefaultPreset(dirPath, configPath);

        using var r = new StreamReader(configPath);
        var json = r.ReadToEnd();
        var appPresetModel = JsonSerializer.Deserialize<AppPresetFull>(json);

        if (appPresetModel == null) throw new ArgumentException("AppPreset, файл конфигурации не распознан.");

        var dbPath = Path.Combine(dirPath, "db.sqlite");
        appPresetModel.SqliteConnection = "Data Source=" + dbPath;

        var filesDir = Path.Combine(dirPath, "files");
        if (!Directory.Exists(filesDir)) Directory.CreateDirectory(filesDir);
        appPresetModel.FilesDir = filesDir;

        var cacheDir = Path.Combine(dirPath, "cache");
        if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
        appPresetModel.CacheDir = cacheDir;

        return appPresetModel;
    }
}