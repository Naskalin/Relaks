using System.Text.Json;
using Relaks.Models;
using Relaks.Utils;

namespace Relaks.Database.Seeders;

public partial class DatabaseSeeder
{
    private void SeedEntryInfos()
    {
        var entries = Db.Entries.Where(x => true).ToList();

        var random = new Random();
        foreach (var entry in entries)
        {
            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var phone = new EiPhone();
                FakeEntryInfo(phone, entry.Id);
                phone.Number = "7812000000" + Faker.Random.Number(0, 9);
                phone.Region = "RU";
                Db.EiPhones.Add(phone);
            }
            
            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var email = new EiEmail();
                FakeEntryInfo(email, entry.Id);
                email.Email = Faker.Internet.Email();
                Db.EiEmails.Add(email);
            }
            
            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var date = new EiDate();
                FakeEntryInfo(date, entry.Id);
                date.Date = Faker.Date.Past();
                Db.EiDates.Add(date);
            }
            //
            // for (int j = 0; j < random.Next(1, 3); j++)
            // {
            //     var eInfo = CreateEntryInfo(entry.Id);
            //     ToNote(eInfo);
            //     Db.EntryInfos.Add(eInfo);
            // }
            //
            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var url = new EiUrl();
                FakeEntryInfo(url, entry.Id);
                url.Url = Faker.Internet.Url();
                Db.EiUrls.Add(url);
            }

            // for (int j = 0; j < random.Next(1, 3); j++)
            // {
            //     var eInfo = new EiCustom();
            //     FakeEntryInfo(eInfo, entry.Id);
            //     ToCustom(eInfo);
            //     Db.EntryInfos.Add(eInfo);
            // }
        }

        Db.SaveChanges();
    }

    private void FakeEntryInfo(EntryInfo eInfo, Guid entryId)
    {
        eInfo.EntryId = entryId;
        eInfo.Title = Faker.Random.ArrayElement(new[] {Faker.Random.Words(), null});
        eInfo.CreatedAt = DateTime.UtcNow;
        eInfo.UpdatedAt = DateTime.UtcNow;
        eInfo.DeletedReason = null;

        if (Faker.Random.Number(1, 10) > 8)
        {
            eInfo.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), null});
            eInfo.DeletedAt = Faker.Date.Past();
        }
    }

    // private void ToCustom(EiCustom eInfo)
    // {
    //     var info = CreateCustomInfo();
    //
    //     eInfo.Value = JsonSerializer.Serialize(info, InfoValueHelper.WriteOptions);
    //     eInfo.Type = EntryInfo.Custom;
    //     eInfo.IsFavorite = Faker.Random.Bool(0.3F);
    //
    //     return eInfo;
    // }
}