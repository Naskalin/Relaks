using System.ComponentModel.DataAnnotations;

namespace App.Models;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}