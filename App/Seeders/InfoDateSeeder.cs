using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class InfoDateSeeder : DatabaseSeeder
{
    public InfoDateSeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        var persons = Db.Entries.Where(x => true).ToList();
        var dates = new[] {"1988-05-04 00:00:00", "1984-10-06 00:00:00", "1988-08-03 00:00:00", "2007-10-30 00:00:00"};

        var i = 0;
        var random = new Random();
        foreach (var person in persons)
        {
            if (i % random.Next(1, 5) == 0)
            {
                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    var entryDate = new EntryDate()
                    {
                        About = "Первая встреча",
                        Val = DateTime.Parse(dates[random.Next(dates.Length)]),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        ActualStartAt = DateTime.UtcNow,
                        ActualEndAtReason = "",
                        ActualStartAtReason = ""
                    };
                    
                    entryDate.Entry = person;
                    Db.EntryDates.Add(entryDate);
                }
            }

            i++;
        }

        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");
        var entryDateId = Guid.Parse("F6B4E7B6-A7E4-4CDB-8EFF-68BB30FAA392");

        var eDate = new EntryDate()
        {
            About = "Birthday of creator",
            EntryId = entryId,
            Id = entryDateId,
            Val = DateTime.Parse("1988-05-04 00:00:00"),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ActualStartAt = DateTime.UtcNow,
            ActualEndAtReason = "",
            ActualStartAtReason = ""
        };

        Db.EntryDates.Add(eDate);

        await Db.SaveChangesAsync();
    }
}