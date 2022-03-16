using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class EntrySeeder : DatabaseSeeder
{
    public EntrySeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        
        var names = new[] {"Tom", "Bob", "Mike", "Frida"};
        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            var person = new Entry()
            {
                Name = names[random.Next(names.Length)],
                Reputation = random.Next(0, 10),
                EntryType = EntryTypeEnum.Person,
            };

            if (i % 2 == 0)
            {
                person.Description = "At vero eos et accusamus et iusto odio dignissimos ducimus";
            }

            Db.Entries.Add(person);
        }

        var creator = new Entry()
        {
            Id = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4"),
            Name = "Michelle",
            Description = "The creator of this",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ActualStartAt = DateTime.UtcNow,
        };
        
        Db.Entries.Add(creator);

        await Db.SaveChangesAsync();
    }
}