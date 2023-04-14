using System.Text.Json.Nodes;

namespace Relaks.Interfaces;

public interface IInfoData
{
    public string Type { get; }
    public JsonObject Info { get; }
}