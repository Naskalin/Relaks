using App.DbConfigurations;
using App.Models;

namespace App.Seeders;

public class EntrySeeder : DatabaseSeeder
{
    public EntrySeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        var names = new[] {"Tom", "Bob", "Mike", "Frida"};
        var random = new Random();
        for (int i = 0; i < 10; i++)
        {
            var person = new Person()
            {
                Name = names[random.Next(names.Length)],
                Reputation = random.Next(0, 10),
            };

            Db.Persons.Add(person);
        }
        
        await Db.SaveChangesAsync();
    }
}