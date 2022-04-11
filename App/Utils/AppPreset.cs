using System.Text.Json;

namespace App.Utils;

public class AppDefaultPreset
{
    public const string DirName = "Data";
    public const string ConfigName = "config.json";
    public const string Timezone = "Europe/Moscow";
    public const string PhoneRegion = "RU";
}

public class AppPresetWriteModel
{
    public string DataDir { get; set; } = null!;
    public string Timezone { get; set; } = null!;
    public string PhoneRegion { get; set; } = null!;
}

public class AppPresetModel : AppPresetWriteModel
{
    public string SqliteConnection { get; set; } = null!;
    public string FilesDir { get; set; } = null!;
}

public class AppPreset
{
    private readonly string _projectDir;

    public AppPreset(string projectDir)
    {
        _projectDir = projectDir;
    }

    public void CreateDefaultPreset(string dirPath, string configPath)
    {
        if (!File.Exists(configPath))
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var data = new AppPresetWriteModel()
            {
                DataDir = dirPath,
                Timezone = AppDefaultPreset.Timezone,
                PhoneRegion = AppDefaultPreset.PhoneRegion
            };
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(configPath, json);
        }
    }
    
    // public Dictionary<> GetDefaultPreset()

    public AppPresetModel GetPreset()
    {
        var dirPath = Path.Combine(_projectDir, "../" + AppDefaultPreset.DirName);
        var configPath = Path.Combine(dirPath, AppDefaultPreset.ConfigName);
        
        CreateDefaultPreset(dirPath, configPath);

        if (!File.Exists(configPath))
        {
            throw new ArgumentException("AppPreset, файл конфигурации не найден.");
        }
        
        using (StreamReader r = new StreamReader(configPath))
        {
            string json = r.ReadToEnd();
            var appPresetModel = JsonSerializer.Deserialize<AppPresetModel>(json);

            if (appPresetModel == null)
            {
                throw new ArgumentException("AppPreset, файл конфигурации не распознан.");
            }

            appPresetModel.SqliteConnection = "Data Source=" + appPresetModel.DataDir + "\\app.db";
            appPresetModel.FilesDir = Path.Combine(appPresetModel.DataDir, "files");
            
            return appPresetModel;
        }

    }
}