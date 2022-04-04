using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class EntryUrlSeeder : DatabaseSeeder
{
    public EntryUrlSeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        var entries = Db.Entries.Where(x => true).ToList();

        var i = 0;
        var random = new Random();
        foreach (var entry in entries)
        {
            if (i % random.Next(1, 5) == 0)
            {
                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    var eInfo = new EntryUrl()
                    {
                        Title = Faker.Random.ArrayElement(new []{Faker.Random.Words(), ""}),
                        Url = Faker.Internet.Url(),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        DeletedReason = ""
                    };
                    
                    if (j % Faker.Random.Number(1, 2) == 0)
                    {
                        eInfo.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), ""});
                        eInfo.DeletedAt = Faker.Date.Past();
                    }
                    
                    eInfo.EntryId = entry.Id;
                    Db.EntryUrls.Add(eInfo);
                }
            }

            i++;
        }

        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");

        var eDate = new EntryUrl()
        {
            Title = "Birthday of creator",
            EntryId = entryId,
            Id = Guid.Parse("07C18674-296E-41BE-9087-E92675007059"),
            Url = Faker.Internet.Url(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedReason = ""
        };

        Db.EntryUrls.Add(eDate);

        await Db.SaveChangesAsync();
    }
}