using System.ComponentModel.DataAnnotations;

namespace Relaks.Models;

public class Country
{
    public Guid Id { get; set; }

    [StringLength(2)]
    public string Alpha2 { get; set; } = null!;

    [StringLength(255)]
    public string TitleRu { get; set; } = null!;

    [StringLength(255)]
    public string TitleEn { get; set; } = null!;

    [StringLength(255)]
    public string Native { get; set; } = null!;

    [StringLength(100)]
    public string Phone { get; set; } = null!;

    [StringLength(255)]
    public string Continent { get; set; } = null!;

    [StringLength(255)]
    public string Capital { get; set; } = null!;

    [StringLength(255)]
    public string Currency { get; set; } = null!;

    public Country()
    {
        Id = Guid.NewGuid();
    }
}