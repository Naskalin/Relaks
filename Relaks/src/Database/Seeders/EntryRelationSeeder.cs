using Relaks.Models;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    private void SeedEntryRelations()
    {
        var entries = Db.BaseEntries.Where(x => true)
            .Where(x => !x.Id.Equals(Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4")))
            .Take(6)
            .ToList();

        var vasya = Db.BaseEntries.First(x => x.Id.Equals(Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4")));
        var relations = new List<EntryRelation>();
        
        foreach (var entry in entries)
        {
            var desc = Faker.Random.Number(1, 3) <= 2 ? Faker.Lorem.Paragraphs(Faker.Random.Number(1, 5)) : null;
            relations.Add(new EntryRelation()
            {
                First = vasya,
                Second = entry,
                FirstRating = Faker.Random.Number(1, 10),
                SecondRating = Faker.Random.Number(1, 10),
                Description = desc
            });    
        }
        
        Db.EntryRelations.AddRange(relations);
        Db.SaveChanges();
    }
}