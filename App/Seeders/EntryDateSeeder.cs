// using App.DbConfigurations;
// using App.Models;
//
// namespace App.Seeders;
//
// public class EntryDateSeeder : DatabaseSeeder
// {
//     public EntryDateSeeder(AppDbContext db) : base(db)
//     {
//     }
//
//     public void Seed()
//     {
//         var entries = Db.Entries.Where(x => true).ToList();
//
//         var i = 0;
//         var random = new Random();
//         foreach (var entry in entries)
//         {
//             for (int j = 0; j < random.Next(1, 5); j++)
//             {
//                 var entryDate = new EntryDate()
//                 {
//                     Title = "Первая встреча",
//                     Date = Faker.Date.Past(),
//                     CreatedAt = DateTime.UtcNow,
//                     UpdatedAt = DateTime.UtcNow,
//                     DeletedReason = ""
//                 };
//                     
//                 if (Faker.Random.Number(1, 10) > 8)
//                 {
//                     entryDate.DeletedReason = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(), ""});
//                     entryDate.DeletedAt = Faker.Date.Past();
//                 }
//                     
//                 entryDate.EntryId = entry.Id;
//                 Db.EntryDates.Add(entryDate);
//             }
//
//             i++;
//         }
//
//         var entryId = Guid.Parse("01FBDDDD-1D69-4757-A8D2-5050A1AED4D4");
//         var entryDateId = Guid.Parse("F6B4E7B6-A7E4-4CDB-8EFF-68BB30FAA392");
//
//         var eDate = new EntryDate()
//         {
//             Title = "Birthday of creator",
//             EntryId = entryId,
//             Id = entryDateId,
//             Date = DateTime.Parse("1988-05-04 00:00:00"),
//             CreatedAt = DateTime.UtcNow,
//             UpdatedAt = DateTime.UtcNow,
//             DeletedReason = ""
//         };
//
//         Db.EntryDates.Add(eDate);
//
//         Db.SaveChanges();
//     }
// }