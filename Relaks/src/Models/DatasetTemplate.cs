using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Relaks.Interfaces;

namespace Relaks.Models;

public class DatasetTemplate : IDataset
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public DatasetModel Dataset
    {
        get => JsonSerializer.Deserialize<DatasetModel>(DatasetValue) ?? new DatasetModel();
        set => DatasetValue = JsonSerializer.Serialize(value);
    }
    
    [NotMapped]
    public string DatasetValue { get; set; } = null!;
}

public class DatasetTemplateRequest : IDataset
{
    public string Title { get; set; } = null!;
    public DatasetModel Dataset { get; set; } = null!;
}