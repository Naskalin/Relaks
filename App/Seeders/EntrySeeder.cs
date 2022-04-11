﻿using App.DbConfigurations;
using App.Models;
using Bogus;

namespace App.Seeders;

public class EntrySeeder : DatabaseSeeder
{
    public EntrySeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {

        foreach (EntryTypeEnum entryType in (EntryTypeEnum[]) Enum.GetValues(typeof(EntryTypeEnum)))
        {
            for (int i = 0; i < 50; i++)
            {
                var entry = new Entry()
                {
                    Reputation = Faker.Random.Number(0, 10),
                    EntryType = entryType,
                    CreatedAt = Faker.Date.Past(),
                    UpdatedAt = Faker.Date.Past(),
                    DeletedReason = ""
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
                    entry.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), ""});
                    entry.DeletedAt = Faker.Date.Past();
                }
        
                switch (entryType)
                {
                    case EntryTypeEnum.Person:
                        entry.Name = Faker.Name.FullName();
                        entry.Description = Faker.Random.ArrayElement(new[] {Faker.Name.JobDescriptor(), ""});
                        break;
                    case EntryTypeEnum.Meet:
                        entry.Name = Faker.Commerce.Department();
                        entry.Description = Faker.Random.ArrayElement(new[] {Faker.Commerce.ProductDescription(), ""});
                        break;
                    case EntryTypeEnum.Company:
                        entry.Name = Faker.Company.CompanyName();
                        entry.Description = Faker.Random.ArrayElement(new[] {Faker.Company.CatchPhrase(), ""});
                        break;
                }

                Db.Entries.Add(entry);
            }
        }

        var creator = new Entry()
        {
            Id = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4"),
            Name = "Вася Пупкин",
            Description = Faker.Lorem.Paragraph(),
            EntryType = EntryTypeEnum.Person,
            CreatedAt = Faker.Date.Past(),
            UpdatedAt = Faker.Date.Past(),
            StartAt = Faker.Date.Past(),
            EndAt = Faker.Date.Past(),
            DeletedReason = Faker.Lorem.Paragraph(),
            // DeletedAt = Faker.Date.Past(),
        };

        Db.Entries.Add(creator);

        await Db.SaveChangesAsync();
    }
}