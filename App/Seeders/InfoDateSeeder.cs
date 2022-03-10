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
                    var infoDate = new InfoDate()
                    {
                        DateType = InfoDateTypeEnum.PersonBirthday,
                        Date = DateTime.Parse(dates[random.Next(dates.Length)])
                    };
                    
                    infoDate.Entries.Add(person);
                    Db.InfoDates.Add(infoDate);
                }
            }

            i++;
        }
        
        await Db.SaveChangesAsync();
    }
}