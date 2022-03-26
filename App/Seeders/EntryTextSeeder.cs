using App.DbConfigurations;
using App.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace App.Seeders;

public class EntryTextSeeder : DatabaseSeeder
{
    public EntryTextSeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        var entries = Db.Entries.Where(x => true).ToList();
        var texts = new[]
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
            "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.",
            "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?",
            "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure,",
            "Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?",
        };

        var faker = new Faker()
        {
            Locale = "ru"
        };

        var random = new Random();
        foreach (var entry in entries)
        {
            for (int j = 0; j < random.Next(2, 10); j++)
            {
                var entryText = new EntryText()
                {
                    EntryId = entry.Id,
                    TextType = TextTypeEnum.Note,
                    About = "",
                    Val = texts[random.Next(texts.Length)],
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ActualStartAt = DateTime.UtcNow,
                    ActualEndAtReason = "",
                    ActualStartAtReason = ""
                };
                
                Db.EntryTexts.Add(entryText);
            }

            for (int i = 0; i < random.Next(1, 5); i++)
            {
                var textPhone = new EntryText()
                {
                    EntryId = entry.Id,
                    TextType = TextTypeEnum.Phone,
                    Val = "RU|+7812000000" + i,
                    About = faker.Random.Words(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ActualStartAt = DateTime.UtcNow,
                    ActualEndAtReason = faker.Random.Words(),
                    ActualStartAtReason = faker.Random.Words()
                };
                
                Db.EntryTexts.Add(textPhone);
            }
            
            for (int i = 0; i < random.Next(1, 5); i++)
            {
                var textPhone = new EntryText()
                {
                    EntryId = entry.Id,
                    TextType = TextTypeEnum.Email,
                    Val = faker.Internet.Email().ToLower(),
                    About = faker.Random.Words(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ActualStartAt = DateTime.UtcNow,
                    ActualEndAtReason = "",
                    ActualStartAtReason = ""
                };
                
                Db.EntryTexts.Add(textPhone);
            }
            
            for (int i = 0; i < random.Next(1, 5); i++)
            {
                var textPhone = new EntryText()
                {
                    EntryId = entry.Id,
                    TextType = TextTypeEnum.Url,
                    Val = faker.Internet.Url(),
                    About = faker.Random.Words(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ActualStartAt = DateTime.UtcNow,
                    ActualEndAtReason = "",
                    ActualStartAtReason = ""
                };
                
                Db.EntryTexts.Add(textPhone);
            }
        }
        
        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");
        var entryTextId = Guid.Parse("1C9C78A7-07E7-441D-A09D-551EE68E2616");
        
        var eText = new EntryText()
        {
            About = "The best of the best email",
            EntryId = entryId,
            Id = entryTextId,
            Val = "1acco@mail.ru",
            TextType = TextTypeEnum.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ActualStartAt = DateTime.UtcNow,
            ActualEndAtReason = "",
            ActualStartAtReason = ""
        };

        Db.EntryTexts.Add(eText);

        await Db.SaveChangesAsync();
    }
}