using App.DbConfigurations;
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
        var faker = new Faker()
        {
            Locale = "ru"
        };

        // foreach (EntryTypeEnum entryType in (EntryTypeEnum[]) Enum.GetValues(typeof(EntryTypeEnum)))
        // {
        //     for (int i = 0; i < 50; i++)
        //     {
        //         var entry = new Entry()
        //         {
        //             Reputation = faker.Random.Number(0, 10),
        //             EntryType = entryType,
        //             CreatedAt = faker.Date.Past(),
        //             UpdatedAt = faker.Date.Past(),
        //         };
        //
        //         var startAt = faker.Date.Past(120, DateTime.UtcNow.AddYears(-10));
        //         var diffYears = (int) Math.Floor(Math.Abs((startAt - DateTime.UtcNow).TotalDays) / 365);
        //         if (diffYears < 1) diffYears = 1;
        //         var endAt = faker.Date.Past(diffYears, DateTime.UtcNow);
        //
        //         if (i % faker.Random.Number(1, 5) == 0)
        //         {
        //             entry.StartAt = startAt;
        //         }
        //
        //         if (i % faker.Random.Number(1, 5) == 0)
        //         {
        //             entry.EndAt = endAt;
        //             if (i % 2 == 0)
        //             {
        //                 entry.StartAt = startAt;
        //             }
        //         }
        //
        //         if (i % faker.Random.Number(1, 2) == 0)
        //         {
        //             entry.DeletedReason = faker.Random.ArrayElement(new[] {faker.Lorem.Paragraph(), ""});
        //             entry.DeletedAt = faker.Date.Past();
        //         }
        //
        //         switch (entryType)
        //         {
        //             case EntryTypeEnum.Person:
        //                 entry.Name = faker.Name.FullName();
        //                 entry.Description = faker.Random.ArrayElement(new[] {faker.Name.JobDescriptor(), ""});
        //                 break;
        //             case EntryTypeEnum.Meet:
        //                 entry.Name = faker.Commerce.Department();
        //                 entry.Description = faker.Random.ArrayElement(new[] {faker.Commerce.ProductDescription(), ""});
        //                 break;
        //             case EntryTypeEnum.Company:
        //                 entry.Name = faker.Company.CompanyName();
        //                 entry.Description = faker.Random.ArrayElement(new[] {faker.Company.CatchPhrase(), ""});
        //                 break;
        //         }
        //
        //
        //         // if (i % 2 == 0)
        //         // {
        //         //     person.Description = "At vero eos et accusamus et iusto odio dignissimos ducimus";
        //         // }
        //
        //         Db.Entries.Add(entry);
        //     }
        // }

        var creator = new Entry()
        {
            Id = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4"),
            Name = "Вася Пупкин",
            Description = faker.Lorem.Paragraph(),
            EntryType = EntryTypeEnum.Person,
            CreatedAt = faker.Date.Past(),
            UpdatedAt = faker.Date.Past(),
            StartAt = faker.Date.Past(),
            EndAt = faker.Date.Past(),
            DeletedReason = faker.Lorem.Paragraph(),
            DeletedAt = faker.Date.Past(),
        };

        Db.Entries.Add(creator);

        await Db.SaveChangesAsync();
    }
}