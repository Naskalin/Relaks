using Relaks.Models;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    private void SeedEntries()
    {
        foreach (Entry.TypeEnum entryType in (Entry.TypeEnum[]) Enum.GetValues(typeof(Entry.TypeEnum)))
        {
            for (int i = 0; i < 50; i++)
            {
                var entry = new Entry()
                {
                    Reputation = Faker.Random.Number(0, 10),
                    Type = entryType,
                    CreatedAt = Faker.Date.Past(),
                    UpdatedAt = Faker.Date.Past(),
                };
        
                var startAt = Faker.Date.Past(120, DateTime.UtcNow.AddYears(-10));
                var diffYears = (int) Math.Floor(Math.Abs((startAt - DateTime.UtcNow).TotalDays) / 365);
                if (diffYears < 1) diffYears = 1;
                var endAt = Faker.Date.Past(diffYears, DateTime.UtcNow);
        
                if (i % Faker.Random.Number(1, 5) == 0)
                {
                    entry.StartAt = startAt;
                }
        
                if (i % Faker.Random.Number(1, 5) == 0)
                {
                    entry.EndAt = endAt;
                    if (i % 2 == 0)
                    {
                        entry.StartAt = startAt;
                    }
                }
        
                if (Faker.Random.Number(1, 10) > 8)
                {
                    entry.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), null});
                    entry.DeletedAt = Faker.Date.Past();
                }

                switch (entryType)
                {
                    case Entry.TypeEnum.Person:
                        entry.Name = Faker.Name.FullName();
                        entry.Description = Faker.Random.ArrayElement(new[] {Faker.Name.JobDescriptor(), ""});
                        break;
                    case Entry.TypeEnum.Meet:
                        entry.Name = Faker.Commerce.Department();
                        entry.Description = Faker.Random.ArrayElement(new[] {Faker.Commerce.ProductDescription(), ""});
                        break;
                    case Entry.TypeEnum.Company:
                        entry.Name = Faker.Company.CompanyName();
                        entry.Description = Faker.Random.ArrayElement(new[] {Faker.Company.CatchPhrase(), ""});
                        break;
                }
                
                if (i == 1)
                {
                    entry.Name = "Hello world";
                }

                Db.Entries.Add(entry);
            }
        }

        var creator = new Entry()
        {
            Id = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4"),
            Name = "Вася Пупкин",
            Description = "Книги, Дом & Электроника Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles",
            Type = Entry.TypeEnum.Person,
            CreatedAt = Faker.Date.Past(),
            UpdatedAt = Faker.Date.Past(),
            StartAt = Faker.Date.Past(),
            EndAt = Faker.Date.Past(),
            DeletedReason = null,
        };

        Db.Entries.Add(creator);
        
        var creatorCompany = new Entry()
        {
            Id = Guid.Parse("01B137DA-A3CF-4C08-AC3E-752B3F156ED4"),
            Name = "Вася COMPANY",
            Description = Faker.Lorem.Paragraph(1),
            Type = Entry.TypeEnum.Company,
            CreatedAt = Faker.Date.Past(),
            UpdatedAt = Faker.Date.Past(),
            StartAt = Faker.Date.Past(),
            EndAt = Faker.Date.Past(),
            DeletedReason = null,
        };
        Db.Entries.Add(creatorCompany);

        Db.SaveChanges();
    }
}