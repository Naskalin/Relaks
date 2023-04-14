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
                var eInfo = CreateEntryInfo(entry.Id);
                ToPhone(eInfo);
                Db.EntryInfos.Add(eInfo);
            }

            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var eInfo = CreateEntryInfo(entry.Id);
                ToEmail(eInfo);
                Db.EntryInfos.Add(eInfo);
            }

            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var eInfo = CreateEntryInfo(entry.Id);
                ToDate(eInfo);
                Db.EntryInfos.Add(eInfo);
            }

            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var eInfo = CreateEntryInfo(entry.Id);
                ToNote(eInfo);
                Db.EntryInfos.Add(eInfo);
            }

            for (int j = 0; j < random.Next(1, 3); j++)
            {
                var eInfo = CreateEntryInfo(entry.Id);
                ToUrl(eInfo);
                Db.EntryInfos.Add(eInfo);
            }

            // for (int j = 0; j < random.Next(1, 3); j++)
            // {
            //     var eInfo = CreateEntryInfo(entry.Id);
            //     ToCustom(eInfo);
            //     Db.EntryInfos.Add(eInfo);
            // }
        }

        var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");

        var eInfoDefault = new EntryInfo()
        {
            Title = "Phone of creator",
            EntryId = entryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedReason = ""
        };
        var phone = (EntryInfo) ToPhone(eInfoDefault).Clone();
        phone.Id = Guid.Parse("07315DD9-8587-4B73-A53A-0C399968647E");

        var email = (EntryInfo) ToEmail(eInfoDefault).Clone();
        email.Title = "Email of creator";
        email.Id = Guid.Parse("11434594-3FD0-46B5-94D3-B6C1DE188824");

        var date = (EntryInfo) ToDate(eInfoDefault).Clone();
        date.Title = "Date of creator";
        date.Id = Guid.Parse("F6B4E7B6-A7E4-4CDB-8EFF-68BB30FAA392");

        var url = (EntryInfo) ToUrl(eInfoDefault).Clone();
        url.Title = "Url of creator";
        url.Id = Guid.Parse("07C18674-296E-41BE-9087-E92675007059");

        var note = (EntryInfo) ToNote(eInfoDefault).Clone();
        note.Title = "Note of creator";
        note.Id = Guid.Parse("f23bfe8c-beb4-450e-b28f-1d3c8e6c4bbd");

        // for (int j = 0; j < random.Next(5, 10); j++)
        // {
        //     var eInfo = (EntryInfo) ToCustom(eInfoDefault).Clone();
        //     eInfo.Title = Faker.Random.ArrayElement(new[] {Faker.Random.Words(), ""});
        //     Db.EntryInfos.Add(eInfo);
        // }
        

        Db.EntryInfos.AddRange(
            phone,
            email,
            date,
            url,
            note
        );

        Db.SaveChanges();
    }

    private EntryInfo CreateEntryInfo(Guid entryId)
    {
        var eInfo = new EntryInfo()
        {
            EntryId = entryId,
            Title = Faker.Random.ArrayElement(new[] {Faker.Random.Words(), ""}),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedReason = ""
        };

        if (Faker.Random.Number(1, 10) > 8)
        {
            eInfo.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), ""});
            eInfo.DeletedAt = Faker.Date.Past();
        }

        return eInfo;
    }

    private EntryInfo ToPhone(EntryInfo eInfo)
    {
        var info = new PhoneInfo {Number = "7812000000" + Faker.Random.Number(0, 9), Region = "RU"};
        eInfo.Value = JsonSerializer.Serialize(info, InfoValueHelper.WriteOptions);
        eInfo.Type = EntryInfo.Phone;
        return eInfo;
    }

    private EntryInfo ToEmail(EntryInfo eInfo)
    {
        var info = new EmailInfo() {Email = Faker.Internet.Email()};
        eInfo.Value = JsonSerializer.Serialize(info, InfoValueHelper.WriteOptions);
        eInfo.Type = EntryInfo.Email;
        return eInfo;
    }

    private EntryInfo ToUrl(EntryInfo eInfo)
    {
        var info = new UrlInfo() {Url = Faker.Internet.Url()};
        eInfo.Value = JsonSerializer.Serialize(info, InfoValueHelper.WriteOptions);
        eInfo.Type = EntryInfo.Url;
        return eInfo;
    }

    private EntryInfo ToNote(EntryInfo eInfo)
    {
        var info = new NoteInfo() {Note = Faker.Random.Words()};
        eInfo.Value = JsonSerializer.Serialize(info, InfoValueHelper.WriteOptions);
        eInfo.Type = EntryInfo.Note;
        return eInfo;
    }

    private EntryInfo ToDate(EntryInfo eInfo)
    {
        var info = new DateInfo() {Date = Faker.Date.Past()};
        eInfo.Value = JsonSerializer.Serialize(info, InfoValueHelper.WriteOptions);
        eInfo.Type = EntryInfo.Date;
        return eInfo;
    }

    // private EntryInfo ToCustom(EntryInfo eInfo)
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