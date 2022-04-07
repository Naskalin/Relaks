using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class EntryEmailSeeder : DatabaseSeeder
{
    public EntryEmailSeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        var entries = Db.Entries.Where(x => true).ToList();

        var i = 0;
        var random = new Random();
        foreach (var entry in entries)
        {
            for (int j = 0; j < random.Next(1, 5); j++)
            {
                var eInfo = new EntryEmail()
                {
                    Title = Faker.Random.ArrayElement(new []{Faker.Random.Words(), ""}),
                    Email = Faker.Internet.Email(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedReason = ""
                };
                    
                if (Faker.Random.Number(1, 10) > 8)
                {
                    eInfo.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), ""});
                    eInfo.DeletedAt = Faker.Date.Past();
                }
                    
                eInfo.EntryId = entry.Id;
                Db.EntryEmails.Add(eInfo);
            }

            i++;
        }

        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");

        var eDate = new EntryEmail()
        {
            Title = "Birthday of creator",
            EntryId = entryId,
            Id = Guid.Parse("11434594-3FD0-46B5-94D3-B6C1DE188824"),
            Email = Faker.Internet.Email(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedReason = ""
        };

        Db.EntryEmails.Add(eDate);

        await Db.SaveChangesAsync();
    }
}