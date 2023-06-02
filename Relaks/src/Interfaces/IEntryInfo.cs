using Relaks.Models;

namespace Relaks.Interfaces;

public interface IEntryInfo
{
    public string? Title { get; set; }
    public bool IsFavorite { get; set; }
}

public interface IPhone
{
    public string Number { get; set; }
    public string Region { get; set; }
}

public interface IEmail
{
    public string Email { get; set; }
}

public interface IDate
{
    public DateTime Date { get; set; }
    public bool WithTime { get; set; }
}

public interface IUrl
{
    public string Url { get; set; }
}

public interface IDataset
{
    public DatasetModel Dataset { get; set; }
}