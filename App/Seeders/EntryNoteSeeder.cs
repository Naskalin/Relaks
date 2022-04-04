using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class EntryNoteSeeder : DatabaseSeeder
{
    public EntryNoteSeeder(AppDbContext db) : base(db)
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
                    var entryDate = new EntryNote()
                    {
                        Title = "Первая встреча",
                        Note = Faker.Lorem.Paragraphs(),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        DeletedReason = ""
                    };
                    
                    if (j % Faker.Random.Number(1, 2) == 0)
                    {
                        entryDate.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), ""});
                        entryDate.DeletedAt = Faker.Date.Past();
                    }
                    
                    entryDate.EntryId = entry.Id;
                    Db.EntryNotes.Add(entryDate);
                }
            }

            i++;
        }

        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");
        var entryNoteId = Guid.Parse("f23bfe8c-beb4-450e-b28f-1d3c8e6c4bbd");

        var eDate = new EntryNote()
        {
            Title = "Birthday of creator",
            EntryId = entryId,
            Id = entryNoteId,
            Note = Faker.Lorem.Paragraphs(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedReason = ""
        };

        Db.EntryNotes.Add(eDate);

        await Db.SaveChangesAsync();
    }
}