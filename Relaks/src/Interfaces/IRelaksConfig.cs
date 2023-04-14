namespace Relaks.Interfaces;

public interface IRelaksConfig
{
    public string SqliteFilePath { get; set; }
    public string FilesDirPath { get; set; }
    public string Timezone { get; set; }
    public string PhoneRegion { get; set; }
}