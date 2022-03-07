using App.DbConfigurations;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Seeders;

public class InfoDateSeeder : DatabaseSeeder
{
    public InfoDateSeeder(AppDbContext db) : base(db)
    {
    }

    public async Task Seed()
    {
        var persons = Db.Persons.Where(x => true).ToList();

        var i = 0;
        foreach (var person in persons)
        {
            if (i % 2 == 0)
            {
                var infoDate = new InfoDate()
                {
                    DateType = InfoDateTypeEnum.Birthday,
                    Date = DateTime.Parse("1988-05-04 00:00:00")
                };
                
                infoDate.Entries.Add(person);
                Db.InfoDates.Add(infoDate);
            }

            i++;
        }
        
        await Db.SaveChangesAsync();
    }
}