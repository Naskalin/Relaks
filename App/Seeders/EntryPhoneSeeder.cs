using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class EntryPhoneSeeder : DatabaseSeeder
{
    public EntryPhoneSeeder(AppDbContext db) : base(db)
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
                    var eInfo = new EntryPhone()
                    {
                        Title = Faker.Random.ArrayElement(new []{Faker.Random.Words(), ""}),
                        PhoneNumber = "+7812000000" + i,
                        PhoneRegion = "RU",
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
                    Db.EntryPhones.Add(eInfo);
                }
            }

            i++;
        }

        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");

        var eDate = new EntryPhone()
        {
            Title = "Birthday of creator",
            EntryId = entryId,
            Id = Guid.Parse("07315DD9-8587-4B73-A53A-0C399968647E"),
            PhoneNumber = "+78121234567",
            PhoneRegion = "RU",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedReason = ""
        };

        Db.EntryPhones.Add(eDate);

        await Db.SaveChangesAsync();
    }
}