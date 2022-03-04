namespace App.Models;

public interface IGuidId
{
    public Guid Id { get; set; }
}

// public interface IEntry : IGuidId
// {
//     public string Name { get; set; }
//     public EntryTypeEnum EntryType { get; set; }
//     public int Reputation { get; set; }
//     public DateTime CreatedAt { get; set; }
// }