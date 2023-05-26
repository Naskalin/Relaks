using Relaks.Interfaces;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Relaks.Managers;

public class RelaksConfigModel : IRelaksConfig
{
    public string ProjectDir { get; set; } = null!;
    public string SqliteFilePath { get; set; } = null!;
    public string FilesDirPath { get; set; } = null!;
    public string Timezone { get; set; } = null!;
    public string PhoneRegion { get; set; } = null!;

    public string SqliteConnectionString() => "Data Source=" + SqliteFilePath;
}

public static class RelaksConfigManager
{
    private const string RelaksConfigName = "relaks.yaml";
    private const string StoreDirName = "relaks_store";

    private static string RelaksConfigPath(string projectDir) => Path.Combine(projectDir, RelaksConfigName);

    /// <summary>
    /// Parent of projectDir
    /// </summary>
    /// <param name="projectDir"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static string DefaultStorePath(string projectDir)
    {
        var parentDir = Directory.GetParent(projectDir)?.ToString();
        if (string.IsNullOrEmpty(parentDir)) throw new ArgumentException("[parentDir] is null");

        return Path.Combine(parentDir, StoreDirName);
    } 

    /// <summary>
    /// Создаём конфигурацию по умолчанию
    /// </summary>
    /// <param name="projectDir"></param>
    /// <returns></returns>
    private static RelaksConfigModel CreateDefaultConfig(string projectDir)
    {
        var storeDir = DefaultStorePath(projectDir);
        
        var model = new RelaksConfigModel()
        {
            ProjectDir = projectDir,
            Timezone = "Europe/Moscow",
            PhoneRegion = "RU",
            FilesDirPath = Path.Combine(storeDir, "files"),
            SqliteFilePath = Path.Combine(storeDir, "relaks.db"),
        };
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var yaml = serializer.Serialize(model);
        File.WriteAllText(RelaksConfigPath(projectDir), yaml);

        Directory.CreateDirectory(model.FilesDirPath);
        Directory.CreateDirectory(Path.GetDirectoryName(model.SqliteFilePath)!);

        return model;
    }

    private static void CreateSymlinkToFilesDir(RelaksConfigModel config)
    {
        var webrootPath = Path.Combine(config.ProjectDir, "wwwroot");
        if (!Directory.Exists(webrootPath))
        {
            Directory.CreateDirectory(webrootPath);
        }

        Directory.CreateSymbolicLink(Path.Combine(webrootPath, "files"), config.FilesDirPath);
    }
    
    /// <summary>
    /// Получаем существующий или создаём новый конфиг
    /// </summary>
    /// <param name="projectDir"></param>
    /// <returns></returns>
    public static RelaksConfigModel GetOrCreateConfig(string projectDir)
    {
        var relaksConfigPath = RelaksConfigPath(projectDir);
        RelaksConfigModel config;
        
        if (!File.Exists(relaksConfigPath))
        {
            config = CreateDefaultConfig(projectDir);
        }
        else
        {
            using var reader = new StreamReader(relaksConfigPath);
            var yamlString = reader.ReadToEnd();
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        
            config = deserializer.Deserialize<RelaksConfigModel>(yamlString);
            if (string.IsNullOrEmpty(config.SqliteFilePath) || string.IsNullOrEmpty(config.FilesDirPath))
            {
                config = CreateDefaultConfig(projectDir);
            }   
        }

        // CreateSymlinkToFilesDir(config);

        return config;
    }
}