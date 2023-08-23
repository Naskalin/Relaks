// using Relaks.Models;
// using Bogus;
//
// namespace Relaks.Seeders;
//
// public partial class DatabaseSeeder
// {
//
//     private void SeedInfoTemplate()
//     {
//         for (int i = 0; i < 10; i++)
//         {
//             var infoTemplate = new InfoTemplate()
//             {
//                 CreatedAt = DateTime.Now,
//                 UpdatedAt = DateTime.Now,
//                 Title = Faker.Lorem.Paragraph(1),
//                 Template = CreateCustomInfo()
//             };
//
//             Db.InfoTemplates.Add(infoTemplate);
//         }
//
//         Db.SaveChanges();
//     }
//
//     private CustomInfo CreateCustomInfo()
//     {
//         List<CustomInfoGroup> groups = new();
//         var random = new Random();
//
//         for (int i = 0; i < random.Next(1, 3); i++)
//         {
//             List<CustomInfoItem> items = new();
//
//             for (int j = 0; j < random.Next(3, 10); j++)
//             {
//                 var item = new CustomInfoItem
//                 {
//                     Key = Faker.Random.Words(),
//                     Value = Faker.Lorem.Paragraph(1)
//                 };
//                 var itemRand = random.Next(1, 3);
//                 if (itemRand == 1) item.Value = "";
//                 else if (itemRand == 2) item.Key = "";
//                 items.Add(item);
//             }
//
//             var group = new CustomInfoGroup()
//             {
//                 Title = Faker.Random.ArrayElement(new[] {Faker.Lorem.Paragraph(1), ""}),
//                 Items = items
//             };
//
//             groups.Add(group);
//         }
//
//         return new CustomInfo()
//         {
//             Groups = groups
//         };
//     }
// }