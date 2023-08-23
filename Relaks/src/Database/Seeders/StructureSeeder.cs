using System.Text.Json;
using Relaks.Managers;
using Relaks.Models;
using Relaks.Models.StructureModels;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    public void SeedStructures()
    {
        var companyId = Guid.Parse("01B137DA-A3CF-4C08-AC3E-752B3F156ED4");
    
        List<StructureGroup> parentGroups = new();
        for (int i = 0; i < Faker.Random.Int(1, 3); i++)
        {
            var structureGroup = new StructureGroup()
            {
                EntryId = companyId,
                Description = Faker.Random.ArrayElement(new[] {null, Faker.Lorem.Paragraph(1)}),
                StartAt = Faker.Date.Past(10),
                Title = Faker.Random.Words(Faker.Random.Int(1, 3)),
            };

            if (Faker.Random.Int(1, 3).Equals(1))
            {
                structureGroup.EndAt = Faker.Date.Past();
            }
            
            TreeManager.UpdateTreePath(structureGroup);
            
            parentGroups.Add(structureGroup);
            
            Db.StructureGroups.Add(structureGroup);
        }
    
        foreach (var structureGroup in parentGroups)
        {
            AddChild(structureGroup, companyId, Faker.Random.Int(1, 3));
        }
        
        Db.SaveChanges();
        
        // Добавляем items
        var entries = Db.BaseEntries.ToList();
        var structureGroups = Db.StructureGroups.ToList();
    
        foreach (var structureGroup in structureGroups)
        {
            AddItems(structureGroup, entries);
    
            // if (Faker.Random.Int(1, 2).Equals(1))
            // {
            //     for (int i = 0; i < Faker.Random.Int(1, 3); i++)
            //     {
            //         // add connection
            //         var connection = new StructureConnection()
            //         {
            //             Description = Faker.Random.ArrayElement(new[] {"", Faker.Lorem.Paragraph(1)}),
            //             CreatedAt = DateTime.UtcNow,
            //             UpdatedAt = DateTime.UtcNow,
            //             StructureFirstId = structureGroup.Id,
            //             StructureSecondId = structureGroups.OrderBy(x => Guid.NewGuid()).First(x => !x.Id.Equals(structureGroup.Id)).Id,
            //             Direction = Faker.Random.Enum<StructureConnection.DirectionEnum>(),
            //             StartAt = Faker.Date.Past(),
            //             DeletedReason = ""
            //         };
            //
            //         Db.StructureConnections.Add(connection);   
            //     }
            // }
        }
        
        Db.SaveChanges();
    }
    
    private void AddChild(StructureGroup parent, Guid companyId, int count, int depth = 0)
    {
        for (int i = 0; i < count; i++)
        {
            var child = new StructureGroup()
            {
                Parent = parent,
                EntryId = companyId,
                Description = Faker.Random.ArrayElement(new[] {null, Faker.Lorem.Paragraph(1)}),
                StartAt = Faker.Date.Past(10),
                Title = Faker.Random.Words(Faker.Random.Int(1, 3)),
            };
            
            if (Faker.Random.Int(1, 3).Equals(1))
            {
                child.EndAt = Faker.Date.Past();
            }
            
            TreeManager.UpdateTreePath(child, parent);
            Db.StructureGroups.Add(child);
    
            if (depth <= 5 && Faker.Random.Int(1, 2).Equals(1))
            {
                AddChild(child, companyId, Faker.Random.Int(1, 3), depth + 1);
            }
        }
    }
    
    private void AddItems(StructureGroup structureGroup, List<BaseEntry> entries)
    {
        var randomEntries = entries.OrderBy(x => Guid.NewGuid()).Take(Faker.Random.Int(1, 5)).ToList();
        foreach (var baseEntry in randomEntries)
        {
            var item = new StructureItem()
            {
                Description = Faker.Random.ArrayElement(new []{Faker.Lorem.Paragraph(1), null}),
                Title = Faker.Random.ArrayElement(new []{Faker.Random.Words(Faker.Random.Int(2, 5)), null}),
                StartAt = Faker.Date.Past(Faker.Random.Int(5, 10)),
                EntryId = baseEntry.Id,
                GroupId = structureGroup.Id,
            };
            
            if (Faker.Random.Int(1, 3).Equals(1))
            {
                item.EndAt = Faker.Date.Past();
            }
            
            Db.StructureItems.Add(item);
        }
    }
}