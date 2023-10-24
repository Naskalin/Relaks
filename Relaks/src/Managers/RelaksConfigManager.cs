using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Relaks.Managers;

public class RelaksConfig
{
    public bool IsFirstRun { get; set; }
    public string ProjectDir { get; set; } = null!;
    public string StoreDirPath { get; set; } = null!;

    public bool IsValid() => Directory.Exists(StoreDirPath);

    public string FilesDirPath() => Path.Combine(StoreDirPath, "files");
    public string SqliteFilePath() => Path.Combine(StoreDirPath, "relaks.db");
    public string SqliteConnectionString() => "Data Source=" + SqliteFilePath();
}

public static class RelaksConfigManager
{
    private const string RelaksConfigName = "relaks.yaml";
    private const string StoreDirName = "relaks_store";

    private static string RelaksConfigPath(string projectDir) => Path.Combine(projectDir, RelaksConfigName);
    private static bool HasConfigFile(string projectDir) => File.Exists(RelaksConfigPath(projectDir));

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
    private static RelaksConfig CreateDefaultConfig(string projectDir)
    {
        var storeDir = DefaultStorePath(projectDir);
        
        var model = new RelaksConfig()
        {
            IsFirstRun = true,
            ProjectDir = projectDir,
            StoreDirPath = storeDir,
        };
        SaveConfigFile(model);

        return model;
    }

    public static void SaveConfigFile(RelaksConfig config)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var yaml = serializer.Serialize(config);
        File.WriteAllText(RelaksConfigPath(config.ProjectDir), yaml);

        Directory.CreateDirectory(config.FilesDirPath());
        Directory.CreateDirectory(Path.GetDirectoryName(config.SqliteFilePath())!);
    }

    /// <summary>
    /// Получаем существующий или создаём новый конфиг
    /// </summary>
    /// <param name="projectDir"></param>
    /// <returns></returns>
    public static RelaksConfig GetOrCreateConfig(string projectDir)
    {
        var relaksConfigPath = RelaksConfigPath(projectDir);
        RelaksConfig config;
        
        if (!HasConfigFile(projectDir))
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
        
            config = deserializer.Deserialize<RelaksConfig>(yamlString);
            if (!config.IsValid())
            {
                config = CreateDefaultConfig(projectDir);
            }
        }

        return config;
    }
}