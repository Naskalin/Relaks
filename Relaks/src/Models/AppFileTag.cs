﻿using System.ComponentModel.DataAnnotations.Schema;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("FileTags")]
public abstract class BaseFileTag : ITag
{
    public Guid Id { get; set; }
    public string Discriminator { get; set; } = null!;
    public string Title { get; set; } = null!;
    public List<BaseFile> Files { get; set; } = new();

    protected BaseFileTag()
    {
        Id = Guid.NewGuid();
    }
}

public class EntryFileTag : BaseFileTag
{
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
}