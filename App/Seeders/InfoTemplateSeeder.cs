using App.DbConfigurations;
using App.Models;
using Bogus;

namespace App.Seeders;

public class InfoTemplateSeeder : DatabaseSeeder
{
    public InfoTemplateSeeder(AppDbContext db) : base(db)
    {
    }

    public void Seed()
    {
        for (int i = 0; i < 10; i++)
        {
            var infoTemplate = new InfoTemplate()
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Title = Faker.Lorem.Paragraph(1),
                Template = CreateCustomInfo(Faker)
            };

            Db.InfoTemplates.Add(infoTemplate);
        }

        Db.SaveChanges();
    }

    public static CustomInfo CreateCustomInfo(Faker faker)
    {
        List<CustomInfoGroup> groups = new();
        var random = new Random();

        for (int i = 0; i < random.Next(1, 3); i++)
        {
            List<CustomInfoItem> items = new();

            for (int j = 0; j < random.Next(3, 10); j++)
            {
                var item = new CustomInfoItem
                {
                    Key = faker.Random.Words(),
                    Value = faker.Lorem.Paragraph(1)
                };
                var itemRand = random.Next(1, 3);
                if (itemRand == 1) item.Value = "";
                else if (itemRand == 2) item.Key = "";
                items.Add(item);
            }

            var group = new CustomInfoGroup()
            {
                Title = faker.Random.ArrayElement(new[] {faker.Lorem.Paragraph(1), ""}),
                Items = items
            };

            groups.Add(group);
        }

        return new CustomInfo()
        {
            Groups = groups
        };
    }
}