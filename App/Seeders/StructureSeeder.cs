using System.Text.Json.Nodes;
using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class StructureSeeder : DatabaseSeeder
{
    public StructureSeeder(AppDbContext db) : base(db)
    {
    }

    public void Seed()
    {
        var companyId = Guid.Parse("01B137DA-A3CF-4C08-AC3E-752B3F156ED4");

        List<Structure> parentStructures = new();
        for (int i = 0; i < Faker.Random.Int(1, 3); i++)
        {
            var structure = new Structure()
            {
                EntryId = companyId,
                Description = Faker.Random.ArrayElement(new[] {"", Faker.Lorem.Paragraph(1)}),
                StartAt = Faker.Date.Past(),
                Title = Faker.Random.Words(Faker.Random.Int(1, 5)),
                DeletedReason = "",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            parentStructures.Add(structure);
            Db.Structures.Add(structure);
        }

        foreach (var parent in parentStructures)
        {
            AddChild(parent, companyId, Faker.Random.Int(2, 5));
        }
        
        Db.SaveChanges();
        
        // Добавляем items
        var entries = Db.Entries.Where(x => x.EntryType == EntryTypeEnum.Person).ToList();
        var structures = Db.Structures.ToList();

        foreach (var structure in structures)
        {
            AddItems(structure, entries);

            if (Faker.Random.Int(1, 2).Equals(1))
            {
                for (int i = 0; i < Faker.Random.Int(1, 3); i++)
                {
                    // add connection
                    var connection = new StructureConnection()
                    {
                        Description = Faker.Random.ArrayElement(new[] {"", Faker.Lorem.Paragraph(1)}),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        StructureFirstId = structure.Id,
                        StructureSecondId = structures.OrderBy(x => Guid.NewGuid()).First(x => !x.Id.Equals(structure.Id)).Id,
                        Direction = Faker.Random.Enum<StructureConnection.DirectionEnum>(),
                        StartAt = Faker.Date.Past(),
                        DeletedReason = ""
                    };

                    Db.StructureConnections.Add(connection);   
                }
            }
        }
        
        Db.SaveChanges();
    }

    private void AddChild(Structure parent, Guid companyId, int count, int depth = 0)
    {
        for (int i = 0; i < count; i++)
        {
            var child = new Structure()
            {
                ParentId = parent.Id,
                EntryId = companyId,
                Description = Faker.Random.ArrayElement(new[] {"", Faker.Lorem.Paragraph(1)}),
                StartAt = Faker.Date.Past(),
                Title = Faker.Random.Words(Faker.Random.Int(1, 5)),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedReason = ""
            };
            Db.Structures.Add(child);

            if (depth <= 5 && Faker.Random.Int(1, 2).Equals(1))
            {
                AddChild(child, companyId, Faker.Random.Int(1, 3), depth + 1);
            }
        }
    }

    private void AddItems(Structure structure, List<Entry> entries)
    {
        // if (10 >= Faker.Random.Int(1, 10)) return;
        var randomEntries = entries.OrderBy(x => Guid.NewGuid()).Take(Faker.Random.Int(1, entries.Count / 2)).ToList();
        foreach (var entry in randomEntries)
        {
            var item = new StructureItem()
            {
                Description = Faker.Random.ArrayElement(new []{Faker.Random.Words(Faker.Random.Int(2, 5)), ""}),
                StartAt = Faker.Date.Past(),
                EntryId = entry.Id,
                StructureId = structure.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedReason = ""
            };
            Db.StructureItems.Add(item);
        }
    }
}