using System.ComponentModel.DataAnnotations;

namespace App.Models;

public abstract class BaseEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
}