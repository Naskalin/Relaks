using Relaks.Models;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    private void SeedEntries()
    {
        for (int i = 0; i < 100; i++)
        {
            var rnd = Faker.Random.Number(1, 3);
            switch (rnd)
            {
                case 1:
                    var ePerson = new EPerson()
                    {
                        Name = Faker.Name.FullName(),
                        Description = Faker.Random.ArrayElement(new[] {Faker.Name.JobDescriptor(), ""})
                    };
                    FakeEntry(ePerson);
                    Db.EPersons.Add(ePerson);
                    break;
                case 2:
                    var eCompany = new ECompany()
                    {
                        Name = Faker.Commerce.Department(),
                        Description = Faker.Random.ArrayElement(new[] {Faker.Commerce.ProductDescription(), ""}),
                    };
                    FakeEntry(eCompany);
                    Db.ECompanies.Add(eCompany);
                    break;
                case 3:
                    var eMeet = new EMeet()
                    {
                        Name = Faker.Company.CompanyName(),
                        Description = Faker.Random.ArrayElement(new[] {Faker.Company.CatchPhrase(), ""}),
                    };
                    FakeEntry(eMeet);
                    Db.EMeets.Add(eMeet);
                    break;
            }
        }

        var creator = new EPerson()
        {
            Id = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4"),
            Name = "Вася Пупкин",
            Description = "Книги, Дом & Электроника Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles",
            CreatedAt = Faker.Date.Past(),
            UpdatedAt = Faker.Date.Past(),
            StartAt = Faker.Date.Past(),
            EndAt = Faker.Date.Past(),
        };

        Db.EPersons.Add(creator);

        var creatorCompany = new ECompany()
        {
            Id = Guid.Parse("01B137DA-A3CF-4C08-AC3E-752B3F156ED4"),
            Name = "Вася COMPANY",
            Description = Faker.Lorem.Paragraph(1),
            CreatedAt = Faker.Date.Past(),
            UpdatedAt = Faker.Date.Past(),
            StartAt = Faker.Date.Past(),
            EndAt = Faker.Date.Past(),
        };
        Db.ECompanies.Add(creatorCompany);

        Db.SaveChanges();
    }

    private void FakeEntry(BaseEntry entry)
    {
        // entry.Reputation = Faker.Random.Number(0, 10);
        entry.CreatedAt = Faker.Date.Past();
        entry.UpdatedAt = Faker.Date.Past();

        var startAt = Faker.Date.Past(120, DateTime.Now.AddYears(-10));
        var diffYears = (int) Math.Floor(Math.Abs((startAt - DateTime.Now).TotalDays) / 365);
        if (diffYears < 1) diffYears = 1;
        var endAt = Faker.Date.Past(diffYears, DateTime.Now);

        if (Faker.Random.Number(1, 5) >= 3)
        {
            entry.StartAt = startAt;
        }

        if (entry.StartAt != null)
        {
            if (Faker.Random.Number(1, 5) >= 3)
            {
                entry.EndAt = endAt;
            }
        }
        else if (Faker.Random.Number(1, 5) >= 3)
        {
            entry.EndAt = endAt;
        }

        if (Faker.Random.Number(1, 10) > 8)
        {
            entry.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), null});
            entry.DeletedAt = Faker.Date.Past();
        }
    }
}