using System.Text.Json;

namespace App.Utils.AppPreset;

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

public class AppPreset : AppPresetPublic
{
    public string SqliteConnection { get; set; } = null!;
    public string FilesDir { get; set; } = null!;
    public string CacheDir { get; set; } = null!;
}

public static class AppPresetManager
{
    public static void CreateDefaultPreset(string dataDir, string configPath)
    {
        if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

        var data = new AppPresetPublic()
        {
            Timezone = AppDefaultPreset.Timezone,
            PhoneRegion = AppDefaultPreset.PhoneRegion
        };
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(configPath, json);
    }

    public static AppPreset GetPreset(string dataDir)
    {
        var configPath = Path.Combine(dataDir, AppDefaultPreset.PresetName);

        if (!File.Exists(configPath)) CreateDefaultPreset(dataDir, configPath);

        using var r = new StreamReader(configPath);
        var json = r.ReadToEnd();
        var appPresetModel = JsonSerializer.Deserialize<AppPreset>(json);

        if (appPresetModel == null) throw new ArgumentException("AppPreset, файл конфигурации не распознан.");

        var dbPath = Path.Combine(dataDir, "db.sqlite");
        appPresetModel.SqliteConnection = "Data Source=" + dbPath;

        var filesDir = Path.Combine(dataDir, "files");
        if (!Directory.Exists(filesDir)) Directory.CreateDirectory(filesDir);
        appPresetModel.FilesDir = filesDir;

        var cacheDir = Path.Combine(dataDir, "cache");
        if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
        appPresetModel.CacheDir = cacheDir;

        return appPresetModel;
    }
}