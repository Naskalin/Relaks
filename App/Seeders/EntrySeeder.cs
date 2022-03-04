using App.Models.Entry;

namespace App.Seeders;

public class EntrySeeder : DatabaseSeeder
{
    public EntrySeeder(ApplicationContext db) : base(db)
    {
    }

    public void Seed()
    {
        Db.Entries.RemoveRange(Db.Entries);
        
        var names = new[] {"Tom", "Bob", "Mike", "Frida"};
        var random = new Random();
        for (int i = 0; i < 500; i++)
        {
            var person = new Person()
            {
                Name = names[random.Next(names.Length)],
                Reputation = random.Next(0, 10),
            };
            if (i % 2 == 0)
            {
                person.BirthDay = DateTime.UtcNow;
            }
            Db.Persons.Add(person);
        }
    }
}